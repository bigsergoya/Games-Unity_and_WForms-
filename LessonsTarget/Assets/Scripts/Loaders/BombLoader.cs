using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Loaders
{
    abstract class BombLoader : MonoBehaviour
    {
        public static GameObject GetSimpleBomb() //Костыль. Лучше бы иначе!
        {
            return Instantiate(Resources.Load("SimpleBomb", typeof(GameObject))) as GameObject;
        }
        public static GameObject GetHardBomb() //Костыль. Лучше бы иначе!
        {
            return Instantiate(Resources.Load("HardBomb", typeof(GameObject))) as GameObject;
        }
    }
}
