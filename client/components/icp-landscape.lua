local layout = require("lib.suit.layout").new()
local IcpButton = require("components.icp-button")
local RockerButton = require("components.rocker")

local Icp = Class({
  padding = 25,
  width = 800,
  height = 280,
  identifier = "f16/icp-landscape",
})

function Icp:init(id, x, y)
  self.id = id
  self.transform = love.math.newTransform():translate(x or 0, y or 0)
  self:createButtons(id)
end

function Icp:createButtons(id)
  self.buttons = {}
  layout:reset(10, 10, self.padding - 5, self.padding)
  table.insert(
    self.buttons,
    IcpButton(id, "COM1", { label = "COM", number = 1, type = "round" }, layout:col(IcpButton.size, IcpButton.size))
  )
  table.insert(self.buttons, IcpButton(id, "COM2", { label = "COM", number = 2, type = "round" }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "IFF", { label = "IFF", type = "round" }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "LIST", { label = "LIST", type = "round" }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "A-A", { label = "A-A", type = "round" }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "A-G", { label = "A-G", type = "round" }, layout:col()))

  local keypad = { x = 10, y = IcpButton.size + 40 }
  layout:reset(0, 0, self.padding)
  layout:push(keypad.x, keypad.y)
  table.insert(self.buttons, IcpButton(id, "1", { label = "T-ILS", number = 1 }, layout:col(IcpButton.size, IcpButton.size)))
  table.insert(self.buttons, IcpButton(id, "2", { label = "ALOW", number = 2 }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "3", { number = 3 }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "4", { label = "STPT", number = 4 }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "5", { label = "CRUS", number = 5 }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "RCL", { label = "RCL" }, layout:col()))

  layout:pop()
  layout:push(keypad.x, keypad.y)
  layout:row(IcpButton.size, IcpButton.size)
  table.insert(self.buttons, IcpButton(id, "6", { label = "TIME", number = 6 }, layout:row()))
  table.insert(self.buttons, IcpButton(id, "7", { label = "MARK", number = 7 }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "8", { label = "FIX", number = 8 }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "9", { label = "A-CAL", number = 9 }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "0", { label = "M-SEL", number = 0 }, layout:col()))
  table.insert(self.buttons, IcpButton(id, "ENTER", { label = "ENTER" }, layout:col()))
  layout:pop()

  -- wpt rocker
  local rocker = {
    x = keypad.x + (8 * IcpButton.size + self.padding * 2),
    y = keypad.y,
  }
  layout:reset(rocker.x, rocker.y, self.padding - 5, self.padding)
  table.insert(self.buttons, RockerButton("icp-wpt-next", { direction = "UP" }, layout:row(IcpButton.size, IcpButton.size)))
  table.insert(self.buttons, RockerButton("icp-wpt-previous", { direction = "DOWN" }, layout:row()))

  -- ded 4-way dobber
  local dobber = {
    x = keypad.x + (10 * IcpButton.size + self.padding),
    y = keypad.y,
  }
  layout:reset(dobber.x, dobber.y, self.padding / 3)
  layout:col(IcpButton.size * 2 / 3, IcpButton.size * 2 / 3)
  table.insert(self.buttons, RockerButton("icp-ded-up", { direction = "UP" }, layout:col()))
  layout:left()
  table.insert(self.buttons, RockerButton("icp-ded-return", { direction = "LEFT" }, layout:row()))
  layout:col()
  table.insert(self.buttons, RockerButton("icp-ded-seq", { direction = "RIGHT" }, layout:col()))
  layout:row()
  layout:left()
  layout:left()
  table.insert(self.buttons, RockerButton("icp-ded-down", { direction = "DOWN" }, layout:col()))
end

function Icp:update(dt) end

function Icp:draw()
  love.graphics.push()
  love.graphics.applyTransform(self.transform)

  love.graphics.setColor(Colors.dark_grey)
  love.graphics.rectangle("fill", 0, 0, self.width, self.height, 10, 10)

  for _, button in ipairs(self.buttons) do
    button:draw()
  end

  love.graphics.pop()
end

function Icp:updateGeometry(x, y, w, h)
  local scale = self:determineScale(w, h)
  self.transform = love.math.newTransform()
  self.transform:translate(x, y):scale(scale)
  self:createButtons(self.id)
end

function Icp:determineScale(w, h)
  -- turns out this is nicely square as well
  if w >= Icp.width and h >= Icp.height then
    -- do not scale up
    return 1.0
  else
    local a, b = w / Icp.width, h / Icp.height
    return math.min(a, b)
  end
end

function Icp:mousepressed(x, y, button, isTouch, presses)
  local dx, dy = self.transform:inverseTransformPoint(x, y)
  for _, button in ipairs(self.buttons) do
    if button:hit(dx, dy) then
      self.pressed = button
      button:pressed()
    end
  end
end

function Icp:mousereleased(x, y, button, isTouch, presses)
  if self.pressed then
    self.pressed:released()
    self.pressed = nil
  end
end

return Icp
