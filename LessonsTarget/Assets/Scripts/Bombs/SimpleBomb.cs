using Assets.Scripts.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class SimpleBomb : BaseBomb
    {
        protected override void SetStartParameters()
        {
            explosionTimer = 4;
            explosionRadius = 1;
        }
    }
}
