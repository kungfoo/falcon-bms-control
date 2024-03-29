local Button = require("lib.button")

local MfdButton = Class({ size = 46, lineOffset = 8 })

function MfdButton:init(mfd, id, x, y, w, h)
  self.mfd = mfd
  self.id = id
  self.x = x
  self.y = y
  self.w = w
  self.h = h
  self.isPressed = false
end

function MfdButton:draw()
  if self.isPressed then
    love.graphics.setColor(Colors.green)
  else
    love.graphics.setColor(Colors.grey)
  end
  love.graphics.rectangle("fill", self.x, self.y, self.w, self.h, 5, 5)
  love.graphics.setColor(Colors.cyan)
  love.graphics.setLineWidth(1)
  love.graphics.rectangle(
    "line",
    self.x + self.lineOffset,
    self.y + self.lineOffset,
    self.w - self.lineOffset * 2,
    self.h - self.lineOffset * 2,
    3,
    3
  )
end

function MfdButton:hit(x, y)
  return x >= self.x and x <= self.x + self.w and y >= self.y and y <= self.y + self.h
end

function MfdButton:pressed()
  Button.pressed()
  self.isPressed = true
  local message = { type = "osb-pressed", mfd = self.mfd, osb = self.id }
  Signal.emit("send-to-server", message)
end

function MfdButton:released()
  Button.released()
  self.isPressed = false
  local message = { type = "osb-released", mfd = self.mfd, osb = self.id }
  Signal.emit("send-to-server", message)
end

return MfdButton
