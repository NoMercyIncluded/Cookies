using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookiesInTheSpace.XNA.WorldScheme
{
    interface SchemeParent
    {
        protected Dictionary<SchemeBond, SchemeNode> children;
        public Boolean IsReferenceSystem();
        public Boolean addChild(SchemeNode child, float orbit, float angle, Boolean spin);
        public void removeChild(SchemeNode child);
        public int getChildCount();
        public SchemeBond getChild(int index); //since Bond should have reference to Node
        
        // orbit tools - later, for now Bool on addChild is enough.
    }
}
