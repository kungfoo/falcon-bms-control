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

return loadFile
