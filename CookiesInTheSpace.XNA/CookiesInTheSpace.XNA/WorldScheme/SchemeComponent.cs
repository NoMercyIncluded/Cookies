using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookiesInTheSpace.XNA.WorldScheme
{
    class SchemeComponent
    {
        protected float radius;
        public float Radius
        {
            public get
            {
                return this.radius;
            }
            
            virtual public set
            {
                this.radius = value;
            }
        }
        protected float density;
        public float Density
        {
            public get
            {
                return this.density;
            }
            virtual public set
            {
                this.density = value;
            }
        }
        public float velocity;
        public string graphicAsset;
        public string name; 
    }
}
