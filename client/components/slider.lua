local Component = Class {padding = 10, slider_height = 20}

function Component:init(value, min, max, setter, style)
  self.transform = love.math.newTransform():translate(x or 0, y or 0)
  self.value = (value - min) / (max - min)
  self.min = min
  self.max = max
  self.setter = setter

  local p = style or {}
  self.width = p.width or 30
  self.orientation = p.orientation or 'horizontal'
  self.track = p.track or 'rectangle'
  self.knob = p.knob or 'rectangle'

  self.grabbed = false
  self.ox = 0
  self.oy = 0

  print(inspect(self))
end

function Component:draw()
  love.graphics.push()
  love.graphics.applyTransform(self.transform)

  if self.track == 'rectangle' then
    if self.orientation == 'horizontal' then
      love.graphics.rectangle('line', 0, 0, self.bounds.w, self.width)
    elseif self.orientation == 'vertical' then
      love.graphics.rectangle('line', 0, 0, self.width, self.bounds.h)
    end
  elseif self.track == 'line' then
    love.graphics.setLineWidth(4)
    if self.orientation == 'horizontal' then
      love.graphics.line(0, self.width / 2, self.bounds.w, self.width / 2)
    elseif self.orientation == 'vertical' then
      love.graphics.line(self.bounds.w / 2, 0, self.bounds.w / 2, self.bounds.h)
    end
  end

  local knobX = 0
  local knobY = 0
  if self.orientation == 'horizontal' then
    knobX = (self.bounds.w - self.width) * self.value
  elseif self.orientation == 'vertical' then
    knobY = (self.bounds.h - self.width) * self.value
  end

  if self.grabbed then love.graphics.setColor(Colors.green) end

  if self.knob == 'rectangle' then
    love.graphics.rectangle('fill', knobX, knobY, self.width, self.width)
  elseif self.knob == 'circle' then
    love.graphics.circle('fill', knobX + self.width / 2, knobY + self.width / 2, self.width / 2)
  end
  love.graphics.setColor(Colors.white)

  love.graphics.pop()
end

function Component:updateGeometry(x, y, w, h)
  self.transform = love.math.newTransform()
  self.transform:translate(x, y):scale(1)
  self.bounds = {}
  self.bounds.w = w
  self.bounds.h = h
end

function Component:update(dt)
  if self.grabbed then
    local dx, dy = self.transform:inverseTransformPoint(love.mouse.getX(), love.mouse.getY())
    self:dragged(dx, dy)
  end
end

function Component:mousepressed(x, y, button, touch, presses)
  local dx, dy = self.transform:inverseTransformPoint(x, y)
  if self:slider_hit(dx, dy) then self:dragged(dx, dy) end
end

function Component:slider_hit(x, y)
  if self.orientation == 'horizontal' then
    return x >= 0 and x <= self.bounds.w and y >= 0 and y <= self.width
  else
    return x >= 0 and x <= self.width and y >= 0 and y <= self.bounds.h
  end
end

function Component:mousereleased(x, y, button, touch, presses)
  local dx, dy = self.transform:inverseTransformPoint(x, y)
  if self.grabbed then
    if self.setter ~= nil then self.setter(self.min + self.value * (self.max - self.min)) end
    self.grabbed = false
  end
end

function Component:dragged(x, y)
  local knobX = 0
  local knobY = 0
  if self.orientation == 'horizontal' then
    knobX = self.bounds.w * self.value
  elseif self.orientation == 'vertical' then
    knobY = self.bounds.h * self.value
  end

  local ox = x - knobX
  local oy = y - knobY

  local dx = ox - self.ox
  local dy = oy - self.oy
  self.grabbed = true
  if self.orientation == 'horizontal' then
    self.value = self.value + dx / self.bounds.w
  elseif self.orientation == 'vertical' then
    self.value = self.value - dy / self.bounds.h
  end

  self.value = math.max(0, math.min(1, self.value))

  -- if pressed then
  --     if self.grabbed then
  --         
  --     elseif (x > knobX - self.width/2 and x < knobX + self.width/2 and y > knobY - self.width/2 and y < knobY + self.width/2) and not self.wasDown then
  --         self.ox = ox
  --         self.oy = oy
  --         self.grabbed = true
  --     end
  -- else
  --     self.grabbed = false
  -- end

  -- 

  -- if self.setter ~= nil then
  --     self.setter(self.min + self.value * (self.max - self.min))
  -- end

  -- self.wasDown = down
end

return Component
