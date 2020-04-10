local msgpack = require("lib.msgpack")
local inspect = require("lib.inspect")

local Icp = require("components.icp")

local icp = {components = {}, stats = {}}

function icp:init()
  self.components["icp"] = Icp("f16/icp", 20, 30)
end

function icp:enter(previous, switcher)
  self.components["switcher"] = switcher
end

function icp:update(dt)
  local t1 = love.timer.getTime()

  for _, component in pairs(self.components) do component:update(dt) end

  local t2 = love.timer.getTime()
  self.stats.time_update = (t2 - t1) * 1000
end

function icp:handleReceive(event)
  -- TODO: handle callback channels and data channels here.
end

function icp:draw()
  local t1 = love.timer.getTime()

  love.graphics.setColor(1, 1, 1)
  for _, component in pairs(self.components) do component:draw() end

  local t2 = love.timer.getTime()
  self.stats.time_draw = (t2 - t1) * 1000
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
