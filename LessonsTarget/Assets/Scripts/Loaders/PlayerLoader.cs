using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Loaders
{
    abstract class PlayerLoader : MonoBehaviour
    {
        public static GameObject GetPlayerPrefab() 
        {
            return Instantiate(Resources.Load("Cowboy", typeof(GameObject))) as GameObject;
            //return Instantiate(Resources.Load("AngryPlayer", typeof(GameObject))) as GameObject;
        }
    }
}
