return {
  id = "one-device",
  name = "One device per screen",
  description = "Puts each device (each MFD, ICP) on a separate screen. Useful for people with multiple tablets.",
  metadata = {
    version = "1.0",
  },
  definition = {
    screens = {
      {
        type = "screen",
        name = "Left MFD",
        components = {
          {
            type = "mfd",
            identifier = "f16/mfd",
            metadata = {
              id = "f16/left-mfd",
            },
          },
        },
      },
      {
        type = "screen",
        name = "Right MFD",
        components = {
          {
            type = "mfd",
            identifier = "f16/mfd",
            metadata = {
              id = "f16/right-mfd",
            },
          },
        },
      },
      {
        type = "screen",
        name = "ICP/DED",
        components = {
          {
            type = "split",
            direction = "y",
            ratio = 0.3,
            components = {
              {
                type = "ded",
                identifier = "f16/ded",
              },
              {
                type = "icp",
                identifier = "f16/icp",
              },
            },
          },
        },
      },
    },
  },
}
