using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Bonuses;

public class Bonus_BombRadius_Controller : MonoBehaviour {

    // Use this for initialization
    BonusBombRadius bbr;
	void Start () {
		bbr = gameObject.AddComponent<BonusBombRadius>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
