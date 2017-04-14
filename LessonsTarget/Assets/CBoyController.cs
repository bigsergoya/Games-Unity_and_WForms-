using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBoyController : MonoBehaviour {

    // Use this for initialization
    Player curPlayer;
    Camera curCam;
    //bool IsMove;
    //float i;
    //Vector3 endPos;

    void Start()
    {
        //curPlayer = gameObject.AddComponent<Player>();
        curCam = gameObject.GetComponent("Main Camera") as Camera;
        
        // Camera.main.transform.
    }
    private void OnTriggerEnter(Collider other)
    {
        print("Collider Works!");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
