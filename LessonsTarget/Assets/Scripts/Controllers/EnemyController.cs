using Assets;
using Assets.Scripts.ActiveObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    CleverEnemy curEnemy;
    // Use this for initialization
    void Start () {
        curEnemy = gameObject.AddComponent<CleverEnemy>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
