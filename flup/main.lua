tick = require("tick")
Class = require("class")
Flup = require("flup")
inspect = require("inspect")

local Snip = Class {}

local width, height = nil, nil

-- Sample component drawing something
function Snip:init(label)
    self.label = label
    self.r = love.math.random()
    self.g = love.math.random()
    self.b = love.math.random()
end

function Snip:updateGeometry(x, y, w, h)
    self.x = x
    self.y = y
    self.w = w
    self.h = h
end

function Snip:draw()
    if self.x then
        love.graphics.setColor(self.r, self.g, self.b)
        love.graphics.rectangle("fill", self.x, self.y, self.w, self.h, 0, 0)
        love.graphics.setColor(0, 0, 0)
        love.graphics.printf(self.label, math.floor(self.x + 10),
                             math.floor(self.y + 10), self.w)
    end
end

local flup1 = Flup.split {
    direction = "y",
    ratio = 0.4,
    components = {
        top = Flup.split {
            direction = "x",
            ratio = 0.3,
            components = {left = Snip("Top Left"), right = Snip("Top Right")}
        },
        bottom = Flup.split {
            direction = "x",
            ratio = 0.5,
            components = {
                left = Snip("Bottom Left"),
                right = Flup.split {
                    direction = "y",
                    ratio = 0.5,
                    components = {
                        top = Snip("Subby Top"),
                        bottom = Flup.split {
                            direction = "x",
                            components = {left = Snip("X"), right = Snip("Y")}
                        }
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
        flup1:fill(0, 0, w, h - 100)
        width, height = w, h
    end
end

function love.draw()
    flup1:draw()
    love.graphics.setColor(0.3, 0.8, 0.5)
    love.graphics.rectangle("fill", 0, love.graphics.getHeight() - 100,
                            love.graphics.getWidth(), 100, 0, 0)
end
