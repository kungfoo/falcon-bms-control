local Ded = Class {}

function Ded:init(id, x, y, width, height)
  self.id = id
  self.image = nil -- drawable image
  self.imageData = nil -- pure image data received from server
  self.position = {x = x or 0, y = y or 0}
  self.dim = {width = width or 400, height = height or 130}
end

function Ded:update(dt)
end

function Ded:draw()
  if self.imageData then
    love.graphics.setColor(Colors.white)
    if not self.image then
      -- only load image if it is a new frame.
      self.image = love.graphics.newImage(self.imageData)
    end
    local dw = self.dim.width / self.image:getWidth()
    local dh = self.dim.height / self.image:getHeight()
    local scale = math.min(dw, dh)
    love.graphics.draw(self.image, self.position.x, self.position.y, 0, scale, scale)
  else
    love.graphics.setColor(Colors.white)
    love.graphics.rectangle("line", self.position.x, self.position.y, self.dim.width, self.dim.height, 0)
    love.graphics.print("DED data...", self.position.x + 10, self.position.y + self.dim.height / 2 - 10)
  end
end

function Ded:consume(data)
  self.imageData = love.image.newImageData(love.data.newByteData(data))
  self.image = nil
end

function Ded:mousepressed(x, y, button, isTouch, presses)
  -- intentionally left blank
end

function Ded:mousereleased(x, y, button, isTouch, presses)
end

return Ded
