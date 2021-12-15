local Component = Class {}

local Slider = require("components.slider")
local Label = require("components.label")

local laserArmLabel = Label("Laser Arm")
local masterArmLabel = Label("Master Arm")

local rollApLabel = Label("Roll AP")
local pitchApLabel = Label("Pitch AP")

local function send_switch_identifier(identifier)
  local message = {type = "cockpit-switch", identifier = identifier}
  Signal.emit("send-to-server", message)
end

local function handle_roll_ap(value)
  local ids = {[-1] = "roll-ap-up", [0] = "roll-ap-mid", [1] = "roll-ap-down"}
  send_switch_identifier(ids[value])
end

local function handle_pitch_ap(value)
  local ids = {[-1] = "pitch-ap-up", [0] = "pitch-ap-mid", [1] = "pitch-ap-down"}
  send_switch_identifier(ids[value])
end

local function handle_master_arm(value)
  local ids = {[-1] = "master-arm-arm", [0] = "master-arm-safe", [1] = "master-arm-sim"}
  send_switch_identifier(ids[value])
end

local function handle_laser_arm(value)
  local ids = {[0] = "laser-arm-on", [1] = "laser-arm-off"}
  send_switch_identifier(ids[value])
end

local rollApSlider = Slider(0, -1, 1, handle_roll_ap, {orientation = "vertical"}, {values = {0, -1, 1}})
local pitchApSlider = Slider(0, -1, 1, handle_pitch_ap, {orientation = "vertical"}, {values = {0, -1, 1}})

local laserArmSlider = Slider(0, 0, 1, handle_laser_arm, {orientation = "vertical"}, {values = {0, 1}})
local masterArmSlider = Slider(0, -1, 1, handle_master_arm, {orientation = "vertical"}, {values = {0, -1, 1}})

function Component:init(options)
  local p = options or {}
  self.value = value
  self.padding = p.padding or 10
  self.font = love.graphics.newFont("fonts/b612/B612Mono-Regular.ttf", p.size or 10, "normal")
  self.transform = love.math.newTransform()

  self.flup = Flup.grid {
    rows = {
      {columns = {laserArmSlider, masterArmSlider}},
      {columns = {laserArmLabel, masterArmLabel}},
      {columns = {rollApSlider, pitchApSlider}},
      {columns = {rollApLabel, pitchApLabel}},
    },
    weights = {0.4, 0.2, 0.4, 0.2},
  }
  self.components = {rollApSlider, pitchApSlider, laserArmSlider, masterArmSlider}
end

function Component:draw()
  love.graphics.push()
  love.graphics.applyTransform(self.transform)
  self.flup:draw()
  love.graphics.pop()
end

function Component:updateGeometry(x, y, w, h)
  self.transform = love.math.newTransform():translate(x, y)
  self.w = w
  self.h = h
  self.flup:fill(self.padding, self.padding, w - self.padding * 2, h - self.padding * 2)
end

function Component:update(dt)
  local dx, dy = self.transform:inverseTransformPoint(love.mouse.getX(), love.mouse.getY())
  for _, component in pairs(self.components) do component:update(dt, dx, dy) end
end

function Component:mousepressed(x, y, button, touch, presses)
  local dx, dy = self.transform:inverseTransformPoint(x, y)
  for _, component in pairs(self.components) do component:mousepressed(dx, dy, button, touch, presses) end
end

function Component:mousereleased(x, y, button, touch, presses)
  local dx, dy = self.transform:inverseTransformPoint(x, y)
  for _, component in pairs(self.components) do component:mousereleased(dx, dy, button, touch, presses) end
end

return Component
