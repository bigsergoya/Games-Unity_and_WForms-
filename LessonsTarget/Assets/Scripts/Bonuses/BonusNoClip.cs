using Assets.Scripts.BaseClasses;
using Assets.Scripts.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Bonuses
{
    class BonusNoClip : BaseBonus
    {
        public BonusNoClip(float x, float z)
        {
            PlaceBonus(x, z);
        }
        protected override void PlaceBonus(float x, float z)
        {
            GameObject gameModelsObject = GameObjectLoader.GetObjectsPrefabByName("Bonus_NoClip");
            gameModelsObject.transform.position = new Vector3(x, 1.0f, z);
        }
    }
}
