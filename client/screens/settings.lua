local Screen = Class {padding = 10, dimensions = {w = 0, h = 0}}
local Slider = require("components.slider")
local Label = require("components.label")
local ImageButton = require("components.image-button")

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

local vibrate_label = Label(describe_vibration(Settings:vibrate()))
local vibrate_slider = Slider(from_bool(Settings:vibrate()), 0, 1, function(value)
  Settings:setVibrate(to_bool(value))
  vibrate_label.value = describe_vibration(Settings:vibrate())
end, {}, {values = {0, 1}})

local settings_label = Label("Settings", {size = 30})

function Screen:init()
  self.close_button = ImageButton("icons/close.png", {align = "right"}, function()
    State.switch(self.previous_screen)
  end)
  self.labels = {settings_label, refresh_rate_label, quality_label, vibrate_label}
  self.components = {refresh_rate_slider, quality_slider, vibrate_slider, self.close_button}
end

function Screen:enter(previous)
  self.previous_screen = previous
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

function Screen:adjustLayoutIfNeeded(w, h)
  local settings_flup = Flup.split {
    direction = "y",
    components = {
      top = Flup.split {
        direction = "y",
        components = {
          top = Flup.split {direction = "x", components = {left = settings_label, right = nil}},
          bottom = Flup.split {direction = "x", components = {left = refresh_rate_label, right = refresh_rate_slider}},
        },
      },
      bottom = Flup.split {
        direction = "y",
        components = {
          top = Flup.split {direction = "x", components = {left = quality_label, right = quality_slider}},
          bottom = Flup.split {direction = "x", components = {left = vibrate_label, right = vibrate_slider}},
        },
      },
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

function Screen:handleReceive(event)
  -- intentionally left blank
end

return Screen
