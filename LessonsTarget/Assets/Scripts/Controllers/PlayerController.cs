using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    Player curPlayer;
    Camera curCam;
    //bool IsMove;
    //float i;
    //Vector3 endPos;

    void Start () {
        //curPlayer = gameObject.AddComponent<Player>();
        curCam = gameObject.GetComponent("Main Camera") as Camera;
       // Camera.main.transform.
    }
        void Update()
    {
        //Проверка для игрока, зажимает ли его квадратами!!!!
    }
}
