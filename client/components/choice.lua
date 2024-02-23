local Choice = Class({
  nowrap = true,
  height = 40,
})

local Button = require("lib.button")

function Choice:init(values, onChange, options)
  self.values = values or {}
  self.options = options or {}
  self.onChange = onChange or function(value) end
  self.font = love.graphics.newFont("fonts/b612/B612Mono-Regular.ttf", self.options.size or 20, "normal")
  self.transform = love.math.newTransform()
  self.index = 1
  self.pressed = false
end

function Choice:draw()
  love.graphics.push()
  love.graphics.applyTransform(self.transform)

  local x, y, w, h = 0, 0, self.w, self.height

  if self.has_focus then
    love.graphics.setColor(Colors.green)
  else
    love.graphics.setColor(Colors.white)
  end
  love.graphics.rectangle("line", 0, 0, w, h)

  love.graphics.setLineStyle("smooth")
  love.graphics.setLineWidth(3)
  love.graphics.setLineJoin("bevel")

  if self.nowrap and self.index == 1 then
    love.graphics.setColor(Colors.grey)
  else
    love.graphics.setColor(Colors.green)
  end

  love.graphics.line(x + h * 0.8, y + h * 0.2, x + h * 0.5, y + h * 0.5, x + h * 0.8, y + h * 0.8)

  if self.nowrap and self.index == #self.values then
    love.graphics.setColor(Colors.grey)
  else
    love.graphics.setColor(Colors.green)
  end

  love.graphics.line(x + w - h * 0.8, y + h * 0.2, x + w - h * 0.5, y + h * 0.5, x + w - h * 0.8, y + h * 0.8)

  local text = self.values[self.index].text

  love.graphics.setColor(Colors.white)
  love.graphics.setFont(self.font)
  love.graphics.printf(text, x + h + 2, y + 5, w - 2 * (h + 2), "center")

  love.graphics.pop()
end

function Choice:updateGeometry(x, y, w, h)
  self.transform = love.math.newTransform():translate(x, y)
  self.w = w
  self.h = h
end

function Choice:update(dt) end

function Choice:checkIndex()
  if self.nowrap then
    self.index = math.clamp(self.index, 1, #self.values)
  else
    if self.index < 1 then
      self.index = #self.values
    end
    if self.index > #self.values then
      self.index = 1
    end
  end
end

function Choice:mousepressed(x, y, button, touch, presses)
  local dx, dy = self.transform:inverseTransformPoint(x, y)
  local hit = dx >= 0 and dx <= self.w and dy >= 0 and dy <= self.height

  if hit then
    self.pressed = true
    Button.pressed()
    local oldindex = self.index

    -- Test whether arrows are hit
    -- NOTE: don't care about arrows being disabled, checkIndex() will fix that.
    if dx <= self.h + 2 then
      self.index = self.index - 1
    elseif dx >= self.w - self.h - 2 then
      self.index = self.index + 1
    end

    self:checkIndex()
    if oldindex ~= self.index then
      self.onChange(self.values[self.index])
    end
  end
end

function Choice:setValue(value)
  log.debug("value:", value)
  for i, v in ipairs(self.values) do
    if v.value == value then
      log.debug("found index of ", value, i)
      self.index = i
    end
  end
end

function Choice:mousereleased(x, y, button, touch, presses)
  if self.pressed then
    self.pressed = false
    Button.released()
  end
end

return Choice
