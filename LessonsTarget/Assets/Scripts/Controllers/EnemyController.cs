using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    SimpleEnemy curEnemy;
    // Use this for initialization
    void Start () {
        curEnemy = gameObject.AddComponent<SimpleEnemy>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
