return {
  id = "one-device",
  name = "one device per screen",
  description = "Puts each device (each MFD, ICP) on a separate screen. Useful for people with multiple tablets.",
  metadata = {
    version = "1.0",
  },
  definition = {
    screens = {
      {
        name = "Left MFD",
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
      {
        name = "Right MFD",
        components = {
          {
            type = "mfd",
            identifier = "f16/mfd",
            data_channel = 2,
            metadata = {
              id = "f16/right-mfd",
            },
          },
        },
      },
      {
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
                data_channel = 3,
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
