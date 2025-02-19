local socket = require("socket")
local enet = require("enet")
local Screen = Class({ padding = 10, dimensions = { w = 0, h = 0 } })

local Label = require("components.label")
local ImageButton = require("components.image-button")

local connection_state_label = Label("Discovering server...", { size = 30 })
local broadcast = { port = 9020 }
local connecting = { port = 9022, channels = 255 }

function Screen:init()
  self.settings_button = ImageButton("icons/settings.png", { align = "right" }, function()
    State.switch(settings_screen, self)
  end)
  self.components = { connection_state_label, self.settings_button }
end

function Screen:enter(previous)
  self.previous_screen = previous

  if Settings:ip() then
    log.info("Connecting to " .. Settings:ip() .. ", stored in settings.")
    Screen:connect(Settings:ip())
  else
    log.info("Starting server discovery...")
    connection_state_label.value = "Discovering server..."
    broadcast.socket = socket.udp4()
    broadcast.socket:settimeout(0)
    broadcast.socket:setoption("broadcast", true)
    broadcast.send = Timer.every(0.9, self.sendHello)
    broadcast.receive = Timer.every(1, self.receiveAck)
  end
end

function Screen:connect(ip)
  connection_state_label.value = "Connecting to " .. ip .. "..."
  Connection.ip = ip or Settings:ip()
  log.debug("enet version: ${version}" % { version = enet.linked_version() })
  Connection.host = enet.host_create()
  Connection.server = Connection.host:connect(Connection.ip .. ":" .. connecting.port, connecting.channels)
end

function Screen:sendHello()
  local message = msgpack.pack({ type = "hello" })
  broadcast.socket:sendto(message, "255.255.255.255", broadcast.port)
end

function Screen:receiveAck()
  local datagram, ip, port, error = broadcast.socket:receivefrom()
  if datagram and ip and port then
    local message = msgpack.unpack(datagram)
    if message.type == "ack" then
      log.info("Discovered server at [${ip}] on port [${port}]" % { ip = ip, port = port })
      Screen:connect(ip)
    end
  end
end

function Screen:leave()
  -- are we even sending?
  if broadcast.send then
    log.info("Stopping server discovery.")
    Timer.cancel(broadcast.send)
    Timer.cancel(broadcast.receive)
    broadcast.socket:close()
  end
end

function Screen:update(dt)
  local x, y, w, h = love.window.getSafeArea()
  if self.dimensions.w ~= w or self.dimensions.h ~= h then
    self:adjustLayoutIfNeeded(w, h)
    self.flup:fill(x + self.padding, y + self.padding, w - self.padding * 2, h - self.padding * 2)
    self.dimensions.w = w
    self.dimensions.h = h
  end
  for _, component in ipairs(self.components) do
    component:update(dt)
  end
end

function Screen:handleReceive(event)
  if event and event.type == "connect" then
    log.info("Connected ...")
    Connection.peer = event.peer
    State.switch(table.back(custom_screens))
  elseif event and event.type == "disconnect" then
    log.info("Disconnected...")
    Connection.peer = nil
  end
end

function Screen:adjustLayoutIfNeeded(w, h)
  self.flup = Flup.split({
    direction = "y",
    ratio = 0.95,
    margin = 10,
    components = { top = connection_state_label, bottom = self.settings_button },
  })
end

function Screen:draw()
  self.flup:draw()
end

function Screen:mousepressed(x, y, button, isTouch, presses)
  for _, component in ipairs(self.components) do
    component:mousepressed(x, y, button, isTouch, presses)
  end
end

function Screen:mousereleased(x, y, button, isTouch, presses)
  for _, component in ipairs(self.components) do
    component:mousereleased(x, y, button, isTouch, presses)
  end
end

return Screen
