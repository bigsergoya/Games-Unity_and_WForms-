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
        const int scoresForExplodedObject = 5;
        private void Start()
        {
            
        }
        bool ObjectCanBeExploded(string tag)
        {
            /*return ((tag != "UnbreakingCube") 
                && (tag != "FieldCell") 
                    && (tag != "Player")
                        && (tag != "Enemy")
                            && (tag != "Bomb")
                                && (tag != "ExitCube"));*/
            return ((tag == "BreakingCube")
                ||(tag == "Bonus_BombCount") 
                    || (tag == "Bonus_Radius")
                        || (tag == "Bonus_NoClip")
                            || (tag == "Bonus_Speed"));
        }
        private IEnumerator OnParticleCollision(GameObject otherObject)
        {
            if (enabled)
                if (ObjectCanBeExploded(otherObject.tag))
                {
                    BaseExplosion.ObjectDestroyEvent(otherObject.transform.position);
                    Destroy(otherObject.gameObject);
                    BaseWorkingWithGame.PrintNewScores(scoresForExplodedObject);
                }
            yield return null;
        }
    }
}
