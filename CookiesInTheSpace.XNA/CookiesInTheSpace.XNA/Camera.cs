using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CookiesInTheSpace.XNA
{
    class Camera
    {
        public float PixelsPerUnit = 1;
        public float UnitsPerPixel {
            get { return 1 / PixelsPerUnit; }
            set { PixelsPerUnit = 1 / value; }
        }

        public Vector2 Position;
    }
}
