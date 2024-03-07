local msgpack = require("lib.msgpack")

local defaults = {
  refresh_rate = 30,
  quality = 70,
  vibrate = false,
  ip = nil,
  dark_mode = false,
  layout = "default-landscape",
}

local Settings = Class({})
local file_name = "settings.msgpack"

function Settings:init()
  local file_data, _ = love.filesystem.read(file_name)
  local success, from_file = false, {}
  if file_data then
    success, from_file = pcall(function()
      return msgpack.unpack(file_data)
    end)
    if not success then
      log.error("Failed to load settings, falling back to defaults because of " .. from_file)
    end
  end
  self.changed = false
  self.proxy = {}
  self.proxy.refresh_rate = from_file.refresh_rate or defaults.refresh_rate
  self.proxy.quality = from_file.quality or defaults.quality
  self.proxy.vibrate = from_file.vibrate or defaults.vibrate
  self.proxy.ip = from_file.ip or defaults.ip
  self.proxy.layout = from_file.layout or defaults.layout
end

function Settings:onChange()
  -- write new settings to disk
  local bytes = msgpack.pack({
    refresh_rate = self.proxy.refresh_rate,
    quality = self.proxy.quality,
    vibrate = self.proxy.vibrate,
    ip = self.proxy.ip,
    layout = self.proxy.layout,
  })
  local success, error = love.filesystem.write(file_name, bytes)
  if not success then
    log.error("Failed to write settings: ", error)
  end
  self.changed = true
end

function Settings:setRefreshRate(value)
  self.proxy.refresh_rate = value
  self:onChange()
end

function Settings:setQuality(value)
  self.proxy.quality = value
  self:onChange()
end

function Settings:setVibrate(value)
  self.proxy.vibrate = value
  self:onChange()
end

function Settings:setIp(value)
  self.needs_reconnect = true
  self.proxy.ip = value
  self:onChange()
end

function Settings:setLayout(value)
  self.needs_reconnect = true
  self.proxy.layout = value
  self:onChange()
end

function Settings:refreshRate()
  return self.proxy.refresh_rate
end

function Settings:quality()
  return self.proxy.quality
end

function Settings:vibrate()
  return self.proxy.vibrate
end

function Settings:ip()
  return self.proxy.ip
end

function Settings:layout()
  return self.proxy.layout
end

return Settings
