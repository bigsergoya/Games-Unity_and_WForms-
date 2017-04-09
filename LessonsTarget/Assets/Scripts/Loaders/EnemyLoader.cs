using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Loaders
{
    abstract class EnemyLoader : MonoBehaviour
    {
        public static GameObject GetEnemyPrefab()
        {
            return Instantiate(Resources.Load("KindEnemy", typeof(GameObject))) as GameObject;
        }
    }
}
