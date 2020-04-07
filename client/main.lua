-- globals that are used all over the place.
Class = require("lib.hump.class")
Signal = require("lib.hump.signal")
State = require("lib.hump.gamestate")
Timer = require("lib.hump.timer")
require("lib.interpolate")

-- libraries
local socket = require("socket")
local enet = require("enet")
local msgpack = require("lib.msgpack")
local tick = require("lib.tick")
local inspect = require("lib.inspect")

-- connecting states
local broadcasting = {port = 9020}
local connecting = {port = 9022}

-- connected screen states
local mfds = require("mfds")
local icp = require("icp")

-- data
local connection = {ip = nil, server = nil, host = nil, peer = nil}
local shine = {dots = {".", "..", "..."}, position = 1}

function love.load()
  love.graphics.setFont(love.graphics.newFont("fonts/falconded.ttf", 20, "normal"))
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

function connecting:update(dt)
  local success, event = pcall(connection.service)
  while success and event do
    if event.type == "connect" then
      print("Connected ...")
      connection.peer = event.peer
      print(inspect(connection))
      State.switch(mfds, connection)
    end
    success, event = pcall(connection.service)
  end
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
