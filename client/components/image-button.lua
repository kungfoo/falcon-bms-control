local Component = Class {}

local Button = require("lib.button")

function Component:init(icon, options, callback)
    local o = options or {}
    self.align = o.align or "left"
    self.callback = callback or function() end
    self.image = love.graphics.newImage(love.image.newImageData(icon), {mipmaps = true})
    self.transform = love.math.newTransform()
    self.pressed = false
end

function Component:draw()
    love.graphics.setColor(Colors.grey)
    if self.pressed then
        love.graphics.setColor(Colors.green)
    end
    love.graphics.draw(self.image, self.transform)
end

function Component:updateGeometry(x, y, w, h)
    local scale = self:determineScale(w, h)
    self.transform = love.math.newTransform()
    print(self.align)
    if self.align == "left" then
        self.transform:translate(x, y):scale(scale)
    end
    if self.align == "right" then
        self.transform:translate(x + w - self.image:getWidth()*scale, y + h - self.image:getHeight()*scale):scale(scale)
    end
    self.w = w
    self.h = h
end

function Component:determineScale(w, h)
  if w >= self.image:getWidth() and h >= self.image:getHeight() then
    -- do not scale up
    return 1.0
  else
    local a, b = w / self.image:getWidth(), h / self.image:getHeight()
    return math.min(a, b)
  end
end

function Component:update(dt)
end

function Component:mousepressed(x, y, button, touch, presses)
    local dx, dy = self.transform:inverseTransformPoint(x,y)
    if dx >= 0 and dx <= self.image:getWidth() and dy >= 0 and dy <= self.image:getHeight() then
        Button.pressed()
        self.pressed = true
    end
end

function Component:mousereleased(x, y, button, touch, presses)
    if self.pressed then
        Button.released()
        self.pressed = false
        self.callback()
    end
end

return Component