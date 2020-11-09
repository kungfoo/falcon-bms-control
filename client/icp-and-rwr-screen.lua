local Icp = require("components.icp")
local Ded = require("components.ded")

local StreamedTexture = require("util.streamed-texture")

local components = {}

local ded = Ded("f16/ded", 20, 30)
local icp = Icp("f16/icp", 20, 175)

local Screen = Class {
  components = {icp, ded},
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

  for _, component in pairs(self.components) do component:update(dt) end

  local t2 = love.timer.getTime()
  self.stats.time_update = (t2 - t1) * 1000
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
