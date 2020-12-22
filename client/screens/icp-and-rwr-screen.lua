local Icp = require("components.icp")
local Ded = require("components.ded")
local Slider = require("components.slider")

local StreamedTexture = require("util.streamed-texture")

local components = {}

local ded = Ded("f16/ded")
local icp = Icp("f16/icp")
local slider = Slider(20, 15, 60, function(v)
  print(v)
end, {knob = 'circle'})

local Screen = Class {
  components = {icp, ded, slider},
  stats = {},
  channels = {
    -- general purpose reliable channel
    [0] = function(event)
      local payload = msgpack.unpack(event.data)
      print("Received general purpose event: ", inspect(payload))
    end,
    -- texture memory channels are unrealiable and pure image data.
    [3] = function(event)
      ded:consume(event.data)
    end,
    [4] = function(event)
      components["f16/rwr"]:consume(event.data)
    end,
  },
  dimensions = {w = 0, h = 0},
  padding = 10,
}

function Screen:init()
end

function Screen:enter(previous, switcher)
  self.components["switcher"] = switcher
  ded:start()
end

function Screen:leave()
  ded:stop()
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

  for _, component in pairs(self.components) do component:update(dt) end

  local t2 = love.timer.getTime()
  self.stats.time_update = (t2 - t1) * 1000
end

function Screen:adjustLayoutIfNeeded(w, h)
  -- currently nothing to display besides these two
  self.flup = Flup.split {
    direction = "y",
    ratio = 0.95,
    components = {
      top = Flup.split {
        direction = "x",
        ratio = 0.6,
        components = {
          left = Flup.split {direction = "y", ratio = 0.3, components = {top = ded, bottom = icp}},
          right = slider,
        },
      },
      bottom = self.components["switcher"],
    },
  }
end

function Screen:handleReceive(event)
  local handler = self.channels[event.channel]
  if handler then handler(event) end
end

function Screen:draw()
  local t1 = love.timer.getTime()

  for _, component in pairs(self.components) do component:draw() end

  local t2 = love.timer.getTime()
  self.stats.time_draw = (t2 - t1) * 1000
end

function Screen:mousepressed(x, y, button, isTouch, presses)
  for _, component in pairs(self.components) do component:mousepressed(x, y, button, isTouch, presses) end
end

function Screen:mousereleased(x, y, button, isTouch, presses)
  for _, component in pairs(self.components) do component:mousereleased(x, y, button, isTouch, presses) end
end

return Screen
