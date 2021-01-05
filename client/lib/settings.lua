local msgpack = require("lib.msgpack")

local defaults = {refresh_rate = 30, quality = 80, vibrate = false}
local Settings = Class {}
local file_name = "settings.msgpack"

function Settings:init()
  local file_data, size = love.filesystem.read(file_name)
  local from_file = {}
  if file_data then from_file = msgpack.unpack(file_data) end
  self.proxy = {}
  self.proxy.refresh_rate = from_file.refresh_rate or defaults.refresh_rate
  self.proxy.quality = from_file.quality or defaults.quality
  self.proxy.vibrate = from_file.vibrate or defaults.vibrate
end

function Settings:changed()
  -- write new settings to disk
  local bytes = msgpack.pack({
    refresh_rate = self.proxy.refresh_rate,
    quality = self.proxy.quality,
    vibrate = self.proxy.vibrate,
  })
  local success, error = love.filesystem.write(file_name, bytes)
  if not success then print("Failed to write settings.") end
end

function Settings:setRefreshRate(value)
  self.proxy.refresh_rate = value
  self:changed()
end

function Settings:setQuality(value)
  self.proxy.quality = value
  self:changed()
end

function Settings:setVibrate(value)
  self.proxy.vibrate = value
  self:changed()
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

return Settings
