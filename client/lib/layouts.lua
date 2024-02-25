local loadFile = require("lib.loadfile")

local Layouts = Class({
  path = "layouts",
})

local function listLayouts(path)
  local result = {}
  local files = love.filesystem.getDirectoryItems(path)
  log.debug("Found " .. #files .. " layouts")
  for _, f in ipairs(files) do
    local ok, layout = loadFile(path .. "/" .. f)
    if ok then
      result[layout.id] = layout
    else
      log.error("Failed to load " .. layout)
    end
  end
  return result
end

function Layouts:init()
  self.layouts = listLayouts(self.path)
end

function Layouts:find(id)
  return self.layouts[id]
end

-- descriptors for all the available layouts
function Layouts:descriptors()
  local result = {}
  for i, l in pairs(self.layouts) do
    table.push(result, {
      id = l.id,
      name = l.name,
      description = l.description,
    })
  end
  return result
end

-- descriptor for one layout
function Layouts:descriptor(id)
  local found = self:find(id)
  if found then
    return {
      id = found.id,
      name = found.name,
      description = found.description,
    }
  end
  return nil
end

return Layouts
