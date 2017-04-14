using Assets.Scripts.PassiveObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardBombController : MonoBehaviour {

    HardBomb bomb;
    // Use this for initialization
    void Start()
    {
        bomb = gameObject.AddComponent<HardBomb>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
