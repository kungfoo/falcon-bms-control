local Button = require("lib.button")

local Switcher = Class {width = 150, height = 60, radius = 5, button = Button(), id = "switcher"}

function Switcher:init(states)
  self.states = states
  self.currentState = nil
  self.index = 0
  self.transform = love.math.newTransform()
end

function Switcher:switch(state)
  if state then
    self.currentState = state
    table.remove_value(self.states, state)
    table.push(self.states, state)
  else
    self.currentState = table.shift(self.states)
    table.push(self.states, self.currentState)
  end
  State.switch(self.currentState, self)
  self.index = math.wrap(self.index + 1, 0, #self.states)
end

function Switcher:update(dt)
end

function Switcher:draw()
  love.graphics.push()
  love.graphics.applyTransform(self.transform)

  if self.isPressed then
    love.graphics.setColor(Colors.green)
  else
    love.graphics.setColor(Colors.white)
  end
  love.graphics.rectangle("line", 0, 0, self.width, self.height, self.radius)

  love.graphics.setColor(Colors.green)

  local highlight = {
    width = (self.width / #self.states) - 20,
    height = self.height - 20,
    x = (10) + (self.index * (self.width / #self.states)),
    y = 10,
  }
  love.graphics.rectangle("fill", highlight.x, highlight.y, highlight.width, highlight.height, self.radius)
  love.graphics.pop()
end

function Switcher:updateGeometry(x, y, w, h)
  local scale = self:determineScale(w, h)
  self.transform = love.math.newTransform()
  self.transform:translate(x, y):scale(scale)
end

function Switcher:determineScale(w, h)
  if w >= Switcher.width and h >= Switcher.height then
    -- do not scale up
    return 1.0
  else
    local a, b = w / Switcher.width, h / Switcher.height
    return math.min(a, b)
  end
end

function Switcher:pressed()
  self.isPressed = true
  Button.pressed()
end

function Switcher:released()
  self.isPressed = false
  Button.released()
  self:switch()
end

function Switcher:mousepressed(x, y, button, isTouch, presses)
  local dx, dy = self.transform:inverseTransformPoint(x, y)
  local hit = dx >= 0 and dx <= self.width and dy >= 0 and dy <= self.height
  if hit then self:pressed() end
end

function Switcher:mousereleased(x, y, button, isTouch, presses)
  if self.isPressed then self:released() end
end

return Switcher
