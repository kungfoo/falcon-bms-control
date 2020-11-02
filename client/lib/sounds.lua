local Sounds = {
    button = {
        pressed = love.audio.newSource("sounds/A/button-pressed.ogg", "static"),
        released = love.audio.newSource("sounds/A/button-released.ogg", "static")
    }
}

return Sounds