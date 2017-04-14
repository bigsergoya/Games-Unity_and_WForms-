using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Loaders
{
    abstract class BonusLoader : MonoBehaviour
    {
        public static GameObject GetSpeedBonus() 
        {
            return Instantiate(Resources.Load("Bonus_Speed", typeof(GameObject))) as GameObject;
        }
        public static GameObject GetRadiusBonus() 
        {
            return Instantiate(Resources.Load("Bonus_BombRadius", typeof(GameObject))) as GameObject;
        }
        public static GameObject GetNoClipBonus() 
        {
            return Instantiate(Resources.Load("Bonus_NoClip", typeof(GameObject))) as GameObject;
        }
        public static GameObject GetBombCountBonus()
        {
            return Instantiate(Resources.Load("Bonus_BombCount", typeof(GameObject))) as GameObject;
        }
    }
}
