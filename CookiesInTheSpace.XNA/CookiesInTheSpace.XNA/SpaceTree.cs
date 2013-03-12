using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CookiesInTheSpace.XNA
{
    enum SpaceTreeIterationCallbackResult 
    { 
        BREAK, CONTINUE_NEXT, CONTINUE_STEP_INTO, CONTINUE_STEP_OUT;
    }

    class SpaceTree
    {
        public float Size
        {
            get { return root.Size; }
        }

        SpaceTreeNode root;

        //-----------------------------------------------------------------------

        public SpaceTree(float Size) { 
            this.root = new SpaceTreeNode(null, new Vector2(0,0), Size);
        }

        private void addMassToBranch(SpaceTreeNode node, float Mass, Vector2 Position) 
        { 
            if(Mass == 0)
                return;

            while (node != null)
            {
                node.addMass(Mass, Position);
                node = node.Parent;
            }
        }

        private void removeMassFromBranch(SpaceTreeNode node, float Mass, Vector2 Position)
        {
            addMassToBranch(node, -Mass, Position);
        }

        public void addObject(SpaceObject spaceObject) 
        { 
            SpaceTreeNode node = getNodeByPosition(spaceObject.Position);
            if(node.spaceObject == null)
            {
                node.spaceObject = spaceObject;
                addMassToBranch(node, spaceObject.Mass, spaceObject.Position);
            } else {
                //TODO if there is already some object.
            }

        }

        public void removeObject(SpaceObject spaceObject) 
        { 
            SpaceTreeNode node = getNodeByPosition(spaceObject.Position);

            
        }

        public SpaceTreeNode getNodeByPosition(Vector2 Position) 
        {
            SpaceTreeNode node = root;
            while(node != null){
                SubnodeIndex idx = node.getSubnodeIndex(Position);
                if(idx == SubnodeIndex.INVALID)
                {
                    node = null;
                } 
                else 
                {
                    node=node.subnodes[(int)idx];

                    if(!node.hasSubnodes())
                        break;
                }
            }
            return node;
        }

        public delegate SpaceTreeIterationCallbackResult SpaceTreeIterationCallback(SpaceTreeNode spaceTreeNode);

        public void iterateTree(SpaceTreeIterationCallback callback)
        {
            SpaceTreeNode node = root;
            SpaceTreeIterationCallbackResult res=SpaceTreeIterationCallbackResult.BREAK;
            do{
                res=callback(node);

                //TODO implement iteration!!!
            }while(node != null && res != SpaceTreeIterationCallbackResult.BREAK);
        }

        public SpaceObject[] queryObjects(Vector2 Position1, Vector2 Position2)
        {
            return queryObjects(Position1, Position2, typeof(SpaceObject));
        }

        public SpaceObject[] queryObjects(Vector2 Position1, Vector2 Position2, Type spaceObjectTypeFilter)
        {
            return new SpaceObject[1];
        }

        public void updatePositions()
        {
        
        }

    }
}
