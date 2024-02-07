local loadFile = require("lib.loadfile")

local Layouts = Class({
  path = "layouts",
})

local function listLayouts(path)
  local result = {}
  local files = love.filesystem.getDirectoryItems(path)
  print("Found " .. #files .. " layouts")
  for _, f in ipairs(files) do
    local ok, layout = loadFile(path .. "/" .. f)
    if ok then
      result[layout.id] = layout
    else
      print("Failed to load " .. layout)
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

return Layouts
