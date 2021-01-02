local Screen = Class {padding = 10}
local Slider = require("components.slider")
local Label = require("components.label")

local refresh_rate_label = Label("Refresh rate: 30/s")
local refresh_rate_slider = Slider(30, 15, 60, function(value)
  refresh_rate_label.value = "Refresh rate: " .. value .. "/s"
end, {}, {values = {15, 30, 60}})

function Screen:init()
  self.settings_label = Label("Settings")
end

function Screen:enter(previous)
end

function Screen:leave()
end

function Screen:update(dt)
end

function Screen:draw()
  self.settings_label:draw()
end

return Screen
