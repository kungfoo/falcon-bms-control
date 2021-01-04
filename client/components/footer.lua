local Footer = Class {}

local ImageButton = require("components.image-button")

function Footer:init(switcher, settings_screen)
  self.switcher = switcher
  self.settings_button = ImageButton("icons/settings.png", {align = "right"}, function()
    State.switch(settings_screen)
  end)
  self.transform = love.math.newTransform()

  self.flup = Flup.split {direction = "x", components = {left = self.switcher, right = self.settings_button}}
end

function Footer:update(dt)
end

function Footer:draw()
  love.graphics.push()
  love.graphics.applyTransform(self.transform)

  self.switcher:draw()
  self.settings_button:draw()

  love.graphics.pop()
end

function Footer:updateGeometry(x, y, w, h)
  self.flup:fill(x, y, w, h)
end

function Footer:mousepressed(x, y, button, isTouch, presses)
  self.switcher:mousepressed(x, y, button, isTouch, presses)
  self.settings_button:mousepressed(x, y, button, isTouch, presses)
end

function Footer:mousereleased(x, y, button, isTouch, presses)
  self.switcher:mousereleased(x, y, button, isTouch, presses)
  self.settings_button:mousereleased(x, y, button, isTouch, presses)
end

return Footer
