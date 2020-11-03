Timer = require("lib.hump.timer")

local socket = require("socket")
local msgpack = require("msgpack")
local enet = require("enet")
local tick = require("tick")
local inspect = require("inspect")

local data = {image = nil}

local chunk = {x = 10, y = 10, width = 300, height = 300}

local server = {host = nil, clients = {}}

local broadcast = {port = 9020}

local connection = {port = 9022}

function chunk:extractImage(image)
    local cropped = love.image.newImageData(self.width, self.height)
    cropped:paste(image, 0, 0, self.x, self.y, self.width, self.height)
    return cropped
end

function chunk:update() end

function send_chunk(peer, message)
    print("Sending image chunk")
    chunk.x = message.position.x
    chunk.y = message.position.y
    local binary = chunk:extractImage(data.image):encode('png')

    local payload = {type = "image-payload", data = binary:getString()}
    peer:send(msgpack.pack(payload), 0, "unreliable")
end

function love.load()
    tick.framerate = 60 -- Limit framerate to 60 frames per second.
    tick.rate = 0.016

    server.host = enet.host_create("*:" .. connection.port)

    broadcast.timer = Timer.every(1, receiveHello)
    broadcast.socket = socket.udp4()
    broadcast.socket:setsockname("*", broadcast.port)
    broadcast.socket:settimeout(0)
    print("Server is ready on broadcast port " .. broadcast.port ..
              " and connection port " .. connection.port)
end

function receiveHello()
    local datagram, ip, port, error = broadcast.socket:receivefrom()

    if not error and datagram then
        local message = msgpack.unpack(datagram)
        print(inspect({msg = message, from = ip, port = port}))
        if message.type == "hello" then
            local ack = msgpack.pack({type = "ack"})
            broadcast.socket:sendto(ack, ip, port)
        end
    end
end

function love.update(dt)
    Timer.update(dt)
    local success, event = pcall(server.service)
    while success and event do
        if event.type == "connect" then
            server.clients[event.peer] = event.peer:connect_id()
            print("Connected, clients are now:")
            print(inspect(server.clients))
        elseif event.type == "disconnect" then
            server.clients[event.peer] = nil
            print("Disconnected, clients are now:")
            print(inspect(server.clients))
        elseif event.type == "receive" then
            local payload = msgpack.unpack(event.data)

            if string.match(payload.type, "osb") then
                print(inspect(payload))
                event.peer:send(msgpack.pack({type = "ack", payload = payload}))
            end

            if string.match(payload.type, "icp") then
                print(inspect(payload))
                event.peer:send(msgpack.pack({type = "ack", payload = payload}))
            end

        else
            print("Not handled: ", event.type)
        end
        success, event = pcall(server.service)
    end
end

function server:service() return server.host:service() end

function love.quit()
    print("Quitting server...")
    for peer, id in pairs(server.clients) do peer:disconnect() end
    server.host:flush()
end
