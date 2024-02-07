local Flup = require("lib.flup")

local Screen = Class({
  stats = {},
  channels = {
    -- general purpose reliable channel
    [0] = function(event)
      local payload = msgpack.unpack(event.data)
      print("Received general purpose event: ", inspect(payload))
    end,
  },
  dimensions = { w = 0, h = 0 },
  padding = 10,
})

local function parseComponentTree(components)
  print(inspect(components[1]))
end

function Screen:init(definition)
  print("Initializing custom layout screen")
  self.name = definition.name

  -- todo: build flup/component tree using ComponentRegistry
  self.componentRoot = parseComponentTree(definition.components)

  self.components = { leftMfd, rightMfd, Footer }
end

function Screen:enter(previous)
  leftMfd:start()
  rightMfd:start()
end

function Screen:leave()
  leftMfd:stop()
  rightMfd:stop()
end

function Screen:update(dt)
  local t1 = love.timer.getTime()

  local w, h = love.graphics.getDimensions()
  if self.dimensions.w ~= w or self.dimensions.h ~= h then
    self:adjustLayoutIfNeeded(w, h)
    self.flup:fill(self.padding, self.padding, w - self.padding * 2, h - self.padding * 2)
    self.dimensions.w = w
    self.dimensions.h = h
  end

  for _, component in ipairs(self.components) do
    component:update(dt)
  end

  local t2 = love.timer.getTime()
  self.stats.time_update = (t2 - t1) * 1000
end

function Screen:adjustLayoutIfNeeded(w, h)
  self.flup = Flup.split({
    direction = "y",
    ratio = 0.95,
    margin = 10,
    components = {
      top = componentRoot,
      bottom = Footer,
    },
  })
end

function Screen:handleReceive(event)
  local handler = self.channels[event.channel]
  if handler then
    handler(event)
  end
end

function Screen:draw()
  local t1 = love.timer.getTime()

  love.graphics.setColor(Colors.white)
  for _, component in pairs(self.components) do
    component:draw()
  end

  local t2 = love.timer.getTime()
  self.stats.time_draw = (t2 - t1) * 1000
end

function Screen:mousepressed(x, y, button, isTouch, presses)
  for _, component in ipairs(self.components) do
    component:mousepressed(x, y, button, isTouch, presses)
  end
end

function Screen:mousereleased(x, y, button, isTouch, presses)
  for _, component in ipairs(self.components) do
    component:mousereleased(x, y, button, isTouch, presses)
  end
end

return Screen
