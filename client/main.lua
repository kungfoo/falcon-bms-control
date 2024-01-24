-- a bunch of functions
require("lib.interpolate")
require("lib.core.table")
require("lib.core.math")

-- globals that are used all over the place.
Class = require("lib.hump.class")
Signal = require("lib.hump.signal")
State = require("lib.hump.gamestate")
Timer = require("lib.hump.timer")
Flup = require("lib.flup")
Connection = { ip = nil, server = nil, host = nil, peer = nil }

-- initialize settings straight away.
local settings = require("lib.settings")
Settings = settings()

Colors = require("lib.colors")
msgpack = require("lib.msgpack")
inspect = require("lib.inspect")

local tick = require("lib.tick")

-- screen states
local mfd_screen = require("screens.mfd-screen")
local icp_and_rwr_screen = require("screens.icp-and-rwr-screen")
local settings_screen = require("screens.settings-screen")
local connecting_screen = require("screens.connecting-screen")

local debug = { enabled = true, stats = { time_update = 0, time_draw = 0 } }

local Switcher = require("components.switcher")
local switcher = Switcher({ mfd_screen, icp_and_rwr_screen })

-- footer component is present on most screens
local footer = require("components.footer")
Footer = footer(switcher, settings_screen)

local font = love.graphics.newFont("fonts/b612/B612Mono-Regular.ttf", 20, "normal")

function love.load()
  tick.framerate = 60 -- Limit framerate to 60 frames per second.
  tick.rate = 0.02 -- 50 updates per second

  State.registerEvents()
  State.switch(connecting_screen, settings_screen)

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

  -- service enet host.
  local success, event = pcall(Connection.service)
  while success and event do
    if event.type == "disconnect" then
      print("Disconnected.")
      State.switch(connecting_screen, settings_screen)
    elseif event.type == "connect" then
      print("Connected ...")
      Connection.peer = event.peer
      -- switch to first screen
      switcher:switch(icp_and_rwr_screen)
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
  print("Quitting client...")
  if Connection.peer then
    Connection.peer:disconnect()
    Connection.host:flush()
  end
end
