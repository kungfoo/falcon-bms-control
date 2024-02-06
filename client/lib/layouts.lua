local Layouts = Class({
  path = ".",
  layouts = {},
  state = "init",
})

local function loadFile(path)
  print("Loading " .. path)
  -- load the chunk safely
  local ok, chunk, err = pcall(love.filesystem.load, path)
  if not ok then
    return false, "Failed loading code: " .. chunk
  end
  if not chunk then
    return false, "Failed reading file: " .. err
  end

  -- execute the chunk safely
  local ok, value = pcall(chunk)
  if not ok then
    return false, "Failed calling chunk: " .. tostring(value)
  end

  return true, value
end

local function listLayouts()
  local result = {}
  local files = love.filesystem.getDirectoryItems("layouts")
  print("Found " .. #files .. " layouts")
  for _, f in ipairs(files) do
    local ok, layout = loadFile("layouts/" .. f)
    if ok then
      table.insert(result, layout)
    else
      print("Failed to load " .. layout)
    end
  end
  return result
end

function Layouts:init()
  self.layouts = listLayouts()
  self.state = "loaded"
end

return Layouts
