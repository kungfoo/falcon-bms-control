local StreamedTexture = require("util.streamed-texture")
local layout = require("lib.suit.layout").new()

local MfdButton = require("components.mfd-button")

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
  for _, osb in ipairs {"OSB_1", "OSB_2", "OSB_3", "OSB_4", "OSB_5"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:col()))
  end

  -- right column
  layout:right()
  for _, osb in ipairs {"OSB_6", "OSB_7", "OSB_8", "OSB_9", "OSB_10"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:row()))
  end

  -- left column
  layout:pop()
  layout:push(self.position.x, self.position.y)
  layout:col(MfdButton.size, MfdButton.size)
  for _, osb in ipairs {"OSB_20", "OSB_19", "OSB_18", "OSB_17", "OSB_16"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:row()))
  end

  -- bottom row
  layout:down()
  for _, osb in ipairs {"OSB_15", "OSB_14", "OSB_13", "OSB_12", "OSB_11"} do
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

function Mfd:start()
  StreamedTexture.start(self.id)
end

function Mfd:stop()
  StreamedTexture.stop(self.id)
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
