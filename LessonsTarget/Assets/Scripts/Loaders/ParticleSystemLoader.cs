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
        public static ParticleSystem GetActiveObjectDeathExplosion() 
        {
            return Instantiate(Resources.Load("Particle System Unit", typeof(ParticleSystem))) as ParticleSystem;
        }
        public static ParticleSystem GetMainExplosion() 
        {
            return Instantiate(Resources.Load("Particle System Main", typeof(ParticleSystem))) as ParticleSystem;
        }
    }
}
