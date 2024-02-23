local Screen = Class({ padding = 10, dimensions = { w = 0, h = 0 } })
local Label = require("components.label")
local ImageButton = require("components.image-button")
local Choice = require("components.choice")

local title_label = Label("Choose a screen layout", { size = 30 })
local choice_description_label = Label("Describes choice...")

function Screen:init()
  self.close_button = ImageButton("icons/close.png", { align = "right" }, function()
    State.switch(self.previous_screen, self)
  end)

  local options = {
    { text = "option 1", value = 1 },
    { text = "option 2", value = 2 },
    { text = "option 3", value = 3 },
  }
  self.choice = Choice(options)

  self.components = { self.close_button, self.choice }
end

function Screen:enter(previous)
  self.previous_screen = previous
end

function Screen:leave() end

function Screen:update(dt)
  local x, y, w, h = love.window.getSafeArea()
  if self.dimensions.w ~= w or self.dimensions.h ~= h then
    self:adjustLayoutIfNeeded(w, h)
    self.flup:fill(x + self.padding, y + self.padding, w - self.padding * 2, h - self.padding * 2)
    self.dimensions.w = w
    self.dimensions.h = h
  end
  table.foreach(self.components, function(component)
    component:update(dt)
  end)
end

function Screen:handleReceive(event)
  -- intentionally left blank
end

function Screen:adjustLayoutIfNeeded(w, h)
  local grid = Flup.grid({
    rows = {
      { columns = { title_label } },
      { columns = { self.choice } },
      { columns = { choice_description_label } },
    },
  })
  self.flup = Flup.split({
    direction = "y",
    ratio = 0.95,
    margin = 10,
    components = { top = grid, bottom = self.close_button },
  })
end

function Screen:draw()
  self.flup:draw()
end

function Screen:mousepressed(x, y, button, isTouch, presses)
  table.foreach(self.components, function(component)
    component:mousepressed(x, y, button, isTouch, presses)
  end)
end

function Screen:mousereleased(x, y, button, isTouch, presses)
  table.foreach(self.components, function(component)
    component:mousereleased(x, y, button, isTouch, presses)
  end)
end

function Screen:textedited(text, start, length)
  table.foreach(self.components, function(component)
    pcall(function()
      component:textedited(text, start, length)
    end)
  end)
end

function Screen:textinput(t)
  table.foreach(self.components, function(component)
    pcall(function()
      component:textinput(t)
    end)
  end)
end

function Screen:keypressed(key)
  table.foreach(self.components, function(component)
    pcall(function()
      component:keypressed(key)
    end)
  end)
end

return Screen
