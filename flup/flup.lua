local flup = {split = Class {}, fixed = Class {}, grid = Class {rows = {}}, internal = {}}

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
  flup.split:_updateGeometry(self, x, y, w, h)
end

function flup.split:updateGeometry(x, y, w, h)
  flup.split:_updateGeometry(self, x, y, w, h)
end

function flup.fixed:init(options)
end

function flup.split:_updateGeometry(node, x, y, w, h)
  if node.direction and node.ratio and node.components then
    -- assume is flup.split or compatible
    local margin = node.margin or 0
    if node.direction == "x" then
      local p = w * node.ratio
      if node.components.left then flup.split:_updateGeometry(node.components.left, x, y, p - margin / 2, h) end

      if node.components.right then
        flup.split:_updateGeometry(node.components.right, x + p + 1 + margin / 2, y, w - p - margin / 2, h)
      end
    end

    if node.direction == "y" then
      local p = h * node.ratio
      if node.components.top then flup.split:_updateGeometry(node.components.top, x, y, w, p - margin / 2) end

      if node.components.bottom then
        flup.split:_updateGeometry(node.components.bottom, x, y + p + 1 + margin / 2, w, h - p - margin / 2)
      end
    end
  else
    -- must be a component then, let's tell it its size
    if node.updateGeometry then node:updateGeometry(x, y, w, h) end
  end
end

function flup.grid:init(options)
  self.rows = options.rows or {}
  self.weights = options.weights or {}
  self.margin = options.margin or 0
end

function flup.grid:fill(x, y, w, h)
  flup.grid:_updateGeometry(self, x, y, w, h)
end

function flup.grid:updateGeometry(x, y, w, h)
  flup.grid:_updateGeometry(self, x, y, w, h)
end

function flup.grid:draw()
  for i, row in pairs(self.rows) do for j, column in pairs(row.columns) do if column.draw then column:draw() end end end
end

function flup.grid:_updateGeometry(node, x, y, w, h)
  if node.rows then
    -- assume flup.grid or compatible
    local row_heights = flup.grid:_calculate_row_heights(node, h)
    local last_height = 0
    for i, row in ipairs(node.rows) do
      local y_start = y + last_height

      local num_columns = #row.columns
      local width_per_column = w / num_columns
      for j, column in ipairs(row.columns) do
        local x_start = x + width_per_column * (j - 1)

        if column._updateGeometry then
          column:_updateGeometry(column, x_start, y_start, width_per_column, row_heights[i])
        end
        -- must be a component then, let's tell it its size
        if column.updateGeometry then column:updateGeometry(x_start, y_start, width_per_column, row_heights[i]) end
      end
      last_height = last_height + row_heights[i]
    end
  end
end

-- performs a left to right reduction of t using f, with o as the initial value
-- reduce({1, 2, 3}, f, 0) -> f(f(f(0, 1), 2), 3)
-- (but performed iteratively, so no stack smashing)
function table.reduce(t, f, o)
  for i, v in ipairs(t) do o = f(o, v) end
  return o
end

-- return the numeric sum of all elements of t
function table.sum(t)
  return table.reduce(t, function(a, b)
    return a + b
  end, 0)
end

function flup.grid:_calculate_row_heights(node, h)
  local heights = {}
  -- distribute evenly
  for i, _ in ipairs(node.rows) do
    heights[i] = h / #node.rows
  end

  local sum = table.sum(node.weights)
  if node.weights then
    for i, weight in ipairs(node.weights) do
      heights[i] = h * (weight / sum)
    end
  end
  return heights
end

return flup
