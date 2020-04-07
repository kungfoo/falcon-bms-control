local msgpack = require("lib.msgpack")
local inspect = require("lib.inspect")

local Mfd = require("mfd")

local mfds = {
  debug = true,
  stats = {time_update = 0, time_draw = 0},
  components = {},
  channels = {
    -- general purpose reliable channel
    [0] = function(event)
      local payload = msgpack.unpack(event.data)
      print("Received general purpose event: ", inspect(payload))
    end,
    -- texture memory channels are unrealiable and pure image data.
    [1] = function(event)
      components["f16/left-mfd"]:consume(event.data)
    end,
    [2] = function(event)
      components["f16/right-mfd"]:consume(event.data)
    end,
    [3] = function(event)
      components["f16/ded"]:consume(event.data)
    end,
    [4] = function(event)
      components["f16/rwr"]:consume(event.data)
    end,
  },
}

function mfds:init()
  local leftMfd = Mfd("f16/left-mfd", 20, 30)
  local rightMfd = Mfd("f16/right-mfd", 520, 30)
  self.components[leftMfd.id] = leftMfd
  self.components[rightMfd.id] = rightMfd
end

function mfds:enter(previous, connection, switcher)
  self.connection = connection
  self.components["switcher"] = switcher
end

function mfds:update(dt)
  local t1 = love.timer.getTime()

  for _, component in pairs(self.components) do component:update(dt) end

  local success, event = pcall(self.connection.service)
  while success and event do
    if event.type == "disconnect" then
      print("Disconnected.")
      State.switch(connecting)
    elseif event.type == "receive" then
      self.channels[event.channel](event)
    end
    success, event = pcall(self.connection.service)
  end
  local t2 = love.timer.getTime()
  self.stats.time_update = (t2 - t1) * 1000
end

function mfds:draw()
  local t1 = love.timer.getTime()

  love.graphics.setColor(1, 1, 1)
  for _, component in pairs(self.components) do component:draw() end

  local t2 = love.timer.getTime()
  self.stats.time_draw = (t2 - t1) * 1000

  self:draw_debug_info()
end

function mfds:draw_debug_info()
  if self.debug then
    local fps = love.timer.getFPS()
    local mem = collectgarbage("count")
    local text = ("upd: %.2fms, drw: %.2fms, fps: %d, mem: %.2fMB, tex_mem: %.2fMB, ping: %.0fms"):format(
                   self.stats.time_update, self.stats.time_draw, fps, mem / 1024,
                   love.graphics.getStats().texturememory / 1024 / 1024, self.connection.server:round_trip_time())

    love.graphics.setColor(255, 255, 255)
    love.graphics.printf(text, 10, love.graphics.getHeight() - 20, love.graphics.getWidth(), "left")
  end
end

function mfds:leave()
  -- TODO: deregister MFD updates from server
end

function mfds:mousepressed(x, y, button, isTouch, presses)
  for _, component in pairs(self.components) do component:mousepressed(x, y, button, isTouch, presses) end
end

function mfds:mousereleased(x, y, button, isTouch, presses)
  for _, component in pairs(self.components) do component:mousereleased(x, y, button, isTouch, presses) end
end

return mfds
