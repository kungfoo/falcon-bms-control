local Ded = Class {
    image = nil, -- drawable image
    imageData = nil
}

function Ded:init(id, x, y)
    self.id = id
    self.position = {x = x or 0, y = y or 0}
end

function Ded:update(dt)
end

function Ded:draw()
    if self.imageData then
        love.graphics.setColor(1, 1, 1)
        if not self.image then
          -- only load image if it is a new frame.
          self.image = love.graphics.newImage(self.imageData)
        end
        love.graphics.draw(self.image, self.position.x, self.position.y, 0, 0.85, 0.85)
      else
        -- TODO: draw no data string here.
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