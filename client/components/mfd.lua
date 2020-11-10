local StreamedTexture = require("util.streamed-texture")
local layout = require("lib.suit.layout").new()

local MfdButton = require("components.mfd-button")

local Mfd = Class {}

function Mfd:init(identifier, x, y)
  self.id = identifier
  self.buttons = {}
  self.transform = love.math.newTransform()
  self:createButtons(identifier)
end

function Mfd:createButtons(identifier)
  self.buttons = {}

  -- top row of buttons
  layout:push(105, 0):padding(61 - MfdButton.size, 0)
  for _, osb in ipairs {"OSB_1", "OSB_2", "OSB_3", "OSB_4", "OSB_5"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:col(MfdButton.size, MfdButton.size)))
  end

  -- right column
  layout:pop()
  layout:push(MfdButton.size + 410, 110):padding(0, 58 - MfdButton.size)
  for _, osb in ipairs {"OSB_6", "OSB_7", "OSB_8", "OSB_9", "OSB_10"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:row(MfdButton.size, MfdButton.size)))
  end

  -- left column
  layout:push(0, 110):padding(0, 58 - MfdButton.size)
  for _, osb in ipairs {"OSB_20", "OSB_19", "OSB_18", "OSB_17", "OSB_16"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:row(MfdButton.size, MfdButton.size)))
  end

  -- bottom row
  layout:pop()
  layout:push(105, 410 + MfdButton.size):padding(61 - MfdButton.size, 0)
  for _, osb in ipairs {"OSB_15", "OSB_14", "OSB_13", "OSB_12", "OSB_11"} do
    table.insert(self.buttons, MfdButton(identifier, osb, layout:col(MfdButton.size, MfdButton.size)))
  end
end

function Mfd:draw()
  -- scale to fit everything
  love.graphics.push()
  love.graphics.applyTransform(self.transform)

  -- love.graphics.setColor(Colors.green)
  -- love.graphics.rectangle("line", 0, 0, 500, 500, 0, 0)

  for _, button in ipairs(self.buttons) do button:draw() end
  if self.imageData then
    love.graphics.setColor(Colors.white)
    if not self.image then
      -- only load image if it is a new frame.
      self.image = love.graphics.newImage(self.imageData)
    end
    -- TODO: scale image according to component width and height, not a fixed scale.
    local texture_scale = 410 / self.image:getWidth()
    love.graphics.draw(self.image, MfdButton.size, MfdButton.size, 0, texture_scale, texture_scale)
  else
    -- TODO: draw no data string here.
  end

  love.graphics.pop()
end

function Mfd:determineScale(w, h)
  if w >= 500 and h >= 500 then
    -- do not scale up
    return 1.0
  else
    local d = math.min(w, h)
    return d / 500
  end
end

function Mfd:update(dt)
end

function Mfd:updateGeometry(x, y, w, h)
  local scale = self:determineScale(w, h)
  self.transform = love.math.newTransform()
  self.transform:translate(x, y):scale(scale)
  self:createButtons(self.id)
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
  local dx, dy = self.transform:inverseTransformPoint(x, y)
  for _, button in ipairs(self.buttons) do
    if button:hit(dx, dy) then
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
