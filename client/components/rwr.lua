local StreamedTexture = require("lib.streamed-texture")

local Rwr = Class {
  -- max size of this component
  padding = 10,
  width = 240,
  height = 240,
  corner_radius = 10,
  font = love.graphics.newFont("fonts/b612/B612Mono-Regular.ttf", 20, "normal"),
}

function Rwr:init(id, x, y)
  self.id = id
  self.image = nil -- drawable image
  self.imageData = nil -- pure image data received from server
  self.transform = love.math.newTransform():translate(x or 0, y or 0)
  self.canvas = love.graphics.newCanvas(Rwr.width + Rwr.padding, Rwr.height + Rwr.padding)
end

function Rwr:update(dt)
end

local function stencil()
  love.graphics.circle("fill", (Rwr.width / 2) + Rwr.padding, (Rwr.height / 2) + Rwr.padding, Rwr.width / 2)
end

local function smallStencil()
  love.graphics.circle("fill", (Rwr.width / 2) + Rwr.padding, (Rwr.height / 2) + Rwr.padding, Rwr.width / 2)
  love.graphics.circle("fill", (Rwr.width / 2), (Rwr.height / 2), 52/2)
end

function Rwr:draw()
  love.graphics.push()
  love.graphics.applyTransform(self.transform)

  love.graphics.setColor(Colors.dark_grey)
  love.graphics.rectangle("fill", 0, 0, self.width + self.padding * 2, self.height + self.padding * 2,
                          self.corner_radius, self.corner_radius)

  love.graphics.stencil(stencil, "replace", 1)
  love.graphics.setStencilTest("gequal", 1)

  if self.imageData then
    love.graphics.setColor(Colors.white)
    if not self.image then
      -- only load image if it is a new frame.
      self.image = love.graphics.newImage(self.imageData)
    end
    love.graphics.draw(self.image, self.padding, self.padding, 0, 1, 1)
  else
    love.graphics.setColor(Colors.black)
    love.graphics.rectangle("fill", self.padding, self.padding, self.width, self.height)
    love.graphics.setColor(Colors.white)
    love.graphics.setFont(self.font)
    love.graphics.print("RWR data...", self.padding + 20, Rwr.height / 2 - 30)
  end

  -- draw overlay lines
  love.graphics.setCanvas({self.canvas, stencil=true})
  love.graphics.clear()
  love.graphics.setLineWidth(1)
  love.graphics.stencil(smallStencil, "increment")
  love.graphics.setStencilTest("less", 2)

  love.graphics.setColor(0.4, 0.5, 0.4, 1)
  local cx, cy = (Rwr.width / 2), (Rwr.height / 2)
  love.graphics.circle("line", cx, cy, 125 / 2)
  love.graphics.circle("line", cx, cy, 53 / 2)

  love.graphics.line(cx, 0, cx, Rwr.height)
  love.graphics.line(0, cy, Rwr.width, cy)
  love.graphics.setStencilTest()
  love.graphics.circle("fill", cx, cy, 2)
  
  -- set back to screen drawing
  love.graphics.setCanvas()
  love.graphics.pop()
  love.graphics.draw(self.canvas, Rwr.padding, Rwr.padding)

  love.graphics.setStencilTest()
end

function Rwr:updateGeometry(x, y, w, h)
  local scale = self:determineScale(w, h)
  self.transform = love.math.newTransform()
  self.transform:translate(x, y):scale(scale)
end

function Rwr:determineScale(w, h)
  local dw, dh = Rwr.width + Rwr.padding * 2, Rwr.height + Rwr.padding * 2
  if w >= dw and h >= dh then
    -- do not scale up
    return 1.0
  else
    local a, b = w / dw, h / dh
    return math.min(a, b)
  end
end

function Rwr:consume(data)
  self.imageData = love.image.newImageData(love.data.newByteData(data))
  self.image = nil
end

function Rwr:start()
  StreamedTexture.start(self.id)
end

function Rwr:stop()
  StreamedTexture.stop(self.id)
end

function Rwr:mousepressed(x, y, button, isTouch, presses)
  -- intentionally left blank
end

function Rwr:mousereleased(x, y, button, isTouch, presses)
end

return Rwr
