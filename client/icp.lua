local msgpack = require("lib.msgpack")
local inspect = require("lib.inspect")

local icp = {components = {}}

function icp:init()
  local switcher = Switcher(50, 50, "icp")
  self.components["switcher"] = switcher
end

function icp:enter(previous, connection, switcher)
  self.connection = connection
  self.components["switcher"] = switcher
end

function icp:update(dt)
  local t1 = love.timer.getTime()

  for _, component in pairs(self.components) do component:update(dt) end

  local success, event = pcall(self.connection.service)
  while success and event do
    if event.type == "disconnect" then
      print("Disconnected.")
      State.switch(connecting)
    elseif event.type == "receive" then
      channels[event.channel](event)
    end
    success, event = pcall(self.connection.service)
  end
  local t2 = love.timer.getTime()
  stats.time_update = (t2 - t1) * 1000
end

function icp:draw()
  local t1 = love.timer.getTime()

  love.graphics.setColor(1, 1, 1)
  for _, component in pairs(self.components) do component:draw() end

  local t2 = love.timer.getTime()
  stats.time_draw = (t2 - t1) * 1000

  self:draw_debug_info()
end

function icp:leave()
  -- TODO: deregister DED updates from server

end

function icp:mousepressed(x, y, button, isTouch, presses)
  for _, component in pairs(self.components) do component:mousepressed(x, y, button, isTouch, presses) end
end

function icp:mousereleased(x, y, button, isTouch, presses)
  for _, component in pairs(self.components) do component:mousereleased(x, y, button, isTouch, presses) end
end

return icp
