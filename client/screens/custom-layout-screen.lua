local Flup = require("lib.flup")

local Screen = Class({
  stats = {},
  channels = {
    -- general purpose reliable channel
    [0] = function(event)
      local payload = msgpack.unpack(event.data)
      log.debug("Received general purpose event: ", inspect(payload))
    end,
  },
  dimensions = { w = 0, h = 0 },
  padding = 10,
})

function Screen:init(spec)
  if not spec then
    -- gets called a second time with nil once we enter the screen, :/
    return
  end
  self.name = spec.name

  if #spec.components > 1 then
    log.error("Creating a screen with more than one root component.")
  end
  self.root_component = table.shift(spec.components)
end

function Screen:enter(previous)
  self:each_component(function(c)
    if c.start then
      c:start()
    end
  end)
end

function Screen:leave()
  self:each_component(function(c)
    if c.stop then
      c:stop()
    end
  end)
end

-- invoke f() for each component in the tree
function Screen:each_component(f)
  self:_each_component(self.root_component, f)
end

function Screen:_each_component(c, f)
  if c.components then
    for _, child in pairs(c.components) do
      self:_each_component(child, f)
    end
  end

  f(c)
end

function Screen:update(dt)
  local t1 = love.timer.getTime()

  local x, y, w, h = love.window.getSafeArea()
  if self.dimensions.w ~= w or self.dimensions.h ~= h then
    self:adjustLayoutIfNeeded(w, h)
    self.flup:fill(x + self.padding, y + self.padding, w - self.padding * 2, h - self.padding * 2)
    self.dimensions.w = w
    self.dimensions.h = h
  end

  Footer:update(dt)
  self:each_component(function(c)
    if c.update then
      ok, error = pcall(function()
        c:update(dt)
      end)
      if not ok then
        log.error("Failed to update component", c, "because of", error)
      end
    end
  end)

  local t2 = love.timer.getTime()
  self.stats.time_update = (t2 - t1) * 1000
end

function Screen:adjustLayoutIfNeeded(w, h)
  self.flup = Flup.split({
    direction = "y",
    ratio = 0.95,
    margin = 10,
    components = {
      top = self.root_component,
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

  Footer:draw()
  self:each_component(function(c)
    if c.draw then
      c:draw()
    end
  end)

  local t2 = love.timer.getTime()
  self.stats.time_draw = (t2 - t1) * 1000
end

function Screen:mousepressed(x, y, button, isTouch, presses)
  Footer:mousepressed(x, y, button, isTouch, presses)
  self:each_component(function(c)
    if c.mousepressed then
      c:mousepressed(x, y, button, isTouch, presses)
    end
  end)
end

function Screen:mousereleased(x, y, button, isTouch, presses)
  Footer:mousereleased(x, y, button, isTouch, presses)
  self:each_component(function(c)
    if c.mousereleased then
      c:mousereleased(x, y, button, isTouch, presses)
    end
  end)
end

return Screen
