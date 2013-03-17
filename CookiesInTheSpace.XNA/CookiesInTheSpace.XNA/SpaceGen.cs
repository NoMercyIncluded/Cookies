using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookiesInTheSpace.XNA
{
    class SpaceGen
    {
        public static Space generate(float size, float massFactor, int maxNumberOfObjects, int maxDepthLevel, 
            float minSolarDensity, float maxSolarDensity, float minSolarRadius, float maxSolarRadius, 
            float radiusScalePerLevel, float orbitRadiusPerMass, int maxNumberOfSatelites) 
        {
            Space space = new Space(size);

            while (space.SpaceObjects.Count < maxNumberOfObjects) {
                
            }    


            return space;
        }

    }
}
