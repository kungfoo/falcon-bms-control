return {
  id = "consolidated",
  name = "All devices consolidated",
  description = "A consolidated layout for bigger screens, puts MFDs and (an alternate) ICP on the same screen.",
  metadata = {
    version = "1.0",
  },
  definition = {
    screens = {
      {
        type = "screen",
        name = "Everything but the kitchen sink.",
        components = {
          {
            type = "split",
            direction = "y",
            margin = 10,
            ratio = 0.3,
            components = {
              {
                type = "split",
                direction = "x",
                margin = 10,
                components = {
                  {
                    type = "icp",
                    identifier = "f16/icp-landscape",
                  },
                  {
                    type = "ded",
                    identifier = "f16/ded",
                    data_channel = 3,
                  },
                },
              },
              {
                type = "split",
                direction = "x",
                margin = 10,
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
        },
      },
    },
  },
}
