using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Box2D.XNA;

namespace CookiesInTheSpace.XNA
{
    class Planet : SpaceObject
    {
        public float orbitRadius;
        public float linearVelocity;
        
        public Planet(Body PhisicsBody, Vector2[] ShapeDefinition):base(PhisicsBody, ShapeDefinition) 
        {}

        public static Shape createShape(Vector2[] shapePoints)
        {
            CircleShape shape = new CircleShape();

            shape._radius = shapePoints[0].X;

            return shape;
        }
    }
}
