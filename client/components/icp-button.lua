local inspect = require("lib.inspect")

local IcpButton = Class {
  smallFont = love.graphics.newFont("fonts/DINRegular.ttf", 22, "normal"),
  largeFont = love.graphics.newFont("fonts/DINRegular.ttf", 32, "normal"),
  size = 80,
  isPressed = false,
  sounds = {
    pressed = love.audio.newSource("sounds/A/button-pressed.ogg", "static"),
    released = love.audio.newSource("sounds/A/button-released.ogg", "static"),
  },
}

function IcpButton:init(icp, id, options, x, y, w, h)
  self.icp = icp
  self.id = id
  self.x = x
  self.y = y
  self.w = w
  self.h = h
  self.options = options
  self.options.type = options.type or "square"
end

function IcpButton:draw()
  if self.options.type == "square" then self:drawSquare() end
  if self.options.type == "round" then self:drawRound() end
end

function IcpButton:drawSquare()
  if self.isPressed then
    love.graphics.setColor(0.3, 0.6, 0.3)
  else
    love.graphics.setColor(0.3, 0.3, 0.3)
  end
  love.graphics.rectangle("fill", self.x, self.y, self.w, self.h, 5, 5)
  love.graphics.setColor(.8, 1, 1)
  love.graphics.rectangle("line", self.x, self.y, self.w, self.h, 5, 5)

  -- labelling
  love.graphics.setColor(1, 1, 1)
  if self.options.number then
    love.graphics.setFont(self.largeFont)
    local x = self:hcentered(self.largeFont, self.options.number, self.x, self.size)
    local y = (self.y + self.size / 2) - 5
    love.graphics.printf(self.options.number, x, y, self.size)
  end

  if self.options.label then
    love.graphics.setFont(self.smallFont)
    local x = self:hcentered(self.smallFont, self.options.label, self.x, self.size)
    local y = self.y + 10
    if not self.options.number then y = self:vcentered(self.smallFont, self.y, self.size) end
    love.graphics.printf(self.options.label, x, y, self.size)
  end
end

function IcpButton:drawRound()
  if self.isPressed then
    love.graphics.setColor(0.3, 0.6, 0.3)
  else
    love.graphics.setColor(0.3, 0.3, 0.3)
  end
  local center = {x = self.x + self.size / 2, y = self.y + self.size / 2}
  local radius = self.size / 2
  love.graphics.circle("fill", center.x, center.y, radius)
  love.graphics.setColor(.8, 1, 1)
  love.graphics.circle("line", center.x, center.y, radius)

  -- labelling
  love.graphics.setColor(1, 1, 1)
  if self.options.number then
    love.graphics.setFont(self.largeFont)
    local x = self:hcentered(self.largeFont, self.options.number, self.x, self.size)
    local y = (self.y + self.size / 2) - 5
    love.graphics.printf(self.options.number, x, y, self.size)
  end

  if self.options.label then
    if self.options.number then
      love.graphics.setFont(self.smallFont)
      local x = self:hcentered(self.smallFont, self.options.label, self.x, self.size)
      local y = self.y + 10
      love.graphics.printf(self.options.label, x, y, self.size)
    else
      love.graphics.setFont(self.largeFont)
      local x = self:hcentered(self.largeFont, self.options.label, self.x, self.size)
      local y = self:vcentered(self.largeFont, self.y, self.size)
      love.graphics.printf(self.options.label, x, y, self.size)
    end
  end
end

function IcpButton:hcentered(font, text, x, width)
  local fWidth = font:getWidth(text)
  return (x + width / 2) - fWidth / 2
end

function IcpButton:vcentered(font, y, height)
  local fHeight = font:getHeight()
  return (y + height / 2) - fHeight / 2
end

function IcpButton:pressed()
  self.sounds.pressed:play()
  self.isPressed = true
  local message = {type = "icp-pressed", icp = self.icp, button = self.id}
  Signal.emit("send-to-server", message)
end

function IcpButton:released()
  self.sounds.released:play()
  self.isPressed = false
  local message = {type = "icp-released", icp = self.icp, button = self.id}
  Signal.emit("send-to-server", message)
end

function IcpButton:hit(x, y)
  return x >= self.x and x <= self.x + self.w and y >= self.y and y <= self.y + self.h
end

return IcpButton
