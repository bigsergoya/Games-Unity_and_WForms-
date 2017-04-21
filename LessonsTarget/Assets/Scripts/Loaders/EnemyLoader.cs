using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Loaders
{
    abstract class EnemyLoader : MonoBehaviour
    {
        public static GameObject GetCleverEnemyPrefab()
        {
            //return Instantiate(Resources.Load("KindEnemy", typeof(GameObject))) as GameObject;
            return Instantiate(Resources.Load("KindSkeleton", typeof(GameObject))) as GameObject;
        }
        public static GameObject GetSimpleEnemyPrefab()
        {
            return Instantiate(Resources.Load("KindEnemy", typeof(GameObject))) as GameObject;
            //return Instantiate(Resources.Load("KindSkeleton", typeof(GameObject))) as GameObject;
        }
    }
}
