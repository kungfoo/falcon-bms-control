require("class")

local Snip = Class({})
-- Sample component drawing something
function Snip:init(label)
  self.label = label
  self.r = love.math.random()
  self.g = love.math.random()
  self.b = love.math.random()
end

function Snip:updateGeometry(x, y, w, h)
  -- print(self.label, x, y, w, h)
  self.x = x
  self.y = y
  self.w = w
  self.h = h
end

function Snip:draw()
  if self.x then
    love.graphics.setColor(self.r, self.g, self.b)
    love.graphics.rectangle("line", self.x, self.y, self.w, self.h, 10, 10)
    love.graphics.setColor(1, 1, 1)
    love.graphics.printf(self.label .. " " .. self.w .. "x" .. self.h, math.floor(self.x + 10), math.floor(self.y + 10), self.w)
  end
end

return Snip
