package.path = "../../?.lua;" .. package.path

-- a bunch of functions
log = require("lib.log")
require("lib.interpolate")
require("lib.core.table")
require("lib.core.functional")

Class = require("lib.hump.class")
