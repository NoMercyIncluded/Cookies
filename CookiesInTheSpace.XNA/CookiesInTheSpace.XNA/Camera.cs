using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CookiesInTheSpace.XNA
{
    class Camera
    {
        SpaceObject LockedOnObject = null;

        public float PixelsPerUnit = 1;
        public float UnitsPerPixel
        {
            get { return 1 / PixelsPerUnit; }
            set { PixelsPerUnit = 1 / value; }
        }

        public Vector2 ViewFrameSize;
        public Vector2 Position;
        public Vector2 OldPosition;
        public Vector2 PositionShift;

        public Camera(int viewFrameSizeX, int viewFrameSizeY)
        {
            ViewFrameSize.X = viewFrameSizeX;
            ViewFrameSize.Y = viewFrameSizeY;
        }

        public void SetViewFrameSize(float x, float y)
        {
            if (x > 0 && y > 0)
            {
                ViewFrameSize.X = x;
                ViewFrameSize.Y = y;
            }
        }

        public void FreeLock()
        {
            if (LockedOnObject != null)
            {
                OldPosition.X = LockedOnObject.Position.X;
                OldPosition.Y = LockedOnObject.Position.Y;
                ShiftReset();
                LockedOnObject = null;
            }
        }

        private void ShiftPosition(float x, float y)
        {
            PositionShift.X += x;
            PositionShift.Y += y;
        }
        private void ShiftReset()
        {
            PositionShift.X = 0;
            PositionShift.Y = 0;
        }

        public void Update(SpaceObject lockOnThis)
        {
            LockedOnObject = lockOnThis;
            ShiftReset();
            Update();
        }
        public void Update(float x, float y)
        {
            ShiftPosition(x, y);
            Update();
        }
        public void Update()
        {
            if (LockedOnObject != null)
            {
                Position.X = LockedOnObject.Position.X + PositionShift.X * UnitsPerPixel - (ViewFrameSize.X * UnitsPerPixel) / 2;
                Position.Y = LockedOnObject.Position.Y + PositionShift.Y * UnitsPerPixel - (ViewFrameSize.Y * UnitsPerPixel) / 2;
            }
            else
            {
                Position.X = OldPosition.X + PositionShift.X * UnitsPerPixel -(ViewFrameSize.X * UnitsPerPixel) / 2;
                Position.Y = OldPosition.Y + PositionShift.Y * UnitsPerPixel -(ViewFrameSize.Y * UnitsPerPixel) / 2;
            }
        }

        public void Zoom(int multiplier)
        {
            if (multiplier > 0 && UnitsPerPixel > 1)
            {
                Position.X += (float)(0.03 * UnitsPerPixel * 1000 / 2);
                Position.Y += (float)(0.03 * UnitsPerPixel * 1000 / 2);
                UnitsPerPixel *= 0.97f;
            }
            else if (multiplier < 0)
            {
                Position.X -= (float)(0.03 * UnitsPerPixel * 1000 / 2);
                Position.Y -= (float)(0.03 * UnitsPerPixel * 1000 / 2);
                UnitsPerPixel *= 1.03f;
            }

        }
    }

}
