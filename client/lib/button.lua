local Button = Class {
  sounds = {
    pressed = love.audio.newSource("sounds/A/button-pressed.ogg", "static"),
    released = love.audio.newSource("sounds/A/button-released.ogg", "static"),
  },
}

function Button.pressed()
  if Settings:vibrate() then love.system.vibrate(0.1) end
  Button.sounds.pressed:play()
end

function Button.released()
  Button.sounds.released:play()
end

return Button
