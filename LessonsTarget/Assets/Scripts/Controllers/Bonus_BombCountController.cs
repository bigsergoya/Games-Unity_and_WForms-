using Assets.Scripts.Bonuses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_BombCountController : MonoBehaviour {

    // Use this for initialization
    BonusBombCount bbc;
	// Use this for initialization
	void Start()
    {
        bbc = gameObject.AddComponent<BonusBombCount>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
