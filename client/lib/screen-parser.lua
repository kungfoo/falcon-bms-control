local Screen = require("lib.screen")
local ScreenParser = Class({})

function ScreenParser:findComponentId(node)
  return (node.metadata or {}).id or node.identifier
end

function ScreenParser:createComponents(node, result)
  if node.identifier then
    -- this is a leaf component
    local c = self.component_registry:find(node.identifier)
    if c then
      local id = self:findComponentId(node)
      table.push(result, c(id))
      return
    else
      local msg = "Could not find component with identifier " .. node.identifier
      log.error(msg)
      error(msg)
    end
  end

  local child_components = {}
  if node.components then
    for _, child in ipairs(node.components) do
      self:createComponents(child, child_components)
    end
  end

  -- lua poor man's match-statement
  local cases = {
    split = function()
      log.debug("Creating a split with " .. #child_components .. " child components")
      -- this is a layout node
      table.push(
        result,
        Flup.split({
          direction = node.direction,
          ratio = node.ratio,
          components = child_components,
        })
      )
    end,
    screen = function()
      log.debug("Visited screen node " .. node.name)
      table.push(result, unpack(child_components))
    end,
    default = function()
      local msg = "Unknown node " .. inspect(node) .. " with child components found."
      log.error(msg)
      error(msg)
    end,
  }

  if node.type then
    local case = cases[node.type] or cases.default
    case()
  end

  return result
end

function ScreenParser:createComponentsOnScreen(screen)
  return self:createComponents(screen, {})
end

function ScreenParser:init(component_registry)
  self.component_registry = component_registry
end

function ScreenParser:createScreens(screen_definitions)
  log.debug("Found " .. #screen_definitions .. " screens")
  local result = {}
  for i, screen in ipairs(screen_definitions) do
    local components = self:createComponentsOnScreen(screen)
    log.debug("Created " .. #components .. " components")
    local name = screen.name or "undefined-" .. i
    table.push(result, Screen(name, components))
  end
  return result
end

return ScreenParser
