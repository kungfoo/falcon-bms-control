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

-- initialize settings straight away.
local settings = require("lib.settings")
Settings = settings()

Colors = require("lib.colors")
msgpack = require("lib.msgpack")
inspect = require("lib.inspect")

Connection = {ip = nil, server = nil, host = nil, peer = nil}

-- libraries
local socket = require("socket")
local enet = require("enet")
local tick = require("lib.tick")

-- connecting states
local broadcasting = {port = 9020}
local connecting = {port = 9022, channels = 255}

-- connected screen states
local mfd_screen = require("screens.mfd-screen")
local icp_and_rwr_screen = require("screens.icp-and-rwr-screen")
local settings_screen = require("screens.settings")

local shine = {dots = {".", "..", "..."}, position = 1}

local debug = {enabled = true, stats = {time_update = 0, time_draw = 0}}

local Switcher = require("components.switcher")
local switcher = Switcher({mfd_screen, icp_and_rwr_screen})

-- footer component is present on all screens
local footer = require("components.footer")
Footer = footer(switcher, settings_screen)

local font = love.graphics.newFont("fonts/b612/B612Mono-Regular.ttf", 20, "normal")

function love.load()
  tick.framerate = 60 -- Limit framerate to 60 frames per second.
  tick.rate = 0.02 -- 50 updates per second

  State.registerEvents()
  -- can bypass broadcast, if server ip is known
  -- State.switch(connecting, "127.0.0.1")
  State.switch(broadcasting)

  Signal.register("send-to-server", function(message)
    Connection.server:send(msgpack.pack(message))
  end)

  Timer.every(0.5, function()
    shine.position = ((shine.position + 1) % #shine.dots) + 1
  end)
end

function broadcasting:enter()
  broadcasting.socket = socket.udp4()
  broadcasting.socket:settimeout(0)
  broadcasting.socket:setoption("broadcast", true)
  broadcasting.send = Timer.every(1, self.sendHello)
  broadcasting.receive = Timer.every(1, self.receiveAck)
end

function broadcasting:sendHello()
  local message = msgpack.pack({type = "hello"})
  broadcasting.socket:sendto(message, "255.255.255.255", broadcasting.port)
end

function broadcasting:receiveAck()
  local datagram, ip, port, error = broadcasting.socket:receivefrom()
  if datagram and ip and port then
    local message = msgpack.unpack(datagram)
    if message.type == "ack" then
      print("Discovered server at [${ip}] on port [${port}]" % {ip = ip, port = port})
      State.switch(connecting, ip)
    end
  end
end

function broadcasting:leave()
  Timer.cancel(broadcasting.send)
  Timer.cancel(broadcasting.receive)
  broadcasting.socket:close()
end

function broadcasting:draw()
  love.graphics.setColor(Colors.white)
  love.graphics.setFont(font)
  love.graphics.print("DISCOVERING SERVER" .. shine.dots[shine.position], 30, 30)
end

function connecting:enter(previous, serverIp)
  Connection.ip = serverIp or connection.ip
  Connection.host = enet.host_create()
  Connection.server = Connection.host:connect(Connection.ip .. ":" .. connecting.port, connecting.channels)
end

function connecting:handleReceive(event)
end

function connecting:draw()
  love.graphics.print("CONNECTING" .. shine.dots[shine.position], 30, 30)
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
      State.switch(broadcasting)
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

function love.draw()
end

function love.keypressed(key, scancode, isrepeat)
  if key == "escape" then love.event.quit() end
end

function love.quit()
  print("Quitting client...")
  if Connection.peer then
    Connection.peer:disconnect()
    Connection.host:flush()
  end
end
