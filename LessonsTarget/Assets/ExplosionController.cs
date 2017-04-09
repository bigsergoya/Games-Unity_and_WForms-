using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.PassiveObjects;
public class ExplosionController : MonoBehaviour {

    ExplosionMechanic curExplosion;
    // Use this for initialization
    void Start () {
        curExplosion = gameObject.AddComponent<ExplosionMechanic>();
    }
    private void OnParticleCollision(GameObject other)
    {
        print("Booooooom1");
    }
}
