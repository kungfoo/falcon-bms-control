-- for things that should be development only
function isDevelopment()
  return os.getenv("RUN_MODE") == "development"
end

-- a bunch of functions
log = require("lib.log")
require("lib.interpolate")
require("lib.core.table")
require("lib.core.math")
require("lib.core.functional")

if not isDevelopment() then
  log.level = "info"
  log.usecolor = false
end

-- globals that are used all over the place.
Class = require("lib.hump.class")
Signal = require("lib.hump.signal")
State = require("lib.hump.gamestate")
Timer = require("lib.hump.timer")
Flup = require("lib.flup")
Connection = { ip = nil, server = nil, host = nil, peer = nil }

-- initialize settings straight away.
local settings = require("lib.settings")
-- load settings
Settings = settings()

Colors = require("lib.colors")
msgpack = require("lib.msgpack")
inspect = require("lib.inspect")

local lovebird = {}

-- fix your simulation time step
local tick = require("lib.tick")

-- load predefined layouts
Layouts = require("lib.layouts")()
local ScreenParser = require("lib.screen-parser")
-- component regisrty for predefined and custom layouts
local ComponentRegistry = require("lib.component-registry")

-- switcher component
local Switcher = require("components.switcher")

-- footer component is present on most screens
local footer = require("components.footer")

-- screen states have to be loaded globally
settings_screen = require("screens.settings-screen")
connecting_screen = require("screens.connecting-screen")

-- this will hold custom screens later
custom_screens = {}

function love.load()
  if isDevelopment() then
    -- todo: hot reloading here...
    lovebird = require("lib.development.lovebird")
  end

  local registry = ComponentRegistry()
  local screen_parser = ScreenParser(registry)

  local switcher = Switcher(custom_screens)

  Footer = footer(switcher, settings_screen)

  tick.framerate = 60 -- Limit framerate to 60 frames per second.
  tick.rate = 0.02 -- 50 updates per second

  State.registerEvents()
  State.switch(connecting_screen)

  Signal.register("send-to-server", function(message)
    Connection.server:send(msgpack.pack(message))
  end)

  Signal.register("settings-changed", function(_)
    log.debug("Settings changed, reconnecting...")
    State.switch(connecting_screen)
  end)

  Signal.register("layout-changed", function(_)
    log.debug("Updating layout to:", Settings:layout())
    local layout = Layouts:find(Settings:layout()) or Layouts:find("default-landscape")

    while #custom_screens > 0 do
      -- hack, since we can't simply clear the table
      table.pop(custom_screens)
    end
    local from_layout = screen_parser:createScreensForLayout(layout)
    table.foreach(from_layout, function(screen)
      table.push(custom_screens, screen)
    end)
  end)

  -- trigger layout loading once
  Signal.emit("layout-changed")
end

function Connection:service()
  return Connection.host:service()
end

function love.update(dt)
  -- always update timers
  Timer.update(dt)

  if isDevelopment() then
    -- todo: development hot reloading
    lovebird.update()
  end

  -- service enet host.
  local success, event = pcall(Connection.service)
  if success then
    State.current():handleReceive(event)
  end
end

function love.draw() end

function love.keypressed(key, scancode, isrepeat)
  if key == "escape" then
    love.event.quit()
  end
end

function love.quit()
  log.info("Quitting client...")
  if Connection.peer then
    Connection.peer:disconnect()
    Connection.host:flush()
  end
end
