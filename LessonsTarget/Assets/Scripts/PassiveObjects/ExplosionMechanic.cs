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
        float explosionRadius = 1;
        private bool m_Exploded;
        bool reset = true;
        private ObjectResetter m_ObjectResetter;
        public float resetTimeDelay = 10;

        private void Start()
        {
            
        }
        bool ObjectCanBeBreaked(string tag)
        {
            return ((tag != "UnbreakingCube") && (tag != "FieldCell"));
        }
        private IEnumerator OnParticleCollision(GameObject otherObject)
        {
            //print("Hit");
            if (enabled)
                if (ObjectCanBeBreaked(otherObject.tag))
                {
                    BaseExplosion.ObjectDestroyEvent(otherObject.transform.position);
                    Destroy(otherObject.gameObject);
                    //if (reset)
                    //{
                        //m_ObjectResetter.DelayedReset(resetTimeDelay);
                    //}
                }
            yield return null;
        }
        //private IEnumerator OnParticleCollision(Collision col)
       // {

        //}
        public void Reset()
        {
            m_Exploded = false;
        }
    }
}
