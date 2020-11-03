local Triangles = {}

function Triangles:fit(direction, x, y, w, h)
  local pad = w/3.5
  if direction == "UP" then
    return {
      p1 = {
        x = (x + x+w)/2,
        y = y + pad
      },
      p2 = {
        x = x + w - pad,
        y = y + h - pad
      },
      p3 = {
        x = x + pad,
        y = y + h - pad
      }
    }
  end
  if direction == "DOWN" then
    return {
      p1 = {
        x = (x + x+w)/2,
        y = y + h - pad
      },
      p2 = {
        x = x + pad,
        y = y + pad
      },
      p3 = {
        x = x + w - pad,
        y = y + pad
      }
    }
  end
  if direction == "LEFT" then
    return {
      p1 = {
        x = x+pad,
        y = (y + y+h)/2
      },
      p2 = {
        x = x + w - pad,
        y = y + pad
      },
      p3 = {
        x = x + w - pad,
        y = y + h - pad
      }
    }
  end
  if direction == "RIGHT" then
    return {
      p1 = {
        x = x + w - pad,
        y = (y + y+h)/2
      },
      p2 = {
        x = x + pad,
        y = y + h - pad
      },
      p3 = {
        x = x + pad,
        y = y + pad
      }
    }
  end
end

return Triangles