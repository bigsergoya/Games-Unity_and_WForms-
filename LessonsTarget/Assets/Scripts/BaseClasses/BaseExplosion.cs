using Assets.Scripts.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    abstract class BaseExplosion : ParticleSystemLoader
    {
        static ParticleSystem explosion;
        static public void DestroyEvent(Vector3 position)
        {
            explosion = ParticleSystemLoader.GetExplosionByName("Particle System Unit");
            explosion.transform.position = position;
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration);
        }
            static public void PlayerDestroyEvent(Vector3 position)
        {
            DestroyEvent(position);
        }
        static public void EnemyDestroyEvent(Vector3 position)
        {
            DestroyEvent(position);
        }
        static public void ObjectDestroyEvent(Vector3 position)
        {
            DestroyEvent(position);
        }
        static public void MainExplosionEvent(Vector3 position, float explosionTimer,
            float explosionRadius)
        {
            explosion = ParticleSystemLoader.GetExplosionByName("Particle System Main");

            explosion.transform.position = position;

            foreach (var ob in explosion.GetComponentsInChildren<ParticleSystem>())
            {
                var x = ob.main;
                x.startDelay = explosionTimer;
                x.startSpeed = explosionRadius;
                ob.Play();
            }
            Destroy(explosion.gameObject, explosionTimer+1);
        }
    }
}
