local Switcher = Class {
  width = 150,
  height = 60,
  radius = 5,
  currentScreen = "mfds",
  sounds = {
    pressed = love.audio.newSource("sounds/A/button-pressed.ogg", "static"),
    released = love.audio.newSource("sounds/A/button-released.ogg", "static"),
  },
}

function Switcher:init(leftMargin, bottomMargin, currentScreen)
  self.margins = {leftMargin = leftMargin, bottomMargin = bottomMargin}
  self.currentScreen = currentScreen
end

function Switcher:draw()
  if self.isPressed then
    love.graphics.setColor(0.3, 0.6, 0.3)
  else
    love.graphics.setColor(1, 1, 1)
  end
  love.graphics.rectangle("line", self.x, self.y, self.width, self.height, self.radius)

  local highlight = {width = (self.width / 2) - 20, height = self.height - 20, x = self.x + 10, y = self.y + 10}
  if self.currentScreen == "icp" then highlight.x = highlight.x + (self.width / 2) end
  love.graphics.setColor(0.3, 0.6, 0.3)
  love.graphics.rectangle("fill", highlight.x, highlight.y, highlight.width, highlight.height, self.radius)

  love.graphics.setColor(1, 1, 1)
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
  self:switchToNextScreen()
end

function Switcher:switchToNextScreen()
  if self.currentScreen == "mfds" then
    self.currentScreen = "icp"
  else
    self.currentScreen = "mfds"
  end
end

function Switcher:mousepressed(x, y, button, isTouch, presses)
  local hit = x >= self.x and x <= self.x + self.width and y >= self.y and y <= self.y + self.height
  if hit then self:pressed() end
end

function Switcher:mousereleased(x, y, button, isTouch, presses)
  if self.isPressed then self:released() end
end

return Switcher
