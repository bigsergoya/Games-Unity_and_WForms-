using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Bonuses;
public class Bonus_SpeedController : MonoBehaviour {

    // Use this for initialization
    BonusSpeed bs;
	void Start () {
		bs = gameObject.AddComponent<BonusSpeed>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
