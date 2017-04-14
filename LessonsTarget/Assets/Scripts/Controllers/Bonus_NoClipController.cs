using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Bonuses;

public class Bonus_NoClipController : MonoBehaviour {

    BonusNoClip bnc;
	void Start () {
        bnc = gameObject.AddComponent<BonusNoClip>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
