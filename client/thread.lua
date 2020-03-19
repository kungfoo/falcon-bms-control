require("love.timer")

channel 	= {}
channel.a	= love.thread.getChannel ( "a" )
channel.b	= love.thread.getChannel ( "b" )

while true do
	local v = channel.b:pop()
	print("Thread: ")
	love.timer.sleep(16)
end