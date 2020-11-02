local layout = require("lib.suit.layout").new()

local MfdButton = require("components.mfd-button")

local Mfd = Class {buttons = {}}

function Mfd:init(identifier, x, y)
  self.id = identifier
  self.position = {x = x or 0, y = y or 0}
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
  -- TODO: draw stencil for mfd border here.
  for _, button in ipairs(self.buttons) do button:draw() end
  if self.imageData then
    love.graphics.setColor(Colors.white)
    if not self.image then
      -- only load image if it is a new frame.
      self.image = love.graphics.newImage(self.imageData)
    end
    love.graphics.draw(self.image, self.position.x + MfdButton.size, self.position.y + MfdButton.size, 0, 0.85, 0.85)
  else
    -- TODO: draw no data string here.
  end
end

function Mfd:update(dt)
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
