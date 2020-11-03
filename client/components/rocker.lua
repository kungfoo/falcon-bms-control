local RockerButton = Class {radius = 5, sounds = Sounds.button, isPressed = false}

function RockerButton:init(opts)
  self.id = opts.id
  self.x, self.y, self.width, self.height = opts.x, opts.y, opts.width, opts.height
end

function RockerButton:draw()
  if self.isPressed then
    love.graphics.setColor(Colors.green)
  else
    love.graphics.setColor(Colors.white)
  end
  love.graphics.rectangle("line", self.x, self.y, self.width, self.height, self.radius)
  love.graphics.printf(self.id, self.x, self.y, self.height - 5)
end

function RockerButton:hit(x, y)
  return x >= self.x and x <= self.x + self.width and y >= self.y and y <= self.y + self.height
end

function RockerButton:pressed()
  self.sounds.pressed:play()
  self.isPressed = true
  local message = {type = "icp-pressed", button = self.id}
  Signal.emit("send-to-server", message)
end

function RockerButton:released()
  self.sounds.released:play()
  self.isPressed = false
  local message = {type = "icp-released", button = self.id}
  Signal.emit("send-to-server", message)
end

--

local Rocker = Class {padding = 25}

function Rocker:init(id, x, y, width, height)
  self.id = id
  self.x, self.y = x, y
  self.width, self.height = width, height
  local next = {id = "NEXT", x = x, y = y, width = width, height = (height - self.padding) / 2}

  local previous = {
    id = "PREVIOUS",
    x = x,
    y = y + height / 2 + self.padding / 2,
    width = width,
    height = (height - self.padding) / 2,
  }
  self.buttons = {RockerButton(next), RockerButton(previous)}
end

function Rocker:draw()
  for _, button in ipairs(self.buttons) do button:draw() end
end

function Rocker:update(dt)
end

function Rocker:hit(x, y)
  for _, button in ipairs(self.buttons) do
    if button:hit(x, y) then
      self.pressed = button
      button:pressed()
    end
  end
end

function Rocker:pressed()
  -- intentionally left blank
end

function Rocker:released()
  if self.pressed then
    self.pressed:released()
    self.pressed = nil
  end
end

return Rocker
