-- globals that are used all over the place.
Class = require("lib.hump.class")
Signal = require("lib.hump.signal")
State = require("lib.hump.gamestate")
Timer = require("lib.hump.timer")

Colors = require("lib.colors")
Sounds = require("lib.sounds")
inspect = require("lib.inspect")

require("lib.interpolate")
require("lib.core.table")
require("lib.core.math")

-- libraries
local socket = require("socket")
local enet = require("enet")
local msgpack = require("lib.msgpack")
local tick = require("lib.tick")

-- connecting states
local broadcasting = {port = 9020}
local connecting = {port = 9022}

-- connected screen states
local mfds = require("mfds")
local icp = require("icp")

-- data
local connection = {ip = nil, server = nil, host = nil, peer = nil}
local shine = {dots = {".", "..", "..."}, position = 1}

local debug = {enabled = true, stats = {time_update = 0, time_draw = 0}}

-- switcher component is present on all screens
local Switcher = require("components.switcher")
local switcher = Switcher(20, 30, {mfds, icp})

local font = love.graphics.newFont("fonts/b612/B612Mono-Regular.ttf", 20, "normal")

function love.load()
  tick.framerate = 60 -- Limit framerate to 60 frames per second.
  tick.rate = 0.02 -- 50 updates per second

  State.registerEvents()
  -- can bypass broadcast, if server ip is known
  -- State.switch(connecting, "127.0.0.1")
  State.switch(broadcasting)

  Signal.register("send-to-server", function(message)
    connection.server:send(msgpack.pack(message))
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
  love.graphics.print("DISCOVERING SERVER" .. shine.dots[shine.position], 30, 30)
end

function connecting:enter(previous, serverIp)
  connection.ip = serverIp or connection.ip
  connection.host = enet.host_create()
  connection.server = connection.host:connect(connection.ip .. ":" .. connecting.port, 255)
end

function connecting:handleReceive(event)
end

function connecting:draw()
  love.graphics.print("CONNECTING" .. shine.dots[shine.position], 30, 30)
end

function connection:service()
  return connection.host:service()
end

function love.update(dt)
  -- always update timers
  Timer.update(dt)

  -- service enet host.
  local success, event = pcall(connection.service)
  while success and event do
    if event.type == "disconnect" then
      print("Disconnected.")
      State.switch(connecting)
    elseif event.type == "connect" then
      print("Connected ...")
      connection.peer = event.peer
      -- switch to first screen
      switcher:switch()
    elseif event.type == "receive" then
      State.current():handleReceive(event)
    end
    success, event = pcall(connection.service)
  end
end

function love.draw()
  if debug.enabled then
    love.graphics.setFont(font)
    local fps = love.timer.getFPS()
    local mem = collectgarbage("count")

    local ping = 0
    if connection.server then ping = connection.server:round_trip_time() end

    local text = ("upd: %2.2fMS, drw: %2.2fMS, fps: %d, mem: %2.2fMB, tex_mem: %2.2fMB, ping: %dMS"):format(
                   debug.stats.time_update, debug.stats.time_draw, fps, mem / 1024,
                   love.graphics.getStats().texturememory / 1024 / 1024, ping)

    love.graphics.setColor(Colors.white)
    love.graphics.printf(text, 10, love.graphics.getHeight() - 26, love.graphics.getWidth(), "left")
  end
end

function love.keypressed(key, scancode, isrepeat)
  if key == "escape" then love.event.quit() end
end

function love.quit()
  print("Quitting client...")
  if connection.peer then
    connection.peer:disconnect()
    connection.host:flush()
  end
end
