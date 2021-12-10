local Screen = Class {padding = 10, dimensions = {w = 0, h = 0}}
local Slider = require("components.slider")
local Label = require("components.label")
local ImageButton = require("components.image-button")
local Input = require("components.input")

-- adjusts max RTT texture update frequency, may be beneficial for slow devices
local function describe_refresh_rate(value)
  return "Displays refresh rate: " .. value .. "/s"
end

local refresh_rate_label = Label(describe_refresh_rate(Settings:refreshRate()))
local refresh_rate_slider = Slider(Settings:refreshRate(), 15, 60, function(value)
  Settings:setRefreshRate(value)
  refresh_rate_label.value = describe_refresh_rate(value)
end, {}, {values = {15, 30, 60}})

-- adjusts jpeg compression quality, may be beneficial for really slow networks
local function describe_quality(value)
  return "Compression quality: " .. value
end

local quality_label = Label(describe_quality(Settings:quality()))
local quality_slider = Slider(Settings:quality(), 50, 90, function(value)
  Settings:setQuality(math.floor(value))
  quality_label.value = describe_quality(Settings:quality())
end)

local function describe_vibration(value)
  if value == true then return "Vibration: ON" end
  return "Vibration: OFF"
end

local function from_bool(value)
  if value then
    return 1
  else
    return 0
  end
end

local function to_bool(value)
  if value == 0 then
    return false
  else
    if value == 1 then
      return true
    else
      print("Intederminate value of bool: " .. value)
      return false
    end
  end
end

local function build_ip_address_allowed_chars()
  local allowed_chars = {}
  for i = 0, 9 do allowed_chars["" .. i] = true end
  allowed_chars["."] = true
  return allowed_chars
end

local ip_address_allowed_chars = build_ip_address_allowed_chars()

local function ip_address_allows_input(value, key)
  if ip_address_allowed_chars[key] then return true end
  return false
end

local function valid_ip_address(value)
  return string.match(value, "^%d%d?%d?%.%d%d?%d?%.%d%d?%d?%.%d%d?%d?$") ~= nil
end

local function handle_ip_address_input(ip)
  if valid_ip_address(ip) then Settings:setIp(ip) end
  -- clear settings when field is empty
  if ip == "" then Settings:setIp(nil) end
end

local server_ip_label = Label("Server IP (disables discovery)")
local server_ip_input = Input(Settings:ip(),
                              {allows_input = ip_address_allows_input,
                              place_holder = "[automatic discovery]"},
                              handle_ip_address_input)

local vibrate_label = Label(describe_vibration(Settings:vibrate()))
local vibrate_slider = Slider(from_bool(Settings:vibrate()), 0, 1, function(value)
  Settings:setVibrate(to_bool(value))
  vibrate_label.value = describe_vibration(Settings:vibrate())
end, {}, {values = {0, 1}})

local settings_label = Label("Settings", {size = 30})

function Screen:init()
  self.close_button = ImageButton("icons/close.png", {align = "right"}, function()
    State.switch(self.previous_screen, self)
  end)
  self.labels = {settings_label, refresh_rate_label, quality_label, vibrate_label, server_ip_label}
  self.components = {refresh_rate_slider, quality_slider, vibrate_slider, self.close_button, server_ip_input}
end

function Screen:enter(previous)
  self.previous_screen = previous
  server_ip_input:setValue(Settings:ip())
end

function Screen:leave()
end

function Screen:update(dt)
  local w, h = love.graphics.getDimensions()
  if self.dimensions.w ~= w or self.dimensions.h ~= h then
    self:adjustLayoutIfNeeded(w, h)
    self.flup:fill(self.padding, self.padding, w - self.padding * 2, h - self.padding * 2)
    self.dimensions.w = w
    self.dimensions.h = h
  end
  for _, component in ipairs(self.components) do component:update(dt) end
end

function Screen:handleReceive(event)
  -- intentionally left blank
end

function Screen:adjustLayoutIfNeeded(w, h)
  local settings_flup = Flup.grid {
    rows = {
      {columns = {settings_label, {}}},
      {columns = {server_ip_label, server_ip_input}},
      {columns = {refresh_rate_label, refresh_rate_slider}},
      {columns = {quality_label, quality_slider}},
      {columns = {vibrate_label, vibrate_slider}},
    },
  }

  self.flup = Flup.split {
    direction = "y",
    ratio = 0.95,
    margin = 10,
    components = {top = settings_flup, bottom = self.close_button},
  }
end

function Screen:draw()
  self.flup:draw()
end

function Screen:mousepressed(x, y, button, isTouch, presses)
  for _, component in ipairs(self.components) do component:mousepressed(x, y, button, isTouch, presses) end
end

function Screen:mousereleased(x, y, button, isTouch, presses)
  for _, component in ipairs(self.components) do component:mousereleased(x, y, button, isTouch, presses) end
end

function Screen:textedited(text, start, length)
  for _, component in ipairs(self.components) do
    pcall(function()
      component:textedited(text, start, length)
    end)
  end
end

function Screen:textinput(t)
  for _, component in ipairs(self.components) do
    pcall(function()
      component:textinput(t)
    end)
  end
end

function Screen:keypressed(key)
  for _, component in ipairs(self.components) do
    pcall(function()
      component:keypressed(key)
    end)
  end
end

return Screen
