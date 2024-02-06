return {
  name = "one device per screen",
  description = "Puts each device (each MFD, ICP) on a separate screen. Useful for people with multiple tablets.",
  metadata = {
    version = "1.0",
  },
  definition = {
    screens = {
      {
        name = "Left MFD",
      },
      {
        name = "Right MFD",
      },
    },
  },
}
