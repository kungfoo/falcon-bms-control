local ScreenFactory = Class({})

function ScreenFactory:listComponentsR(node)
  if node.identifier then
    local c = self.component_registry:find(node.identifier)
    if c then
      print("Found component with id " .. node.identifier)
    end
  end

  if node.components then
    for _, child in ipairs(node.components) do
      self:listComponentsR(child)
    end
  end
end

function ScreenFactory:listComponentsOnScreens(screen)
  self:listComponentsR(screen)
end

function ScreenFactory:init(component_registry)
  self.component_registry = component_registry
end

function ScreenFactory:createScreens(screen_definitions)
  print("Found " .. #screen_definitions .. " screens")

  for i, screen in ipairs(screen_definitions) do
    print("Screen name: " .. screen.name)
    self:listComponentsOnScreens(screen)
  end
end

return ScreenFactory
