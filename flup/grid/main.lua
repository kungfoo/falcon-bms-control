tick = require("../tick")
Class = require("../class")
Flup = require("../flup")
inspect = require("../inspect")
local Snip = require("../snip")

local width, height = nil, nil

-- example of using flup grip, a space-filling grid.
-- components can be dynamically layouted to use the most space of whatever
-- they are placed in and have weights/percentages defined. Think html table.
-- Default weights are all components get the same amount of space, equally divided.

local grid = Flup.grid {
  rows = {
    {
      columns = {
        Snip("1.1"), Snip("1.2"), Snip("1.3")
      },
    },
    {
      columns = {
        Snip("2.1"), Snip("2.2"), Snip("2.3"), Snip("2.4")
      }
    },
    {
      columns = {
        -- empty rows should be possible and take space too
      }
    },
    {
      columns = {
        Snip("4.1"),
        Snip("4.2"),
        Flup.grid {
          rows = {
            {
              columns = {
                Snip("4.3.1"), Snip("4.3.2"), Snip("4.3.3")
              },
            },
          }
        }
      }
    }
  }
}

function love.load()
  tick.framerate = 60 -- Limit framerate to 60 frames per second.
  tick.rate = 0.016
end

function love.update(dt)
  local w, h = love.graphics.getDimensions()
  if width ~= w or height ~= h then
    grid:fill(0, 0, w, h - 100)
    width, height = w, h
  end
end

function love.draw()
  grid:draw()
  love.graphics.setColor(0.3, 0.8, 0.5, 0.3)
  love.graphics.rectangle("fill", 0, love.graphics.getHeight() - 100, love.graphics.getWidth(), 100, 0, 0)
end

function love.keypressed(key, scancode, isrepeat)
  if key == "escape" then love.event.quit() end
  if key == "space" then end
end