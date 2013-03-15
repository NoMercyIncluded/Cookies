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
        private const double G = 6.67e-11;
        private const float phi = 0.5F;

        SpaceTree STree;
        World PhisicsWorld;

        List<SpaceObject> SpaceObjects = new List<SpaceObject>();

        //-----------------------------------------------------------------------

        public SpaceTreeIterationCallbackResult applyGravityForcesCallback(SpaceTreeNode spaceTreeNode, object userData) 
        {
            SpaceObject spaceObject = (SpaceObject)userData;

            if (spaceTreeNode.spaceObject == spaceObject || spaceTreeNode.Mass == 0)
                return SpaceTreeIterationCallbackResult.CONTINUE_NEXT;

            double dist = Vector2.Distance(spaceObject.Position, spaceTreeNode.CenterOfMassPosition);

            if (spaceTreeNode.Size / dist < phi || !spaceTreeNode.hasSubnodes())
            {
                //If node size is irrellevant in comparison to distance between CenterOffMassPosition and given spaceObject
                Vector2 vec = spaceTreeNode.CenterOfMassPosition - spaceObject.Position;
                vec = Vector2.Normalize(vec);
                vec.X = (float)((spaceObject.Mass * spaceTreeNode.Mass / (dist * dist)) * G) * vec.X;
                vec.Y = (float)((spaceObject.Mass * spaceTreeNode.Mass / (dist * dist)) * G) * vec.Y;

                spaceObject.PhisicsBody.ApplyForce(vec, spaceObject.Position);

                return SpaceTreeIterationCallbackResult.CONTINUE_NEXT;
            }
            else
            {
                //If object is too close to CenterOfMassPosition to skip calculations
                return SpaceTreeIterationCallbackResult.CONTINUE_STEP_INTO;
            }

        }

        public void applyGravityForces()
        {
            foreach (SpaceObject so in SpaceObjects) 
            {
                STree.iterateTree(this.applyGravityForcesCallback, so);
            }
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
