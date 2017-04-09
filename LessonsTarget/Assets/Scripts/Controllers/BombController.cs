using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

    SimpleBomb bomb;
	// Use this for initialization
	void Start () {
        bomb = gameObject.AddComponent<SimpleBomb>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
