local StreamedTexture = require("util.streamed-texture")
local layout = require("lib.suit.layout").new()

local MfdButton = require("components.mfd-button")

local Mfd = Class {}

function Mfd:init(identifier, x, y)
  self.id = identifier
  self.position = {x = x or 0, y = y or 0}
  self.dimensions = {w = 500, h = 500}
  self.buttons = {}
  self:createButtons(identifier)
end

function Mfd:createButtons(identifier)
  self.buttons = {}

  -- top row of buttons
  layout:push(self.position.x + 105, self.position.y):padding(61 - MfdButton.size, 0)
  for _, osb in ipairs {"OSB_1", "OSB_2", "OSB_3", "OSB_4", "OSB_5"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:col(MfdButton.size, MfdButton.size)))
  end

  -- right column
  layout:pop()
  layout:push(self.position.x + MfdButton.size + 410, self.position.y + 110):padding(0, 58 - MfdButton.size)
  for _, osb in ipairs {"OSB_6", "OSB_7", "OSB_8", "OSB_9", "OSB_10"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:row(MfdButton.size, MfdButton.size)))
  end

  -- left column
  layout:push(self.position.x, self.position.y + 110):padding(0, 58 - MfdButton.size)
  for _, osb in ipairs {"OSB_20", "OSB_19", "OSB_18", "OSB_17", "OSB_16"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:row(MfdButton.size, MfdButton.size)))
  end

  -- bottom row
  layout:pop()
  layout:push(self.position.x + 105, self.position.y + 410 + MfdButton.size):padding(61 - MfdButton.size, 0)
  for _, osb in ipairs {"OSB_15", "OSB_14", "OSB_13", "OSB_12", "OSB_11"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:col(MfdButton.size, MfdButton.size)))
  end
end

function Mfd:draw()
  -- scale to fit everything
  local scale = self:determineScale()
  love.graphics.push()
  love.graphics.scale(scale, scale)

  love.graphics.setColor(Colors.grey)
  love.graphics.rectangle("line", self.position.x, self.position.y, self.dimensions.w, self.dimensions.h, 0, 0)

  for _, button in ipairs(self.buttons) do button:draw() end
  if self.imageData then
    love.graphics.setColor(Colors.white)
    if not self.image then
      -- only load image if it is a new frame.
      self.image = love.graphics.newImage(self.imageData)
    end
    -- TODO: scale image according to component width and height, not a fixed scale.
    local texture_scale = 410 / self.image:getWidth()
    love.graphics.draw(self.image, self.position.x + MfdButton.size, self.position.y + MfdButton.size, 0, texture_scale, texture_scale)
  else
    -- TODO: draw no data string here.
  end

  love.graphics.pop()
end

function Mfd:determineScale()
  if self.dimensions.w >= 500 and self.dimensions.w >= 500 then
    -- do not scale up
    return 1.0
  else
    local d = math.min(self.dimensions.w, self.dimensions.h)
    return d / 500
  end
end

function Mfd:update(dt)
end

function Mfd:updateGeometry(x, y, w, h)
  self.position.x = x
  self.position.y = y
  self.dimensions.w = w
  self.dimensions.h = h
  self:createButtons()
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
