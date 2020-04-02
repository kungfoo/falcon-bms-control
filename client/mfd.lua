local layout = require("lib.suit.layout").new()
local inspect = require("lib.inspect")

-- MfdButton

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

--- Mfd
local Mfd = Class {}

function Mfd:init(identifier, x, y)
  self.id = identifier
  self.position = {x = x or 0, y = y or 0}
  self.buttons = {}
  self:createButtons(identifier)
end

function Mfd:createButtons(identifier)
  layout:reset(self.position.x, self.position.y):padding(20, 20)

  -- top row of buttons
  layout:push(self.position.x, self.position.y)
  layout:col(MfdButton.size, MfdButton.size)
  for _, osb in ipairs {"OSB1", "OSB2", "OSB3", "OSB4", "OSB5"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:col()))
  end

  -- right column
  layout:right()
  for _, osb in ipairs {"OSB6", "OSB7", "OSB8", "OSB9", "OSB10"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:row()))
  end

  -- left column
  layout:pop()
  layout:push(self.position.x, self.position.y)
  layout:col(MfdButton.size, MfdButton.size)
  for _, osb in ipairs {"OSB20", "OSB19", "OSB18", "OSB17", "OSB16"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:row()))
  end

  -- bottom row
  layout:down()
  for _, osb in ipairs {"OSB15", "OSB14", "OSB13", "OSB12", "OSB11"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:col()))
  end
end

function Mfd:draw()
  for _, button in ipairs(self.buttons) do button:draw() end
  if self.imageData then
    love.graphics.setColor(1, 1, 1)
    if not self.image then
      -- only load image if it is a new frame.
      self.image = love.graphics.newImage(self.imageData)
    end
    love.graphics.draw(self.image, self.position.x + MfdButton.size, self.position.y + MfdButton.size, 0, 0.85, 0.85)
  else
    -- draw no data string here.
  end
end

function Mfd:update(dt)
  local message = {type = "streamed-texture", identifier = self.id}
  Signal.emit("send-to-server", message)
end

function Mfd:consume(data)
  self.imageData = love.image.newImageData(love.data.newByteData(data))
  self.image = nil
end

function Mfd:mousepressed(x, y, button, isTouch, presses)
  for _, button in ipairs(self.buttons) do
    if button:hit(x, y) then
      self.pressed = button
      button:pressed()
    end
  end
end

function Mfd:mousereleased(x, y, button, isTouch, presses)
  if self.pressed then
    self.pressed:released()
    self.pressed = nil
  end
end

return Mfd
