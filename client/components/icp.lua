local layout = require("lib.suit.layout").new()
local IcpButton = require("components.icp-button")
local RockerButton = require("components.rocker")

local Icp = Class {
  buttons = {}, 
  padding = 25
}

function Icp:init(id, x, y)
  self.id = id
  self.position = {x = x or 0, y = y or 0}
  self:createButtons(id)
end

function Icp:createButtons(id)
  layout:reset(self.position.x, self.position.y, self.padding)
  table.insert(self.buttons, IcpButton(id, "COM1", {label = "COM", number = 1, type = "round"},
                                       layout:col(IcpButton.size, IcpButton.size)))
  table.insert(self.buttons, IcpButton(id, "COM2", {label = "COM", number = 2, type = "round"}, layout:col()))
  table.insert(self.buttons, IcpButton(id, "IFF", {label = "IFF", type = "round"}, layout:col()))
  table.insert(self.buttons, IcpButton(id, "LIST", {label = "LIST", type = "round"}, layout:col()))
  table.insert(self.buttons, IcpButton(id, "A-A", {label = "A-A", type = "round"}, layout:col()))
  table.insert(self.buttons, IcpButton(id, "A-G", {label = "A-G", type = "round"}, layout:col()))

  local keypad = {x = self.position.x, y = self.position.y + IcpButton.size + 30}
  layout:push(keypad.x, keypad.y)
  table.insert(self.buttons,
               IcpButton(id, "1", {label = "T-ILS", number = 1}, layout:col(IcpButton.size, IcpButton.size)))
  table.insert(self.buttons, IcpButton(id, "2", {label = "ALOW", number = 2}, layout:col()))
  table.insert(self.buttons, IcpButton(id, "3", {number = 3}, layout:col()))
  table.insert(self.buttons, IcpButton(id, "RCL", {label = "RCL"}, layout:col()))

  layout:pop()
  layout:push(keypad.x, keypad.y)
  layout:row(IcpButton.size, IcpButton.size)
  table.insert(self.buttons, IcpButton(id, "4", {label = "STPT", number = 4}, layout:row()))
  table.insert(self.buttons, IcpButton(id, "5", {label = "CRUS", number = 5}, layout:col()))
  table.insert(self.buttons, IcpButton(id, "6", {label = "TIME", number = 6}, layout:col()))
  table.insert(self.buttons, IcpButton(id, "ENTER", {label = "ENTER"}, layout:col()))

  layout:pop()
  layout:push(keypad.x, keypad.y)
  layout:row(IcpButton.size, IcpButton.size)
  layout:row()
  table.insert(self.buttons, IcpButton(id, "7", {label = "MARK", number = 7}, layout:row()))
  table.insert(self.buttons, IcpButton(id, "8", {label = "FIX", number = 8}, layout:col()))
  table.insert(self.buttons, IcpButton(id, "9", {label = "A-CAL", number = 9}, layout:col()))
  table.insert(self.buttons, IcpButton(id, "0", {label = "M-SEL", number = 0}, layout:col()))

  -- wpt rocker
  layout:pop()
  layout:push(keypad.x, keypad.y)
  layout:row(IcpButton.size, IcpButton.size)
  layout:row()
  layout:row()
  table.insert(self.buttons, RockerButton("icp-wpt-next", {direction = "UP"}, layout:row(IcpButton.size, IcpButton.size)))
  table.insert(self.buttons, RockerButton("icp-wpt-previous", {direction = "DOWN"}, layout:row()))

  -- ded 4-way rocker
  layout:reset(keypad.x + IcpButton.size + self.padding, keypad.y + (4*IcpButton.size + self.padding/2), self.padding/3)
  layout:col(IcpButton.size*2/3, IcpButton.size*2/3)
  table.insert(self.buttons, RockerButton("icp-ded-up", {direction = "UP"}, layout:col()))
  layout:left()
  table.insert(self.buttons, RockerButton("icp-ded-return", {direction = "LEFT"}, layout:row()))
  layout:col()
  table.insert(self.buttons, RockerButton("icp-ded-seq", {direction = "RIGHT"}, layout:col()))
  layout:row()
  layout:left()
  layout:left()
  table.insert(self.buttons, RockerButton("icp-ded-down", {direction = "DOWN"}, layout:col()))
end

function Icp:update(dt)
end

function Icp:draw()
  for _, button in ipairs(self.buttons) do button:draw() end
end

function Icp:mousepressed(x, y, button, isTouch, presses)
  for _, button in ipairs(self.buttons) do
    if button:hit(x, y) then
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
