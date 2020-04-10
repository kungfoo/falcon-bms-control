local inspect = require("lib.inspect")

local IcpButton = Class {
  smallFont = love.graphics.newFont("fonts/MS33558.ttf", 18, "normal"),
  largeFont = love.graphics.newFont("fonts/MS33558.ttf", 24, "normal"),
  size = 80,
  lineOffset = 0,
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
  self.options.type = "square"
end

function IcpButton:draw()
  if self.options.type == "square" then
    if self.isPressed then
      love.graphics.setColor(0.3, 0.6, 0.3)
    else
      love.graphics.setColor(0.3, 0.3, 0.3)
    end
    love.graphics.rectangle("fill", self.x, self.y, self.w, self.h, 5, 5)
    love.graphics.setColor(.8, 1, 1)
    love.graphics.rectangle("line", self.x + self.lineOffset, self.y + self.lineOffset, self.w - self.lineOffset * 2,
                            self.h - self.lineOffset * 2, 3, 3)

    -- labelling follows
    love.graphics.setColor(1, 1, 1)
    if self.options.number then
      love.graphics.setFont(self.largeFont)
      local fWidth = self.largeFont:getWidth(self.options.number)
      love.graphics
        .printf(self.options.number, (self.x + self.size / 2) - fWidth / 2, self.y + self.size / 2, self.size)
    end

    if self.options.label then
      love.graphics.setFont(self.smallFont)
      local fWidth = self.smallFont:getWidth(self.options.label)
      love.graphics.printf(self.options.label, (self.x + self.size / 2) - fWidth / 2, self.y + 10, self.size)
    end
  end
end

function IcpButton:update(dt)

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
