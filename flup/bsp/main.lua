tick = require("../tick")
Class = require("../class")
Flup = require("../flup")
inspect = require("../inspect")

Snip = require("../snip")

local width, height = nil, nil

-- example of using flup bsp, a binary space partition.
-- components can be dynamically layouted to use the most space of whatever
-- they are placed in. Think tiling window manager for components/things.

local flup1 = Flup.split({
  direction = "y",
  ratio = 0.4,
  margin = 20,
  components = {
    top = Flup.split({ direction = "x", ratio = 0.3, components = { left = Snip("Top Left"), right = Snip("Top Right") } }),
    bottom = Flup.split({
      direction = "x",
      ratio = 0.5,
      components = {
        left = Snip("Bottom Left"),
        right = Flup.split({
          direction = "y",
          ratio = 0.5,
          components = {
            top = Snip("Subby Top"),
            bottom = Flup.split({ direction = "x", components = { left = Snip("X"), right = Snip("Y") } }),
          },
        }),
      },
    }),
  },
})

local flup2 = Flup.split({ direction = "x", components = { left = Snip("Left"), right = Snip("Right") }, margin = 40 })

local flup = flup1

function love.load()
  tick.framerate = 60 -- Limit framerate to 60 frames per second.
  tick.rate = 0.016
end

function love.update(dt)
  local w, h = love.graphics.getDimensions()
  if width ~= w or height ~= h then
    flup:fill(0, 0, w, h - 100)
    width, height = w, h
  end
end

function love.draw()
  flup:draw()
  love.graphics.setColor(0.3, 0.8, 0.5, 0.3)
  love.graphics.rectangle("fill", 0, love.graphics.getHeight() - 100, love.graphics.getWidth(), 100, 0, 0)
end

function love.keypressed(key)
  if key == "space" then
    if flup == flup1 then
      flup = flup2
    else
      flup = flup1
    end
  end
end
