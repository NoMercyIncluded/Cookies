using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CookiesInTheSpace.XNA
{
    enum SpaceTreeIterationCallbackResult 
    {
        BREAK = 0, CONTINUE_STEP_OUT = 1, CONTINUE_NEXT = 2, CONTINUE_STEP_INTO = 3
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
            //Get proposed node
            SpaceTreeNode node = getNodeByPosition(spaceObject.Position);
            
            //If there is no object just put new one and update Center Of Mass.
            if(node.spaceObject == null)
            {
                node.spaceObject = spaceObject;
                addMassToBranch(node, spaceObject.Mass, spaceObject.Position);
            } 
            else 
            {
                //if there is object store it, and remove temporary from branch
                SpaceObject oldObject = node.spaceObject;
                node.spaceObject = null;
                removeMassFromBranch(node, spaceObject.Mass, spaceObject.Position);

                //Now we have 2 objects to put into subtree.
                SubnodeIndex s1;
                SubnodeIndex s2;
                SpaceTreeNode currentNode = node;

                //As long as both objects would go into the same space quarter we need to go deeper creating new subnode.
                do
                {
                    s1 = currentNode.getSubnodeIndex(spaceObject.Position);
                    s2 = currentNode.getSubnodeIndex(oldObject.Position);

                    currentNode = currentNode.createSubnode(s1);
                } while (s1 == s2);
                
                currentNode = currentNode.Parent;

                //When we have unique SubNodeIndex for both objects just create additional subnode...
                currentNode.createSubnode(s2);

                //and store both objects into tree
                currentNode.subnodes[(int)s1].spaceObject = spaceObject;
                currentNode.subnodes[(int)s2].spaceObject = oldObject;
                addMassToBranch(currentNode.subnodes[(int)s1], spaceObject.Mass, spaceObject.Position);
                addMassToBranch(currentNode.subnodes[(int)s2], oldObject.Mass, oldObject.Position);
            }

        }

        public void removeObject(SpaceObject spaceObject) 
        { 
            SpaceTreeNode node = getNodeByPosition(spaceObject.Position);

            //please don't delete root node
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

            //ItarationStart!
            do{
                //First of all call callback from user on current node
                res=callback(node);

                //if user wants us to go deeper
                if ((int)res >= (int)SpaceTreeIterationCallbackResult.CONTINUE_STEP_INTO)
                {
                    //check if we can go deeper
                    if(node.hasSubnodes())
                    {
                        //find first subnode that is not null
                        bool found = false;
                        for(int i = 0; i<4;i++)
                        {
                            if(node.subnodes[i] != null)
                            {
                                //store new node, end loop
                                node = node.subnodes[i];
                                found = true;
                               
                            }
                        }
                        if(found)
                            break;
                    }
                }

                //we can't go deeper, we'll try to go to neghbour
                if ((int)res >= (int)SpaceTreeIterationCallbackResult.CONTINUE_NEXT)
                {
                    if (node.Parent != null)
                    {
                        //find next siblings that is not null
                        bool found = false;
                        for (int i = (int)node.Parent.getSubnodeIndex(node.Position) + 1; i < 4; i++)
                        {
                            if (node.subnodes[i] != null)
                            {
                                //if found just store new node and break loop.
                                found = true;
                                node = node.Parent.subnodes[i];
                            }
                        }
                        if (found)
                            break;
                    };
                }

                //we can't go deeper and/or to the neighbour, lets step to the parents neighbour.
                if ((int)res >= (int)SpaceTreeIterationCallbackResult.CONTINUE_STEP_OUT)
                {
                    //no point of doing anything if there is no parent
                    if (node.Parent == null)
                        return;

                    bool found = false;
                    //While we can lets go up the tree
                    while (node.Parent.Parent != null)
                    {
                        //and find our parents neighbour
                        for (int i = (int)node.Parent.Parent.getSubnodeIndex(node.Parent.Position) + 1; i < 4; i++)
                        {
                            if (node.subnodes[i] != null)
                            {
                                //if found just store and break as usually
                                found = true;
                                node = node.Parent.Parent.subnodes[i];
                            }
                        }
                        if (found)
                            break;

                        //if not found go up the tree and repeat searching loop
                        node = node.Parent;
                    }

                    //we got up to the root, so its just end of whole iteration
                    if (!found)
                        return;
                }
                

                        
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
