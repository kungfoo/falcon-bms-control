local socket = require("socket")
local enet = require("enet")

log = require("lib.log")
Timer = require("lib.hump.timer")
local msgpack = require("lib.msgpack")
local tick = require("lib.tick")
local inspect = require("lib.inspect")

local server = { host = nil, clients = {} }

local broadcast = { port = 9020 }

local connection = { port = 9022, max_peers = 10, channels = 255 }

local chunks_to_send = {}

local images = {
  ["f16/left-mfd"] = { channel = 1, data = love.filesystem.newFileData("images/left-mfd.jpeg") },
  ["f16/right-mfd"] = { channel = 2, data = love.filesystem.newFileData("images/right-mfd.jpeg") },
}

function love.load()
  tick.framerate = 60 -- Limit framerate to 60 frames per second.
  tick.rate = 0.016

  server.host = enet.host_create("*:" .. connection.port, connection.max_peers, connection.channels)

  broadcast.timer = Timer.every(1, receiveHello)
  Timer.every(0.3, sendChunks)
  broadcast.socket = socket.udp4()
  broadcast.socket:setsockname("*", broadcast.port)
  broadcast.socket:settimeout(0)
  log.info("Server is ready on broadcast port " .. broadcast.port .. " and connection port " .. connection.port)
end

function receiveHello()
  local datagram, ip, port, error = broadcast.socket:receivefrom()

  if not error and datagram then
    local message = msgpack.unpack(datagram)
    log.debug(inspect({ msg = message, from = ip, port = port }))
    if message.type == "hello" then
      local ack = msgpack.pack({ type = "ack" })
      broadcast.socket:sendto(ack, ip, port)
    end
  end
end

function sendChunks()
  for peer, identifiers in pairs(chunks_to_send) do
    for _, id in ipairs(identifiers) do
      local image = images[id]
      if image then
        peer:send(image.data:getString(), image.channel, "unreliable")
      end
    end
  end
end

function love.update(dt)
  Timer.update(dt)
  local success, event = pcall(server.service)
  while success and event do
    if event.type == "connect" then
      server.clients[event.peer] = event.peer:connect_id()
      log.info("Connected, clients are now:")
      log.info(inspect(server.clients))
    elseif event.type == "disconnect" then
      server.clients[event.peer] = nil
      chunks_to_send[event.peer] = nil
      log.info("Disconnected, clients are now:")
      log.info(inspect(server.clients))
    elseif event.type == "receive" then
      local payload = msgpack.unpack(event.data)
      log.info(inspect(payload))

      if string.match(payload.type, "osb-") then
        event.peer:send(msgpack.pack({ type = "ack", payload = payload }))
      end

      if string.match(payload.type, "icp-") then
        event.peer:send(msgpack.pack({ type = "ack", payload = payload }))
      end
      if payload.type == "streamed-texture" then
        if payload.command == "start" then
          if chunks_to_send[event.peer] == nil then
            chunks_to_send[event.peer] = { payload.identifier }
          else
            table.insert(chunks_to_send[event.peer], payload.identifier)
          end
        end

        if payload.command == "stop" then
          chunks_to_send[event.peer] = {}
        end
      end
    else
      log.error("Unhandled event type:", event.type)
    end
    success, event = pcall(server.service)
  end
end

function server:service()
  return server.host:service()
end

function love.quit()
  log.info("Quitting server...")
  for peer, id in pairs(server.clients) do
    peer:disconnect()
  end
  server.host:flush()
end
