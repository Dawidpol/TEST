using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Color
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public Color()
        {
            R = G = B = 0.0f;
            A = 1.0f;
        }

        public Color(float r, float g, float b)
        {
            A = 1.0f;
        }

        public Color(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color(byte r, byte g, byte b)
        {
            R = r / 255.0f;
            G = g / 255.0f;
            B = b / 255.0f;
            A = 1.0f;
        }
        public Color(byte r, byte g, byte b, byte a)
        {
            R = r / 255.0f;
            G = g / 255.0f;
            B = b / 255.0f;
            A = a / 255.0f;
        }
    }
}
