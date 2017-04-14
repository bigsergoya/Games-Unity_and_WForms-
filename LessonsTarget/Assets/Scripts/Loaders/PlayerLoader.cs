using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Loaders
{
    abstract class PlayerLoader : MonoBehaviour
    {
        public static GameObject GetPlayerPrefab() //А можно ли статик юзать в абстрактном классе?
        {
            return Instantiate(Resources.Load("CowBoy", typeof(GameObject))) as GameObject;
        }
    }
}
