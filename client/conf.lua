function love.conf(t)
  t.appendidentity = false -- Search files in source directory before save directory (boolean)
  t.version = "11.3" -- The LÖVE version this game was made for (string)
  t.console = true -- Attach a console (boolean, Windows only)
  t.accelerometerjoystick = false -- Enable the accelerometer on iOS and Android by exposing it as a Joystick (boolean)
  t.externalstorage = false -- True to save files (and read from the save directory) in external storage on Android (boolean) 
  t.gammacorrect = false -- Enable gamma-correct rendering, when supported by the system (boolean)

  t.audio.mic = false -- Request and use microphone capabilities in Android (boolean)
  t.audio.mixwithsystem = true -- Keep background music playing when opening LOVE (boolean, iOS and Android only)

  t.window.title = "Falcon BMS Universal MFD/ICP" -- The window title (string)
  t.window.width = 1024 -- The window width (number)
  t.window.height = 768 -- The window height (number)
  t.window.borderless = false -- Remove all border visuals from the window (boolean)
  t.window.resizable = true -- Let the window be user-resizable (boolean)
  t.window.fullscreen = true -- Enable fullscreen (boolean)
  t.window.fullscreentype = "desktop" -- Choose between "desktop" fullscreen or "exclusive" fullscreen mode (string)
  t.window.vsync = 1 -- Vertical sync mode (number)
  t.window.msaa = 4 -- The number of samples to use with multi-sampled antialiasing (number)
  t.window.depth = nil -- The number of bits per sample in the depth buffer
  t.window.stencil = nil -- The number of bits per sample in the stencil buffer
  t.window.display = 1 -- Index of the monitor to show the window in (number)
  t.window.highdpi = true -- Enable high-dpi mode for the window on a Retina display (boolean)
  t.window.usedpiscale = true -- Enable automatic DPI scaling when highdpi is set to true as well (boolean)

  t.modules.audio = true -- Enable the audio module (boolean)
  t.modules.data = true -- Enable the data module (boolean)
  t.modules.event = true -- Enable the event module (boolean)
  t.modules.font = true -- Enable the font module (boolean)
  t.modules.graphics = true -- Enable the graphics module (boolean)
  t.modules.image = true -- Enable the image module (boolean)
  t.modules.joystick = false -- Enable the joystick module (boolean)
  t.modules.keyboard = false -- Enable the keyboard module (boolean)
  t.modules.math = true -- Enable the math module (boolean)
  t.modules.mouse = true -- Enable the mouse module (boolean)
  t.modules.physics = false -- Enable the physics module (boolean)
  t.modules.sound = true -- Enable the sound module (boolean)
  t.modules.system = true -- Enable the system module (boolean)
  t.modules.thread = true -- Enable the thread module (boolean)
  t.modules.timer = true -- Enable the timer module (boolean), Disabling it will result 0 delta time in love.update
  t.modules.touch = true -- Enable the touch module (boolean)
  t.modules.video = false -- Enable the video module (boolean)
  t.modules.window = true -- Enable the window module (boolean)
end
