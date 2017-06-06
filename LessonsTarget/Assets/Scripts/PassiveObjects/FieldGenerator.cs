using Assets.Scripts.BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour {

	// Use this for initialization
    public int fieldSize;
    public int countOfBreakingWalls;
    private Assets.Scripts.Player player;
    //Player player;

    void CreateField()
    {
        Assets.Field field = new Assets.Field(fieldSize, countOfBreakingWalls);
        field.Create();
        player = field.Player;
        Camera.main.transform.position = new Vector3(field.PlayerPos.I, 15, field.PlayerPos.J);
    }
    void Start () {
        CreateField();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.R))
        {
            string[] tagsOfDeletedObjects = { "Player", "Enemy", "Bomb", "FieldCell", "ExitCube"
                , "BreakingCube", "UnbreakingCube", "Bonus_Radius", "Bonus_Speed", "Bonus_NoClip"
                    , "Bonus_BombCount"};
            BaseWorkingWithGame.DeleteAllObjects(tagsOfDeletedObjects);
            CreateField();
            BaseWorkingWithGame.ClearAllText();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
}
