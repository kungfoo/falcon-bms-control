local IcpDedRocker = Class {
}

function IcpDedRocker:init(id, x,y,width,height)
    self.id = id
    self.x, self.y = x, y
    self.width, self.height = width, height
end

function IcpDedRocker:draw()
    love.graphics.setColor(Colors.white)
    love.graphics.rectangle("line", self.x, self.y, self.width, self.height, 0)
end

function IcpDedRocker:update(dt)
end

function IcpDedRocker:hit(x, y)
    return false
end

return IcpDedRocker