local component_registry = require("lib.component-registry")
local parser = require("lib.screen-parser")(component_registry)

describe("screen parser", function()
  it("should handle an empty screen", function()
    local spec = {
      {
        name = "empty screen",
        type = "screen",
        components = {},
      },
    }

    local result = parser:createScreens(spec)
    assert.is.equal(1, #result)
  end)

  it("should handle a single screen with 1 component", function()
    local spec = {
      {
        name = "single component screen",
        type = "screen",
        components = {
          {
            type = "mfd",
            identifier = "f16/mfd",
            data_channel = 1,
            metadata = {
              id = "f16/left-mfd",
            },
          },
        },
      },
    }

    local result = parser:createScreens(spec)
    assert.is.equal(1, #result)
    local components = result.components
    assert.is.equal("f16/left-mfd", components[1].identifier)
  end)

  it("tests positive assertions", function()
    assert.is_true(true) -- Lua keyword chained with _
    assert.True(true) -- Lua keyword using a capital
    assert.are.equal(1, 1)
    assert.has.errors(function()
      error("this should fail")
    end)
  end)

  it("tests negative assertions", function()
    assert.is_not_true(false)
    assert.are_not.equals(1, "1")
    assert.has_no.errors(function() end)
  end)
end)
