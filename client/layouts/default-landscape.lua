return {
  id = "default-landscape",
  name = "default landscape",
  description = "The default landscape layout, puts two MFDs side-by-side and the ICP and RWR on another screen.",
  metadata = {
    version = "1.0",
  },
  definition = {
    screens = {
      {
        type = "screen",
        name = "Both MFDs",
        components = {
          {
            type = "split",
            direction = "x",
            margin = 20,
            components = {
              {
                type = "mfd",
                identifier = "f16/mfd",
                data_channel = 1,
                metadata = {
                  id = "f16/left-mfd",
                },
              },
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
        },
      },
      {
        type = "screen",
        name = "ICP, DED and RWR",
        components = {
          {
            type = "split",
            direction = "x",
            ratio = 0.3,
            components = {
              {
                type = "rwr",
                identifier = "f16/rwr",
                data_channel = 4,
              },
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
    },
  },
}
