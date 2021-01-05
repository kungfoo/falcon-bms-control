local StreamedTexture = Class {}

function StreamedTexture.start(identifier)
  Signal.emit("send-to-server", {
    type = "streamed-texture",
    identifier = identifier,
    command = "start",
    refresh_rate = Settings:refreshRate(),
    quality = Settings:quality(),
  })
end

function StreamedTexture.stop(identifier)
  Signal.emit("send-to-server", {type = "streamed-texture", identifier = identifier, command = "stop"})
end

return StreamedTexture
