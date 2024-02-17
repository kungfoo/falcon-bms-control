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

-- fix your simulation time step
local tick = require("lib.tick")

-- load predefined layouts
local Layouts = require("lib.layouts")
local ScreenParser = require("lib.screen-parser")
-- component regisrty for predefined and custom layouts
local ComponentRegistry = require("lib.component-registry")

-- screen states
local settings_screen = require("screens.settings-screen")
local connecting_screen = require("screens.connecting-screen")
local CustomLayoutScreen = require("screens.custom-layout-screen")

local debug = { enabled = true, stats = { time_update = 0, time_draw = 0 } }

local Switcher = require("components.switcher")

-- footer component is present on most screens
local footer = require("components.footer")

local font = love.graphics.newFont("fonts/b612/B612Mono-Regular.ttf", 20, "normal")

function love.load()
  if isDevelopment() then
    -- todo: hot reloading here...
    lovebird = require("lib.development.lovebird")
  end

  layouts = Layouts()
  registry = ComponentRegistry()
  screen_parser = ScreenParser(registry)
  -- tbd: replace with layout from settings
  layout = layouts:find("default-landscape")
  screens_from_layout = screen_parser:createScreens(layout.definition.screens)

  local custom_screens = table.map(screens_from_layout, function(spec)
    log.debug("Creating a screen for", spec.name)
    return CustomLayoutScreen(spec)
  end)

  switcher = Switcher(custom_screens)

  Footer = footer(switcher, settings_screen)

  tick.framerate = 60 -- Limit framerate to 60 frames per second.
  tick.rate = 0.02 -- 50 updates per second

  State.registerEvents()
  State.switch(connecting_screen, settings_screen, custom_screens[1])

  Signal.register("send-to-server", function(message)
    Connection.server:send(msgpack.pack(message))
  end)
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
  while success and event do
    if event.type == "disconnect" then
      log.info("Disconnected.")
      State.switch(connecting_screen, settings_screen)
    elseif event.type == "connect" then
      log.info("Connected ...")
      Connection.peer = event.peer
      -- switch to first screen
      switcher:switch()
    elseif event.type == "receive" then
      State.current():handleReceive(event)
    end
    success, event = pcall(Connection.service)
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
