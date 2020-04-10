local MfdButton = Class {
  size = 50,
  lineOffset = 8,
  isPressed = false,
  sounds = {
    pressed = love.audio.newSource("sounds/A/button-pressed.ogg", "static"),
    released = love.audio.newSource("sounds/A/button-released.ogg", "static"),
  },
}

function MfdButton:init(mfd, id, x, y, w, h)
  self.mfd = mfd
  self.id = id
  self.x = x
  self.y = y
  self.w = w
  self.h = h
end

function MfdButton:draw()
  if self.isPressed then
    love.graphics.setColor(0.3, 0.6, 0.3)
  else
    love.graphics.setColor(0.3, 0.3, 0.3)
  end
  love.graphics.rectangle("fill", self.x, self.y, self.w, self.h, 5, 5)
  love.graphics.setColor(.8, 1, 1)
  love.graphics.rectangle("line", self.x + self.lineOffset, self.y + self.lineOffset, self.w - self.lineOffset * 2,
                          self.h - self.lineOffset * 2, 3, 3)
end

function MfdButton:hit(x, y)
  return x >= self.x and x <= self.x + self.w and y >= self.y and y <= self.y + self.h
end

function MfdButton:pressed()
  self.sounds.pressed:play()
  self.isPressed = true
  local message = {type = "osb-pressed", mfd = self.mfd, osb = self.id}
  Signal.emit("send-to-server", message)
end

function MfdButton:released()
  self.sounds.released:play()
  self.isPressed = false
  local message = {type = "osb-released", mfd = self.mfd, osb = self.id}
  Signal.emit("send-to-server", message)
end

return MfdButton
