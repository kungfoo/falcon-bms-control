
local function Proxy(f)
	return setmetatable({}, {__index = function(t,k)
		local v = f(k)
		t[k] = v
		return v
	end})
end

Font  = Proxy(function(arg)
	if tonumber(arg) then
		return love.graphics.newFont('fonts/slkscr.ttf', arg)
	end
	return Proxy(function(size) return love.graphics.newFont('fonts/'..arg..'.ttf', size) end)
end)

Image = Proxy(function(image_file)
	assert(type(image_file) == 'string', 'Loading images requires a string path')
	return love.image.newImageData('resources/images/'..image_file)
end)

