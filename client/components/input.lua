local utf8 = require 'utf8'

local Component = Class {has_focus = false, width = 500, height = 40, place_holder = "Enter some text..."}

function Component:init(value, options, callback)
  self.callback = callback or function()
    -- intentionally left blank
  end
  self.allows_input = options.allows_input or function(_, _)
    return true
  end

  self.font = love.graphics.newFont("fonts/b612/B612Mono-Regular.ttf", options.size or 20, "normal")
  self.transform = love.math.newTransform()
  self.value = value or ""
  self.place_holder = options.place_holder or self.place_holder
end

function Component:draw()
  love.graphics.push()
  love.graphics.applyTransform(self.transform)

  if self.has_focus then
    love.graphics.setColor(Colors.green)
  else
    love.graphics.setColor(Colors.white)
  end
  love.graphics.rectangle("line", 0, 0, self.w, self.height)
  if self.value == "" then
    love.graphics.setColor(Colors.grey)
    love.graphics.printf(self.place_holder, self.font, 5, 10, self.w)
  else
    love.graphics.setColor(Colors.white)
    love.graphics.printf(self.value, self.font, 5, 10, self.w)
  end

  love.graphics.pop()
end

function Component:setValue(value)
  self.value = value or ""
end

function Component:updateGeometry(x, y, w, h)
  self.transform = love.math.newTransform():translate(x, y)
  self.w = w
  self.h = h
end

function Component:update(dt)
end

function Component:mousepressed(x, y, button, touch, presses)
  local dx, dy = self.transform:inverseTransformPoint(x, y)
  local hit = dx >= 0 and dx <= self.w and dy >= 0 and dy <= self.height
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
  local x, y = self.transform:transformPoint(0, 0)
  love.keyboard.setTextInput(value, x, y, self.width, self.height)
end

function Component:textedited(text, start, length)
  -- intentionally left blank
end

function Component:textinput(key)
  -- intentionally left blank
end

function Component:keypressed(key)
  if self.has_focus then
    if self.allows_input(self.value, key) then self.value = self.value .. key end
    if key == "backspace" and string.len(self.value) > 0 then
      self.value = string.sub(self.value, 0, string.len(self.value) - 1)
    end

    self.callback(self.value)
  end
end

return Component
