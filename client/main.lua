
require("util.proxy")

local tick = require("lib.tick.tick")
local Color = require("util.colors")
local enet = require("enet")

local love_graphics = love.graphics
local love_timer = love.timer

local state = {
    debug = true
}

local stats = {
    time_update = 0,
	time_draw = 0
}

local connection = {
    connected = false
}

local data = {}

function love.load()
    tick.framerate = 60
    tick.rate = 0.016

    connection.host = enet.host_create()
    connection.server = connection.host:connect("127.0.0.1:6789")
    connection.host:compress_with_range_coder()
end

function love.draw()
    local t1 = love_timer.getTime()
    draw_debug_info()

    love_graphics.setFont(Font[20])

    love_graphics.printf("Universal MFD/ICP", 10, 10, love_graphics.getWidth(), "left")

    if data.left_mfd then
        local image = love.graphics.newImage(data.left_mfd)
        love_graphics.draw(image, 10, 40)
    end

    if data.right_mfd then
        local image = love.graphics.newImage(data.right_mfd)
        love_graphics.draw(image, 500, 40)
    end

    local t2 = love_timer.getTime()
	stats.time_update = (t2-t1) * 1000
end

function love.update(dt)
    local t1 = love_timer.getTime()

    if connection.connected then
        request_data()
    else
        connect()
    end

    local t2 = love_timer.getTime()
	stats.time_update = (t2-t1) * 1000
end

function connect()
    if not connection.connected then
        local event = connection.host:service()
        if event and event.type == "connect" then
            print("Connected to peer:", event.peer)
            connection.peer = event.peer
            connection.connected = true
        elseif event then
            print("connection event: ", event.type)
        end
    end
end

function disconnect(event)
    print(event.peer, "disconnected.")

    connection.server:disconnect()
    connection.host:flush()
    connection.connected = false
end

function request_data()
    -- request left mfd for now
    connection.peer:send("f16/left-mfd", 0)
    -- connection.peer:send("f16/right-mfd")
    local event = connection.host:service()
    while event do
        if event.type == "receive" then
            local bytes = love.data.newByteData(event.data)
            data.left_mfd = love.image.newImageData(bytes)
        elseif event.type == "disconnect" then
            disconnect(event)
        else
            print("Unmtached event type: ", event.type)
        end
        event = connection.host:service()
    end
end

function love.quit()
    if(connection.connected) then
        connection.server:disconnect()
        connection.host:flush()
    end
end

function draw_debug_info()
    if state.debug then
        local fps = love_timer.getFPS()
		local mem = collectgarbage("count")
		local text = ("upd: %.2fms, drw: %.2fms, fps: %d, mem: %.2fMB, tex_mem: %.2f MB"):format(stats.time_update, stats.time_draw, fps, mem / 1024, love_graphics.getStats().texturememory / 1024 / 1024)

		love_graphics.setFont(Font[15])
		love_graphics.setColor(255, 255, 255)
		love_graphics.printf(text, 10, love_graphics.getHeight() - 20, love_graphics.getWidth(), "left")
    end
end
