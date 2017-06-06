using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Loaders
{
    abstract class GameObjectLoader : MonoBehaviour
    {
        /*public static GameObject GetPlayerPrefab(string objectName) 
        {
            return Instantiate(Resources.Load<GameObject>("Cowboy"));
            //return Instantiate(Resources.Load("Cowboy", typeof(GameObject))) as GameObject;
            //return Instantiate(Resources.Load("AngryPlayer", typeof(GameObject))) as GameObject;
        }*/
        public static GameObject GetObjectsPrefabByName(string objectName)
        {
            return Instantiate(Resources.Load<GameObject>(objectName));
        }
    }
}
