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
        static public void PlayerDestroyEvent(Vector3 position)
        {
            explosion = ParticleSystemLoader.GetActiveObjectDeathExplosion();
            explosion.transform.position = position;
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration);
        }
        static public void EnemyDestroyEvent(Vector3 position)
        {
            explosion = ParticleSystemLoader.GetActiveObjectDeathExplosion();
            explosion.transform.position = position;
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration);
        }
        static public void ObjectDestroyEvent(Vector3 position)
        {
            explosion = ParticleSystemLoader.GetActiveObjectDeathExplosion();
            explosion.transform.position = position;
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration);
        }
        static public void MainExplosionEvent(Vector3 position, float explosionTimer,
            float explosionRadius)
        {
            explosion = ParticleSystemLoader.GetMainExplosion();
            
            explosion.transform.position = position;

            //var xx = explosion.main;
            //xx.startDelay = 5f;



            foreach (var ob in explosion.GetComponentsInChildren<ParticleSystem>())
            {
                //explosion.time = 3f;
                var x = ob.main;
                x.startDelay = explosionTimer;
                x.startSpeed = explosionRadius;
                ob.Play();
                //sss.startDelay = 1f;
            }
            //explosion.Play();
            Destroy(explosion.gameObject, explosionTimer+1);
        }
    }
}
