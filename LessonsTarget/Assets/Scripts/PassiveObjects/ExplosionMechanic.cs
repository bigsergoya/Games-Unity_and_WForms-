using Assets.Scripts.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace Assets.Scripts.PassiveObjects
{
    class ExplosionMechanic : MonoBehaviour
    {

        private void Start()
        {
            
        }
        bool ObjectCanBeExploded(string tag)
        {
            return ((tag != "UnbreakingCube") 
                && (tag != "FieldCell") 
                    && (tag != "Player")
                        && (tag != "Enemy")
                            && (tag != "Bomb"));
        }
        private IEnumerator OnParticleCollision(GameObject otherObject)
        {
            //print("Hit");
            if (enabled)
                if (ObjectCanBeExploded(otherObject.tag))
                {
                    BaseExplosion.ObjectDestroyEvent(otherObject.transform.position);
                    Destroy(otherObject.gameObject);
                }
            yield return null;
        }
    }
}
