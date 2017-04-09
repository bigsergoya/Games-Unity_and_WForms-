using Assets.Scripts.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.PassiveObjects
{
    class BonusBombRadius : BaseBonus
    {
        public BonusBombRadius(float x, float z)
        {
            PlaceBonus(x, z);
        }
        protected override void PlaceBonus(float x, float z)
        {
            throw new NotImplementedException();
        }
    }
}
