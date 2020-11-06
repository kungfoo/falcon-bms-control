local Mfd = require("components.mfd")
local Flup = require("lib.flup")

local leftMfd = Mfd("f16/left-mfd", 20, 30)
local rightMfd = Mfd("f16/right-mfd", 520, 30)

local mfds = Class {
  stats = {},
  components = {},
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
}

function mfds:init()
  self.components[leftMfd.id] = leftMfd
  self.components[rightMfd.id] = rightMfd
end

function mfds:enter(previous, switcher)
  self.components["switcher"] = switcher
  leftMfd:start()
  rightMfd:start()
end

function mfds:leave()
  leftMfd:stop()
  rightMfd:stop()
end

function mfds:update(dt)
  local t1 = love.timer.getTime()

  for _, component in pairs(self.components) do component:update(dt) end

  local t2 = love.timer.getTime()
  self.stats.time_update = (t2 - t1) * 1000
end

function mfds:handleReceive(event)
  local handler = self.channels[event.channel]
  if handler then handler(event) end
end

function mfds:draw()
  local t1 = love.timer.getTime()

  love.graphics.setColor(Colors.white)
  for _, component in pairs(self.components) do component:draw() end

  local t2 = love.timer.getTime()
  self.stats.time_draw = (t2 - t1) * 1000
end

function mfds:mousepressed(x, y, button, isTouch, presses)
  for _, component in pairs(self.components) do component:mousepressed(x, y, button, isTouch, presses) end
end

function mfds:mousereleased(x, y, button, isTouch, presses)
  for _, component in pairs(self.components) do component:mousereleased(x, y, button, isTouch, presses) end
end

return mfds
