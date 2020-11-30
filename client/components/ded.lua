local StreamedTexture = require("util.streamed-texture")
local Ded = Class {
  -- max size of this component
  width = 460,
  height = 200
}

function Ded:init(id, x, y)
  self.id = id
  self.image = nil -- drawable image
  self.imageData = nil -- pure image data received from server
  self.transform = love.math.newTransform():translate(x or 0, y or 0)
end

function Ded:update(dt)
end

function Ded:draw()
  love.graphics.push()
  love.graphics.applyTransform(self.transform)

  if self.imageData then
    love.graphics.setColor(Colors.white)
    if not self.image then
      -- only load image if it is a new frame.
      self.image = love.graphics.newImage(self.imageData)
    end
    love.graphics.draw(self.image, 0, 0, 0, 1, 1)
  else
    love.graphics.setColor(Colors.white)
    love.graphics.rectangle("line", 0, 0, Ded.width, Ded.height, 0)
    love.graphics.print("DED data...", 0 + 10, 0 + Ded.height / 2 - 10)
  end

  love.graphics.pop()
end

function Ded:updateGeometry(x, y, w, h)
  local scale = self:determineScale(w, h)
  self.transform = love.math.newTransform()
  self.transform:translate(x, y):scale(scale)
end

function Ded:determineScale(w, h)
  if w >= Ded.width and h >= Ded.height then
    -- do not scale up
    return 1.0
  else
    local a, b = w / Ded.width, h / Ded.height
    return math.min(a, b)
  end
end

function Ded:consume(data)
  self.imageData = love.image.newImageData(love.data.newByteData(data))
  self.image = nil
end

function Ded:start()
  StreamedTexture.start(self.id)
end

function Ded:stop()
  StreamedTexture.stop()
end

function Ded:mousepressed(x, y, button, isTouch, presses)
  -- intentionally left blank
end

function Ded:mousereleased(x, y, button, isTouch, presses)
end

return Ded
