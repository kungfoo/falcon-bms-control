--[[
	barebones oop basics
	supports basic inheritance and means you don't have to build/set your own metatable each time

	todo: collect some stats on classes/optional global class registry
]]
function class(inherits)
  local c = {}
  c.__mt = { __index = c }
  -- handle single inheritence
  if type(inherits) == "table" and inherits.__mt then
    setmetatable(c, inherits.__mt)
  end
  -- common class functions

  -- internal initialisation
  -- sets up an initialised object with a default value table
  -- performing a super construction if necessary and assigning the right metatable
  function c:init(t, ...)
    if inherits then
      -- super ctor, then overlay args table
      t = table.overlay(inherits:new(...), t)
    end
    -- upgrade to this class and return
    return setmetatable(t, self.__mt)
  end

  -- constructor
  -- generally to be overridden
  function c:new()
    return self:init({})
  end

  -- get the inherited class for super calls if/as needed
  -- allows overrides that still refer to superclass behaviour
  function c:super()
    return inherits
  end

  -- done
  return c
end
