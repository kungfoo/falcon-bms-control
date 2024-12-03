return {
  id = "cockpit-like",
  name = "Cockpit-like layout",
  description = "A layout that mimicks a cockpit. Puts two MFDs left and right and a portrait ICP between them. This is a great layout for bigger touchscreens.",
  metadata = {
    version = "1.0",
  },
  definition = {
    screens = {
      {
        type = "screen",
        name = "Basically all you need.",
        components = {
          {
            type = "split",
            direction = "x",
            margin = 20,
            ratio = 0.33333,
            components = {
              {
                type = "mfd",
                identifier = "f16/mfd",
                metadata = {
                  id = "f16/left-mfd",
                },
              },
              {
                type = "split",
                direction = "x",
                ratio = 0.5,
                margin = 20,
                components = {
                  {
                    type = "split",
                    direction = "y",
                    ratio = 0.33333,
                    margin = 20,
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
                  {
                    type = "mfd",
                    identifier = "f16/mfd",
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
