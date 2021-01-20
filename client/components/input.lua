local utf8 = require 'utf8'

local Component = Class {}

function Component:init(options, callback)
  self.callback = callback or function()
    -- intentionally left blank
  end
end

function Component:draw()

end

function Component:updateGeometry(x, y, w, h)
  local scale = self:determineScale(w, h)
  self.transform = love.math.newTransform()
  self.w = w
  self.h = h
end

function Component:determineScale(w, h)
  return 1
end

function Component:update(dt)
end

function Component:mousepressed(x, y, button, touch, presses)
  local dx, dy = self.transform:inverseTransformPoint(x, y)
end

function Component:mousereleased(x, y, button, touch, presses)
end

function Component:textedited(text, start, length)
  print("textedited()", text, start, length)
end

function Component:textinput(t)
  print(t)
end

function Component:keypressed(key)
  print("keypressed()", key)
end

return Component
