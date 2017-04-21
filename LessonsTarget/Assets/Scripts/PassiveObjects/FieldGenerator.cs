using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour {

	// Use this for initialization
    public int fieldSize;
    public int countOfBreakingWalls;
    private Assets.Scripts.Player player;
    //Player player;

    void Start () {
        Assets.Field field = new Assets.Field(fieldSize, countOfBreakingWalls);
        field.Create();
        player = field.Player;
        Camera.main.transform.position = new Vector3(field.PlayerPos.I, 15, field.PlayerPos.J);

    }
	
	// Update is called once per frame
	void Update () {

    }
}
