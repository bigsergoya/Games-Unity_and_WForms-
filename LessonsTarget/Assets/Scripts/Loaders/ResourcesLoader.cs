using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
namespace Assets
{
    abstract class ResourcesLoader : MonoBehaviour
    {
        protected GameObject GetBreakingWallPrefab()
        {
            return Instantiate(Resources.Load("BreakabbleCube", typeof(GameObject))) as GameObject;            
        }
        protected GameObject GetUnbreakingWallPrefab()
        {
            return Instantiate(Resources.Load("UnbreakeabbleCube", typeof(GameObject))) as GameObject;
        }
        protected GameObject GetFieldPrefab()
        {
            return Instantiate(Resources.Load("FieldPlane", typeof(GameObject))) as GameObject;
        }
    }
}
