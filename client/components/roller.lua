local Roller = Class {
  mode = "vertical",
  radius = 5,
  currentValue = 0,
  minValue = 0,
  maxValue = 128,
  steps = 6,
  sounds = {},
}

function Roller:init(x, y, width, height)
  if width > height then self.mode = "horizontal" end
  self.x, self.y = x, y
  self.width, self.height = width or 30, height or 200
end

function Roller:draw()
  if self.isPressed then
    love.graphics.setColor(Colors.green)
  else
    love.graphics.setColor(Colors.white)
  end
  love.graphics.rectangle("line", self.x, self.y, self.width, self.height, self.radius)

  love.graphics.setColor(Colors.green)
  local indicator = {}
  love.graphics.rectangle("fill", indicator.x, indicator.y, indicator.width, indicator.height, self.radius)
end

function Roller:update(dt)
end

return Roller
