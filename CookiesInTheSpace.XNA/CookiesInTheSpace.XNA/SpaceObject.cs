using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Box2D.XNA;

namespace CookiesInTheSpace.XNA
{
    class SpaceObject
    {
        private Vector2[] _shapeDefinition;
        public Vector2[] ShapeDefinition {
            get { return _shapeDefinition; }
        }

        //DrawingData

        public Space GameSpace;

        public Body PhisicsBody;

        public float Mass {
            get { return PhisicsBody.GetMass(); } 
        }

        public Vector2 Position {
            get { return PhisicsBody.Position; }
            set { PhisicsBody.Position = value; }
        }

        public Vector2 OldPosition;

        public float Rotation
        {
            get { return PhisicsBody.Rotation; }
            set { PhisicsBody.Rotation = value; }
        }

        //------------------------------------------------------------------

        public SpaceObject(Body PhisicsBody, Vector2[] ShapeDefinition) {
            this.PhisicsBody = PhisicsBody;
            this._shapeDefinition = ShapeDefinition;
        }
    }
}
