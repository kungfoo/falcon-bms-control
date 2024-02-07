local loadFile = require("lib.loadfile")

local Layouts = Class({
  path = "layouts",
  layouts = {},
  state = "init",
})

local function listLayouts(path)
  local result = {}
  local files = love.filesystem.getDirectoryItems(path)
  print("Found " .. #files .. " layouts")
  for _, f in ipairs(files) do
    local ok, layout = loadFile(path .. "/" .. f)
    if ok then
      table.insert(result, layout)
    else
      print("Failed to load " .. layout)
    end
  end
  return result
end

function Layouts:init()
  self.layouts = listLayouts(self.path)
  self.state = "loaded"
end

return Layouts
