local Component = Class {padding = 10, width = 30}

function Component:init(value, min, max, setter, style, options)
  self.transform = love.math.newTransform()
  self.value = (value - min) / (max - min)
  self.min = min
  self.max = max
  self.setter = setter

  local p = style or {}
  self.width = p.width or self.width
  self.orientation = p.orientation or 'horizontal'
  self.track = p.track or 'rectangle'
  self.knob = p.knob or 'rectangle'

  local o = options or {}
  self.values = o.values or nil

  self.grabbed = false
end

function Component:draw()
  love.graphics.push()
  love.graphics.applyTransform(self.transform)
  love.graphics.setColor(Colors.white)

  if self.track == 'rectangle' then
    if self.orientation == 'horizontal' then
      love.graphics.rectangle('line', 0, 0, self.bounds.w, self.width)
    elseif self.orientation == 'vertical' then
      love.graphics.rectangle('line', 0, 0, self.width, self.bounds.h)
    end
  elseif self.track == 'line' then
    love.graphics.setLineWidth(3)
    if self.orientation == 'horizontal' then
      love.graphics.line(0, self.width / 2, self.bounds.w, self.width / 2)
    elseif self.orientation == 'vertical' then
      love.graphics.line(self.width / 2, 0, self.width / 2, self.bounds.h)
    end
    love.graphics.setLineWidth(1)
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
  if self.grabbed then
    if self.setter ~= nil then
      local value = self.min + self.value * (self.max - self.min)
      if self.values then
        -- snap to closest value
        local dist = math.pow(2, 32)
        local index = 0
        for i, v in ipairs(self.values) do
          if math.abs(value - v) < dist then
            index = i
            dist = math.abs(value - v)
          end
        end
        value = self.values[index]
        self.value = (value - self.min) / (self.max - self.min)
      end
      -- call setter
      self.setter(value)
    end
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

  local dx = x - knobX
  local dy = y - knobY

  self.grabbed = true
  if self.orientation == 'horizontal' then
    self.value = self.value + dx / self.bounds.w
  elseif self.orientation == 'vertical' then
    self.value = self.value + dy / self.bounds.h
  end

  self.value = math.max(0, math.min(1, self.value))
end

return Component
