local Screen = Class({})

function Screen:init(name, components)
  self.name = name
  self.components = components
end

-- invoke fn for each component on this screen
function Screen:each_component(fn)
  self:_each_component(fn, self.components)
end

function Screen:_each_component(fn, components)
  table.foreach(components, function(c)
    fn(c)
    if c.components then
      Screen:_each_component(fn, c.components)
    end
  end)
end

return Screen
