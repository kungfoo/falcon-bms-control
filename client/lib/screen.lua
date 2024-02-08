local Screen = Class({})

function Screen:init(name, components)
  self.name = name
  self.components = components
end

return Screen
