using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CookiesInTheSpace.XNA
{
    enum SubnodeIndex {
        INVALID = -1, NORTH_WEST = 0, NORTH_EAST = 1, SOUTH_WEST=2, SOUTH_EAST=3
    }

    class SpaceTreeNode
    {        
        private float _size;
        public float Size
        {
            get { return _size; }
        }

        public Vector2 CenterOfMassPosition;
        public float Mass;
        public Vector2 Position;

        public SpaceObject spaceObject;

        public SpaceTreeNode Parent;

        public SpaceTreeNode[] subnodes;
        //--------------------------------------------------------

        public SpaceTreeNode(SpaceTreeNode aParent, Vector2 aPosition, float aSize)
        {
            Parent = aParent;
            Position = aPosition;
            _size = aSize;
        }

        public void addMass(float mass, Vector2 position) 
        {
            if (mass == 0)
                return;

            if (Mass + mass != 0)
            {
                CenterOfMassPosition.X = (CenterOfMassPosition.X * Mass + position.X * mass) /
                                            (Mass + mass);

                CenterOfMassPosition.Y = (CenterOfMassPosition.Y * Mass + position.Y * mass) /
                                        (Mass + mass);
            }
            Mass += mass;    
        }

        public void removeMass(float mass, Vector2 position)
        {
            addMass(-mass, position);
        }

        public bool containsPosition(Vector2 position)
        {
            if(position.X < this.Position.X || position.Y < this.Position.Y)
                return false;

            if (position.X >= this.Position.X + Size || position.Y >= this.Position.Y + Size)
                return false;

            return true;
        }

        public SpaceTreeNode createSubnode(SubnodeIndex idx)
        {
            if (idx == SubnodeIndex.INVALID)
                return null;

            if (subnodes == null)
                subnodes = new SpaceTreeNode[4];

            if (subnodes[(int)idx] != null)
                return null;

            switch (idx)
            {
                case SubnodeIndex.NORTH_WEST:
                    if (subnodes[(int)idx] == null)
                       return subnodes[(int)idx] = new SpaceTreeNode(this, new Vector2(Position.X, Position.Y), Size / 2);

                    break;
                case SubnodeIndex.NORTH_EAST:
                    if (subnodes[(int)idx] == null)
                        return subnodes[(int)idx] = new SpaceTreeNode(this, new Vector2(Position.X + Size / 2, Position.Y), Size / 2);

                    break;
                case SubnodeIndex.SOUTH_WEST:
                    if (subnodes[(int)idx] == null)
                        return subnodes[(int)idx] = new SpaceTreeNode(this, new Vector2(Position.X, Position.Y + Size / 2), Size / 2);

                    break;
                case SubnodeIndex.SOUTH_EAST:
                    if (subnodes[(int)idx] == null)
                        return subnodes[(int)idx] = new SpaceTreeNode(this, new Vector2(Position.X + Size / 2, Position.Y + Size / 2), Size / 2);

                    break;
            }

            return null;
        }

        public void removeSubnode(SubnodeIndex idx)
        {
            if (subnodes == null)
                return;

            if (subnodes[(int)idx] == null)
                return;

            subnodes[(int)idx].Parent = null;
            subnodes[(int)idx] = null;
        }

        public SubnodeIndex getSubnodeIndex(Vector2 position) {
            if (!containsPosition(position))
                return SubnodeIndex.INVALID;
            
            if (position.X < Position.X + Size / 2)
            {
                if (position.Y < Position.Y + Size / 2)
                {
                   return SubnodeIndex.NORTH_WEST;
                }
                else
                {
                    return SubnodeIndex.SOUTH_WEST;
                }
            }
            else
            {
                if (position.Y < Position.Y + Size / 2)
                {
                    return SubnodeIndex.NORTH_EAST;
                }
            }

            return SubnodeIndex.SOUTH_EAST;
        }

        public bool hasSubnodes() 
        {
            if(subnodes == null)
                return false;    

            foreach(SpaceTreeNode subnode in subnodes)
            {
                if (subnode != null)
                    return true;
            }

            return false;
        }

        public bool isEmpty() 
        {
            return !hasSubnodes() && spaceObject == null;
        }



    }
}
