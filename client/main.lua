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
local suit = require("lib.suit")

-- connection states
local broadcasting = {
	port = 9020
}

local connecting = {
	port = 9022
}

local connected = {
	stats = {
		time_update = 0,
		time_draw = 0
	}
}

local Mfd = require("mfd")

local connection = {
	ip = nil,
	server = nil,
	host = nil,
	peer = nil
}

local shine = {
	dots = { ".", "..", "..." },
	position = 1
}

local state = {
	debug = true
}

local components = {}

function love.load()
	love.graphics.setFont(love.graphics.newFont("fonts/falconded.ttf", 20, 'normal'))
	tick.framerate = 60 -- Limit framerate to 60 frames per second.
	tick.rate = 0.016

	State.registerEvents()
	State.switch(broadcasting)

	Signal.register("send-to-server", function(message)
		print("Sending to server:\n" .. inspect(message))
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
	local message = msgpack.pack({
		type = "hello"
	})
	broadcasting.socket:sendto(message, "255.255.255.255", broadcasting.port)
end

function broadcasting:receiveAck()
	local datagram, ip, port, error = broadcasting.socket:receivefrom()
	if datagram and ip and port then
		local message = msgpack.unpack(datagram)
		if message.type == "ack" then
			print("Discovered server at [${ip}] on port [${port}]" % { ip = ip, port = port })
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
	connection.server = connection.host:connect(connection.ip .. ":" .. connecting.port)
end

function connecting:update(dt)
	local event = connection.host:service()
	while event do
		if event.type == "connect" then
			print("Connected ...")
			connection.peer = event.peer
			State.switch(connected)
		end
		event = connection.host:service()
	end
end

function connecting:draw()
	love.graphics.print("CONNECTING" .. shine.dots[shine.position], 30, 30)
end

function connected:init()
	local leftMfd = Mfd("f16/left-mfd", 20, 30)
	local rightMfd = Mfd("f16/right-mfd", 520, 30)
	table.insert(components, leftMfd)
	table.insert(components, rightMfd)
end

function connected:update(dt)
	local t1 = love.timer.getTime()
	local event = connection.host:service()
	while event do
        if event.type == "disconnect" then
			print("Disconnected.")
			State.switch(connecting)
		elseif event.type == "receive" then
			local payload = msgpack.unpack(event.data)
			if payload.type == "image-payload" then
				local bytes = love.data.newByteData(payload.data)
				data.image = love.graphics.newImage(love.image.newImageData(bytes))

			else
				print("Received: ", inspect(payload))
			end
        else
            print("Not handled: ", event.type)
        end
		event = connection.host:service()
	end
	local t2 = love.timer.getTime()
	self.stats.time_update = (t2-t1) * 1000
end

function connected:draw()
	local t1 = love.timer.getTime()

	love.graphics.setColor(1,1,1)
	for _,component in ipairs(components) do
		component:draw()
	end

	local t2 = love.timer.getTime()
	self.stats.time_draw = (t2-t1) * 1000

	if state.debug then
		self:draw_debug_info()
	end
end

function connected:draw_debug_info()
	if state.debug then
		local fps = love.timer.getFPS()
		local mem = collectgarbage("count")
        local text = ("upd: %.2fms, drw: %.2fms, fps: %d, mem: %.2fMB, tex_mem: %.2f MB"):format(stats.time_update, stats.time_draw, fps, mem / 1024, love_graphics.getStats().texturememory / 1024 / 1024)
        
		love.graphics.setColor(255, 255, 255)
		love.graphics.printf(text, 10, love.graphics.getHeight() - 20, love.graphics.getWidth(), "left")
	end
end

function connected:leave()
	connection.peer:disconnect()
	connection.host:flush()
end

function connected:mousepressed(x, y, button, isTouch, presses)
	for _,component in ipairs(components) do
		component:mousepressed(x, y, button, isTouch, presses)
	end
end

function connected:mousereleased(x, y, button, isTouch, presses)
	for _,component in ipairs(components) do
		component:mousereleased(x, y, button, isTouch, presses)
	end
end

function love.update(dt)
	-- always update timers
	Timer.update(dt)
end

function love.keypressed(key, scancode, isrepeat)
  if key == "escape" then
	love.event.quit()
  end
end

function love.quit()
	print("Quitting client...")
	if connection.peer then
		connection.peer:disconnect()
		connection.host:flush()
	end
end