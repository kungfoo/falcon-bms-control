local Mfd = require("components.mfd")
local Flup = require("lib.flup")

local leftMfd = Mfd("f16/left-mfd")
local rightMfd = Mfd("f16/right-mfd")

local Screen = Class({
  stats = {},
  channels = {
    -- general purpose reliable channel
    [0] = function(event)
      local payload = msgpack.unpack(event.data)
      print("Received general purpose event: ", inspect(payload))
    end,
    -- texture memory channels are unrealiable and pure image data.
    [1] = function(event)
      leftMfd:consume(event.data)
    end,
    [2] = function(event)
      rightMfd:consume(event.data)
    end,
  },
  dimensions = { w = 0, h = 0 },
  padding = 10,
})

function Screen:init()
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
  if w >= h then
    self.flup = Flup.split({
      direction = "y",
      ratio = 0.95,
      margin = 10,
      components = {
        top = Flup.split({ direction = "x", margin = 20, components = { left = leftMfd, right = rightMfd } }),
        bottom = Footer,
      },
    })
  else
    self.flup = Flup.split({
      direction = "y",
      ratio = 0.95,
      margin = 10,
      components = {
        top = Flup.split({ direction = "y", margin = 20, components = { top = leftMfd, bottom = rightMfd } }),
        bottom = Footer,
      },
    })
  end
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
