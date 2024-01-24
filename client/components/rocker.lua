local triangles = require("lib.triangles")
local Button = require("lib.button")

local RockerButton = Class({ radius = 5, size = 30 })

function RockerButton:init(id, opts, x, y, width, height)
  opts = opts or {}
  self.id = id
  self.x, self.y, self.w, self.h = x, y, width, height
  self.direction = opts.direction or "UP"
  self.isPressed = false
end

function RockerButton:draw()
  if self.isPressed then
    love.graphics.setColor(Colors.green)
  else
    love.graphics.setColor(Colors.grey)
  end
  love.graphics.rectangle("fill", self.x, self.y, self.w, self.h, 5, 5)
  love.graphics.setColor(Colors.cyan)
  love.graphics.rectangle("line", self.x, self.y, self.w, self.h, 5, 5)

  -- direction indicator
  local points = triangles:fit(self.direction, self.x, self.y, self.w, self.h)
  love.graphics.setColor(Colors.white)
  love.graphics.polygon("fill", points.p1.x, points.p1.y, points.p2.x, points.p2.y, points.p3.x, points.p3.y)
end

function RockerButton:hit(x, y)
  return x >= self.x and x <= self.x + self.w and y >= self.y and y <= self.y + self.h
end

function RockerButton:pressed()
  Button.pressed()
  self.isPressed = true
  local message = { type = "icp-pressed", button = self.id }
  Signal.emit("send-to-server", message)
end

function RockerButton:released()
  Button.released()
  self.isPressed = false
  local message = { type = "icp-released", button = self.id }
  Signal.emit("send-to-server", message)
end

return RockerButton
