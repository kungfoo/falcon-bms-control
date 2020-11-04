local Switcher = Class {width = 150, height = 60, radius = 5, sounds = Sounds.button}

function Switcher:init(leftMargin, bottomMargin, states)
  self.margins = {leftMargin = leftMargin, bottomMargin = bottomMargin}
  self.states = states
  self.currentState = nil
  self.index = 0
end

function Switcher:switch()
  self.currentState = table.shift(self.states)
  table.push(self.states, self.currentState)
  State.switch(self.currentState, self)
  self.index = math.wrap(self.index + 1, 0, #self.states)
end

function Switcher:draw()
  if self.isPressed then
    love.graphics.setColor(Colors.green)
  else
    love.graphics.setColor(Colors.white)
  end
  love.graphics.rectangle("line", self.x, self.y, self.width, self.height, self.radius)

  love.graphics.setColor(Colors.green)

  local highlight = {
    width = (self.width / #self.states) - 20,
    height = self.height - 20,
    x = (self.x + 10) + (self.index * (self.width / #self.states)),
    y = self.y + 10,
  }
  love.graphics.rectangle("fill", highlight.x, highlight.y, highlight.width, highlight.height, self.radius)
end

function Switcher:update(dt)
  self.x, self.y = self.margins.leftMargin, love.graphics.getHeight() - self.margins.bottomMargin - self.height
end

function Switcher:pressed()
  self.isPressed = true
  self.sounds.pressed:play()
end

function Switcher:released()
  self.isPressed = false
  self.sounds.released:play()
  self:switch()
end

function Switcher:mousepressed(x, y, button, isTouch, presses)
  local hit = x >= self.x and x <= self.x + self.width and y >= self.y and y <= self.y + self.height
  if hit then self:pressed() end
end

function Switcher:mousereleased(x, y, button, isTouch, presses)
  if self.isPressed then self:released() end
end

return Switcher
