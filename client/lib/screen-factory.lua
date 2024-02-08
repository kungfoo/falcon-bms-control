local Screen = require("lib.screen")
local ScreenFactory = Class({})

function ScreenFactory:findComponentId(node)
  return (node.metadata or {}).id or node.identifier
end

function ScreenFactory:createComponents(node, result)
  if node.type == "split" then
    -- this is a layout node
    local child_components = {}
    self:createComponents(node.components, child_components)
    table.push(
      result,
      Flup.split({
        direction = node.direction,
        components = child_components,
      })
    )
  end

  if node.components then
    for _, child in ipairs(node.components) do
      self:createComponents(child, result)
    end
  end
  if node.identifier then
    -- this is an actual component
    local c = self.component_registry:find(node.identifier)
    if c then
      local id = self:findComponentId(node)
      table.push(result, c(id))
    else
      print("Could not find component with identifier " .. node.identifier)
    end
  end

  return result
end

function ScreenFactory:createComponentsOnScreen(screen)
  return self:createComponents(screen, {})
end

function ScreenFactory:init(component_registry)
  self.component_registry = component_registry
end

function ScreenFactory:createScreens(screen_definitions)
  print("Found " .. #screen_definitions .. " screens")
  local result = {}
  for i, screen in ipairs(screen_definitions) do
    local components = self:createComponentsOnScreen(screen)
    local name = screen.name or "undefined-" .. i
    table.push(result, Screen(name, components))
  end
  return result
end

return ScreenFactory
