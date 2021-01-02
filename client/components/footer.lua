local Footer = Class {}

function Footer:init(switcher, settings_screen)
    self.switcher = switcher
    self.settings_screen = settings_screen
    self.transform = love.math.newTransform()
end

function Footer:update(dt)
end

function Footer:draw()
    love.graphics.push()
    love.graphics.applyTransform(self.transform)
    self.switcher:draw()
    love.graphics.pop()
end


function Footer:updateGeometry(x, y, w, h)
    self.switcher:updateGeometry(x,y,w,h)
end

function Footer:mousepressed(x, y, button, isTouch, presses)
    self.switcher:mousepressed(x,y,button, isTouch, presses)
end

function Footer:mousereleased(x, y, button, isTouch, presses)
    self.switcher:mousereleased(x,y,button, isTouch, presses)
end

return Footer