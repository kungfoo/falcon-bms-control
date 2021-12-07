local flup = {split = Class {}, fixed = Class {}, grid = Class {
  rows = {
    columns = {}
  }
}, internal = {}}

function flup.split:init(options)
  self.direction = options.direction or "x"
  self.ratio = options.ratio or 0.5
  self.components = options.components or {}
  self.margin = options.margin or 0
end

function flup.split:draw()
  for _, c in pairs(self.components) do c:draw() end
end

function flup.split:fill(x, y, w, h)
  flup.internal.updateGeometry(self, x, y, w, h)
end

function flup.fixed:init(options)
end

function flup.internal.updateGeometry(node, x, y, w, h)
  if node.direction and node.ratio and node.components then
    -- assume is flup.split or compatible
    local margin = node.margin or 0
    if node.direction == "x" then
      local p = w * node.ratio
      if node.components.left then flup.internal.updateGeometry(node.components.left, x, y, p - margin / 2, h) end

      if node.components.right then
        flup.internal.updateGeometry(node.components.right, x + p + 1 + margin / 2, y, w - p - margin / 2, h)
      end
    end

    if node.direction == "y" then
      local p = h * node.ratio
      if node.components.top then flup.internal.updateGeometry(node.components.top, x, y, w, p - margin / 2) end

      if node.components.bottom then
        flup.internal.updateGeometry(node.components.bottom, x, y + p + 1 + margin / 2, w, h - p - margin / 2)
      end
    end
  else
    -- must be a component then, let's tell it its size
    if node.updateGeometry then node:updateGeometry(x, y, w, h) end
  end
end

function flup.grid:init(options)
  self.rows = options.rows or {}
  self.margin = options.margin or 0
end

function flup.grid:fill(x, y, w, h)
  flup.internal.updateGridGeometry(self, x, y, w, h)
end

function flup.grid:draw()
  for i, row in pairs(self.rows) do
    for j, column in pairs(row.columns) do
      column:draw()
    end
  end
end

function flup.internal.updateGridGeometry(node, x, y, w, h)
  if node.rows then
    -- assume flup.grid or compatible
    local num_rows = #node.rows
    local height_per_row = h / num_rows

    for i, row in pairs(node.rows) do
      local y_start = height_per_row * (i-1)
      local y_end = y_start + height_per_row - 1

      local num_columns = #row.columns
      local width_per_column = w / num_columns
      for j, column in pairs(row.columns) do
        local x_start = width_per_column * (j-1)
        local x_end = x_start + width_per_column

        -- must be another grid-like thing
        if column.updateGridGeometry then
          flup.internal.updateGridGeometry(column, x_start, y_start, width_per_column, height_per_row)
        end
        -- must be a component then
        if column.updateGeometry then
          column:updateGeometry(x_start, y_start, width_per_column, height_per_row)
        end
      end
    end
  else
    -- must be a component then, let's tell it its size
    if node.updateGeometry then node:updateGeometry(x, y, w, h) end
  end
end

return flup
