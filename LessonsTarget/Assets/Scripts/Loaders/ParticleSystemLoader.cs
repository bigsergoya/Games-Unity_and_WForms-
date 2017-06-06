using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Loaders
{
    abstract class ParticleSystemLoader : MonoBehaviour
    {
        public static ParticleSystem GetExplosionByName(string objectName)
        {
            return Instantiate(Resources.Load(objectName, typeof(ParticleSystem))) as ParticleSystem;
        }
    }
}
