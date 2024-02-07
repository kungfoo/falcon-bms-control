local loadFile = require("lib.loadfile")

local ComponentRegistry = Class({
  path = "components",
  components = {},
  state = "init",
})

local function findAllComponents(path)
  local result = {}
  local files = love.filesystem.getDirectoryItems(path)
  for _, f in ipairs(files) do
    local ok, component = loadFile(path .. "/" .. f)
    if ok then
      if component.identifier then
        -- a component we can identify
        result[component.identifier] = component
      end
    else
      print("Failed to load component due to " .. component)
    end
  end
  print("Found the following components:")
  for id, c in pairs(result) do
    print("\t" .. id)
  end
  return result
end

function ComponentRegistry:init()
  self.components = findAllComponents(self.path)
  self.state = "loaded"
end

function ComponentRegistry:find(identifier)
  return self.components[identifier]
end

return ComponentRegistry
