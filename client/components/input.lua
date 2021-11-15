local utf8 = require 'utf8'

local Component = Class {has_focus = false, width = 400, height = 30}

function Component:init(options, callback)
  self.callback = callback or function()
    -- intentionally left blank
  end
  self.transform = love.math.newTransform()
end

function Component:draw()
  love.graphics.rectangle("fill", 0, 0, self.width, self.height)
end

function Component:updateGeometry(x, y, w, h)
  local scale = self:determineScale(w, h)
  self.width = w
  self.height = h
end

function Component:determineScale(w, h)
  return 1
end

function Component:update(dt)
end

function Component:mousepressed(x, y, button, touch, presses)
  local dx, dy = self.transform:inverseTransformPoint(x, y)
  local hit = dx >= 0 and dx <= self.width and dy >= 0 and dy <= self.height
  if hit then
    self:focus(true)
  else
    self:focus(false)
  end
end

function Component:mousereleased(x, y, button, touch, presses)
end

function Component:focus(value)
  self.has_focus = value
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
