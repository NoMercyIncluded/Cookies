using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookiesInTheSpace.XNA.WorldScheme
{
    class ReferenceSystem: SchemeParent
    {
        SchemeNode  root;
        SchemeBond  rootBond;
        float       inflParam;

        public ReferenceSystem(SchemeNode aRoot, SchemeBond aRootBond)
        {
            this.root = aRoot;
            this.rootBond = new SchemeBond
        }
        
        
        
        public float InfluenceRadius()
        {
            return rootBond.InflRadius;
        }
    }
}
