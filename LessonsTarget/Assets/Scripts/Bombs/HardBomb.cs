using Assets.Scripts.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.PassiveObjects
{
    class HardBomb : BaseBomb
    {
        protected override void SetStartParameters()
        {
            explosionTimer = 4;
            explosionRadius = 3;
        }
    }
}
