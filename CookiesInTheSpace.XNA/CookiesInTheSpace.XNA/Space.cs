using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2D.XNA;
using Microsoft.Xna.Framework;

namespace CookiesInTheSpace.XNA
{
    class Space
    {
        SpaceTree STree;
        World PhisicsWorld;

        List<SpaceObject> SpaceObjects = new List<SpaceObject>();

        //-----------------------------------------------------------------------

        public SpaceTreeIterationCallbackResult applyGravityForcesCallback(SpaceTreeNode spaceTreeNode) 
        {
            return SpaceTreeIterationCallbackResult.BREAK;
        }

        public void applyGravityForces()
        {
            
        }

        public SpaceObject createObject(Vector2[] shapePoints, float density, Vector2 position)
        {
            return createObject(shapePoints, density, position, typeof(SpaceObject));
        }

        public SpaceObject createObject(Vector2[] shapePoints, float density, Vector2 position, Type spaceObjectType) 
        {
            if (!typeof(SpaceObject).IsAssignableFrom(spaceObjectType)) {
                return null;
            }

            var shape = new PolygonShape();

            shape.Set(shapePoints, shapePoints.Length);

            var fd = new FixtureDef();

            fd.shape = shape;

            fd.restitution = 0f;

            fd.friction = 1f;

            fd.density = density;

            BodyDef bd = new BodyDef();

            bd.type = BodyType.Dynamic;

            Body body = PhisicsWorld.CreateBody(bd);

            body.CreateFixture(fd);
            
            Object [] pars = { body, shapePoints };

            SpaceObject result = (SpaceObject)Activator.CreateInstance(spaceObjectType, pars);

            body.SetUserData(result);
            result.PhisicsBody = body;

            result.OldPosition = position;
            result.Position = position;

            SpaceObjects.Add(result);
            STree.addObject(result);

            return result;
        }

        public void destroyObject(SpaceObject spaceObject) 
        {
            SpaceObjects.Remove(spaceObject);
            STree.removeObject(spaceObject);
            PhisicsWorld.DestroyBody(spaceObject.PhisicsBody);

            spaceObject.PhisicsBody = null;
        }

    }
}
