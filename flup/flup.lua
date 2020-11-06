local flup = {split = Class {}, fixed = Class {}, internal = {}}

function flup.split:init(options)
    self.direction = options.direction or "x"
    self.ratio = options.ratio or 0.5
    self.components = options.components or {}
end

function flup.split:draw() for _, c in pairs(self.components) do c:draw() end end

function flup.split:fill(x, y, w, h)
    flup.internal.updateGeometry(self, x, y, w, h)
end

function flup.fixed:init(options) end

function flup.internal.updateGeometry(node, x, y, w, h)
    if node.direction and node.ratio and node.components then
        -- assume is flup.split or compatible
        if node.direction == "x" then
            local p = w * node.ratio
            if node.components.left then
                flup.internal.updateGeometry(node.components.left, x, y, p, h)
            end

            if node.components.right then
                flup.internal.updateGeometry(node.components.right, x + p + 1,
                                             y, w - p, h)
            end
        end

        if node.direction == "y" then
            local p = h * node.ratio
            if node.components.top then
                flup.internal.updateGeometry(node.components.top, x, y, w, p)
            end

            if node.components.bottom then
                flup.internal.updateGeometry(node.components.bottom, x,
                                             y + p + 1, w, h - p)
            end
        end
    else
        if node.updateGeometry then node:updateGeometry(x, y, w, h) end
    end
end

return flup
