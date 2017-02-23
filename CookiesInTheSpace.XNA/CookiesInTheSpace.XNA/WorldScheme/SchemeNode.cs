using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CookiesInTheSpace.XNA.WorldScheme
{
    class SchemeNode : SchemeComponent
    {
        public static float calculateInfluenceRadius(float mass, float density, float influenceParameter)
        {
            return 0f;
        }

        protected SchemeNode parent;
        private Boolean allowChildInflParam;
        private float inflParam;
        private float inflRadius;
        private SortedDictionary<SchemeBond,SchemeNode> children;


        // <PROPERTY GETTERS & SETTERS ~~~~~~~~~~~~~~~~~~~~~>
        public Boolean AllowChildInfleParam
        {
            get
            {
                return allowChildInflParam;
            }   
            set
            {
                this.allowChildInflParam = value;
            }
        }

        public float InflParam
        {
            get
            {
                if (parent != null && ! parent.allowChildInflParam){
                    return parent.InflParam;
                }
                else return this.inflParam;
            }
            set
            {
                this.inflParam = value;
                if (parent != null){
                    if(parent.allowChildInflParam){
                        refreshInfluenceRadius();
                        if (!this.allowChildInflParam)
                        {
                            refreshChildrenInfluenceRadius();
                        }
                    }
                }          
            }
        }

        public float InflRadius
        {
            get
            {
                return this.inflRadius;
            }
        }
        // </PROPERTY GETTERS & SETTERS ~~~~~~~~~~~~~~~~~>
        // PARENT PROPERTY OVERRIDES ~~~~~~~~~~~~~~~~~~~~~~
        override public float Radius
        {
            get
            {
                return this.radius;
            }
            set
            {
                this.radius = value;
                refreshInfluenceRadius();
            }
        }

        override public float Density
        {
            get
            {
                return this.density;
            }
            set
            {
                this.density = value;
                refreshInfluenceRadius();
            }
        }
        // END PARENT PROPERTY OVERRIDES ~~~~~~~~~~~~~~~~~~

        public Boolean hasChildren()
        {
            return children.Count > 0;
        }

        //public SortedDictionary<SchemeBond,SchemeNode> getChildren()
        //{
        //    return children;
        //}

        //public void addChildren(SchemeNode node)
        //{
        //    children.Add(node);
        //}

        public float refreshInfluenceRadius()
        {
            this.inflRadius = 0;
            return this.inflRadius;
        }

        public void refreshChildrenInfluenceRadius()
        {
            //foreach (SchemeNode scheme in children){
            //    scheme.refreshInfluenceRadius();
            //}
        }



    }
}
