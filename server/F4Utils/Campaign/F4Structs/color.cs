using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
namespace F4Utils.Campaign.F4Structs
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public class color
    {
        public float r, g, b;
        public float h, s, v;
        public float X, Y, Z;
        public float x, y, L;

        private const float SMALL_NUMBER		=(1e-8f);
        private const float KINDA_SMALL_NUMBER	=(1e-4f);
	    public color()
	    {
		    r = g = b = h = s = v = X = Y = Z = x = y = L = 0f;
	    }
        public color(Stream stream)
        {
            Read(stream);
        }

        private void Read(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                r = reader.ReadSingle();
                g = reader.ReadSingle();
                b = reader.ReadSingle();
                h = reader.ReadSingle();
                s = reader.ReadSingle();
                v = reader.ReadSingle();
                X = reader.ReadSingle();
                Y = reader.ReadSingle();
                Z = reader.ReadSingle();
                x = reader.ReadSingle();
                y = reader.ReadSingle();
                L = reader.ReadSingle();
            }
        }
        public void Write(Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(r);
                writer.Write(g);
                writer.Write(b);
                writer.Write(h);
                writer.Write(s);
                writer.Write(v);
                writer.Write(X);
                writer.Write(Y);
                writer.Write(Z);
                writer.Write(x);
                writer.Write(y);
                writer.Write(L);
            }
        }
	    public color(float _r,float _g,float _b):this()
	    {
		    r = _r;
		    g = _g;
		    b = _b;
	    }

	    public color(uint cw):this()
	    {
		    r = (float)((cw & 0x00ff0000) >> 16);
		    g = (float)((cw & 0x0000ff00) >> 8);
		    b = (float)(cw & 0x000000ff);

		    r /= 255.9F;
		    g /= 255.9F;
		    b /= 255.9F;
	    }

	    public void SetRGB(float _r,float _g,float _b)
	    {
		    r = _r;
		    g = _g;
		    b = _b;
	    }

	    public void SetHSV(float _h,float _s,float _v)
	    {
		    h = _h;
		    s = _s;
		    v = _v;
	    }

	    public void SetXYZ(float _x,float _y,float _z)
	    {
		    X = _x;
		    Y = _y;
		    Z = _z;
	    }

	    public void SetxyL(float _x,float _y,float _L)
	    {
		    x = _x;
		    y = _y;
		    L = _L;
	    }

	    public void XYZtoRGB()
	    {
		    //CIE XYZ tristimulus to Rec. 709 (D65 Whitepoint) RGB
		    r =  3.240479f*X - 1.537150f*Y - 0.498535f*Z;
		    g = -0.969256f*X + 1.875991f*Y + 0.041556f*Z;
		    b =  0.055648f*X - 0.204043f*Y + 1.057311f*Z;
	    }

	    public void RGBtoXYZ()
	    {
	        X =  0.412453f*r + 0.357580f*g + 0.180423f*b;
	        Y =  0.212671f*r + 0.715160f*g + 0.072169f*b;
	        Z =  0.019334f*r + 0.119193f*g + 0.950227f*b;
	    }

	    public float Maximum( float x, float y, float z )
	    {
		    if( x > y )
			    if( x > z)
				    return x;
			    else
				    return z;
		    else
			    if( y > z)
				    return y;
			    else
				    return z;
	    }

	    public float Minimum( float x, float y, float z )
	    {
		    if( x < y )
			    if( x < z)
				    return x;
			    else
				    return z;
		    else
			    if( y < z)
				    return y;
			    else
				    return z;
	    }


	    public void RGBtoHSV()
	    {	
	        float _max = Maximum(r,g,b);
		    float _min = Minimum(r,g,b);
		    v = _max;
		    s = (v != 0f) ? ((_max-_min)/_max) : 0f;

		    if(s == 0f) h = 0f;
		    else
		    {
			    float delta = _max-_min;

			    if(r == _max)
				    h = (g-b)/delta;
			    else if(g == _max)
				    h = 2f+(b-r)/delta;
			    else if(b == _max)
				    h = 4f+(r-g)/delta;

			    h *= 60;
			    if(h < 0f) h += 360f;
			    h /= 360f;
		    }
	    }

	    public void HSVtoRGB()
	    {
		    if(s == 0f || h == -1f)
		    {
			    r = g = b = v;
			    return;
		    }

		    h *= 360f;
		    h /= 60f;
		    int i = FloatToIntTrunc(h);
		    float f = h-i;
		    float p = v*(1-s);
		    float q = v*(1-s*f);
		    float t = v*(1-s*(1f-f));

		    switch(i)
		    {
			    case 0:
				    r = v;
				    g = t;
				    b = p;
				    break;
			    case 1:
				    r = q;
				    g = v;
				    b = p;
				    break;
			    case 2:
				    r = p;
				    g = v;
				    b = t;
				    break;
			    case 3:
				    r = p;
				    g = q;
				    b = v;
				    break;
			    case 4:
				    r = t;
				    g = p;
				    b = v;
				    break;
			    default:
				    r = v;
				    g = p;
				    b = q;
                    break;
		    }

	    }

	    public void xyLtoXYZ()
	    {
		    X = x * (L / y);
		    Y = L;
		    Z = (1f - x - y)*(Y/y);
	    }

	    public void xyLtoRGB()
	    {
		    xyLtoXYZ();
		    XYZtoRGB();
	    }

	    public void XYZtoHSV()
	    {
		    XYZtoRGB();
		    RGBtoHSV();
	    }

	    public void GammaCorrectRGB(float R, float G, float B)
	    {
		    r = (float)Math.Pow((double)r,(double)(1f/R));
		    g = (float)Math.Pow((double)g,(double)(1f/G));
		    b = (float)Math.Pow((double)b,(double)(1f/B));
	    }

	    public void ExposureRGB(float exposure)
	    {
		    r = 1f-(float)Math.Exp((double)(r*exposure));
		    g = 1f-(float)Math.Exp((double)(g*exposure));
		    b = 1f-(float)Math.Exp((double)(b*exposure));
	    }

	    public void ExposureV(float exposure)
	    {
		    v = 1f-(float)Math.Exp((double)(v*exposure));
	    }

	    public void ExposureL(float exposure)
	    {
		    L = 1f-(float)Math.Exp((double)(L*exposure));
	    }

	    public void ClampRGB()
	    {
		    r = Clamp(r,0f,1f);
		    g = Clamp(g,0f,1f);
		    b = Clamp(b,0f,1f);
	    }
	    public uint MakeARGB() 
	    {
		    var A = 0xFF;
		    var R = (byte)FloatToIntTrunc(r*255.9f);
		    var G = (byte)FloatToIntTrunc(g*255.9f);
		    var B = (byte)FloatToIntTrunc(b*255.9f);

		    return MAKEARGB((uint)A,(uint)R,(uint)G,(uint)B);
	    }

	    public uint MakeARGB(byte alpha) 
	    {
		    var A = alpha;
		    var R = (byte)FloatToIntTrunc(r*255.9f);
		    var G = (byte)FloatToIntTrunc(g*255.9f);
		    var B = (byte)FloatToIntTrunc(b*255.9f);

		    return MAKEARGB(A,R,G,B);
	    }

	    public bool Normalize()
	    {
		    float squareSum = r*r+g*g+b*b;

		    if(squareSum >= SMALL_NUMBER)
		    {
			    float scale = 1f/(float)Math.Sqrt(squareSum);
			    r *= scale; g *= scale; b *= scale;
			    return true;
		    }
		    else return false;
	    }

        public static color operator *(float Scale, color V)
	    {
		    return new color(V.r * Scale,V.g * Scale,V.b * Scale);
	    }

        public static color operator +(color V1, color V2) 
	    {
		    return new color(V1.r + V2.r,V1.g + V2.g,V1.b + V2.b);
	    }

        public static color operator -(color V1, color V2) 
	    {
		    return new color(V1.r - V2.r,V1.g - V2.g,V1.b - V2.b);
	    }

        public static bool operator ==(color V1, color V2) 
	    {
		    return V1.r == V2.r && V1.g==V2.g && V1.b==V2.b;
	    }

        public static bool operator !=(color V1, color V2) 
	    {
		    return V1.r != V2.r || V1.g!=V2.g || V1.b!=V2.b;
	    }


        public static color Empty { get { return new color(); } }
        private int FloatToIntTrunc(float f)
        {
            return (int)Math.Truncate((double)f);
        }
        private float Clamp(float val, float lower, float upper)
        {
            if (val < lower) return lower;
            else if (val > upper) return upper;
            else return val;
        }
        private uint MAKERGB(uint r, uint g, uint b)
        {
            return ((r << 16) + (g << 8) + b);
        }
        private uint MAKEARGB(uint a, uint r, uint g, uint b)
        {
            return ((a << 24) + (r << 16) + (g << 8) + b);
        }
        public override bool Equals(object obj)
        {
            return obj !=null && (obj is color) &&
                r == ((color)obj).r && g == ((color)obj).g && b == ((color)obj).b;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(("color {,"));
            sb.Append($"r:{r},");
            sb.Append($"g:{g},");
            sb.Append($"b:{b},");
            sb.Append($"h:{h},");
            sb.Append($"s:{s},");
            sb.Append($"v:{v},");
            sb.Append($"X:{X},");
            sb.Append($"Y:{Y},");
            sb.Append($"Z:{Z},");
            sb.Append($"x:{x},");
            sb.Append($"y:{y},");
            sb.Append($"L:{L}");
            sb.Append(("}"));
            return sb.ToString();
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

}

