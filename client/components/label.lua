local Component = Class({})

function Component:init(value, options)
  local p = options or {}
  self.value = value
  self.font = love.graphics.newFont("fonts/b612/B612Mono-Regular.ttf", p.size or 20, "normal")
  self.transform = love.math.newTransform()
end

function Component:draw()
  love.graphics.push()
  love.graphics.applyTransform(self.transform)

  love.graphics.setColor(Colors.white)
  love.graphics.printf(self.value, self.font, 0, 0, self.w)
  -- love.graphics.rectangle("line", 0,0, self.w, self.h)
  love.graphics.pop()
end

function Component:updateGeometry(x, y, w, h)
  self.transform = love.math.newTransform():translate(x, y)
  self.w = w
  self.h = h
  -- print(inspect({
  --   value = self.value,
  --   x = x,
  --   y =  y,
  --   w = self.w,
  --   h = self.h
  -- }))
end

function Component:update(dt) end

function Component:mousepressed(x, y, button, touch, presses) end

function Component:mousereleased(x, y, button, touch, presses) end

return Component
