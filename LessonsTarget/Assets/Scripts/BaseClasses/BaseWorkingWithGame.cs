using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.BaseClasses
{
    public class BaseWorkingWithGame : MonoBehaviour
    {
        static public void DeleteAllObjects(string[] tagsOfDeletedObjects)
        {
            //var deletedGameObjects = Object.FindObjectsOfType<GameObject>();
            /*string[] tagsOfDeletedObjects = { "Player", "Enemy", "Bomb", "FieldCell", "ExitCube"
                    , "BreakingCube", "UnbreakingCube", "Bonus_Radius", "Bonus_Speed", "Bonus_NoClip"
                    , "Bonus_BombCount"
            };*/
            foreach (string tag in tagsOfDeletedObjects)
            {
                var deletedGameObjects = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject o in deletedGameObjects)
                {
                    o.SetActive(false);
                    Destroy(o);
                }
            }
        }
        static int ParseIntValue(string line)
        {
            int subInt = line.LastIndexOf(":") + 1;
            int scoresCount;
            int.TryParse(line.Substring(subInt, line.Length - subInt), out scoresCount);
            return scoresCount;
        }
        static public void PrintNewScores(int addingScores)
        {
            Text scoresTextPanel = GameObject.Find("TextPanel").GetComponentsInChildren<Text>()[0];
            scoresTextPanel.text = "Scores :" + (ParseIntValue(scoresTextPanel.text) + addingScores);
        }
        static public void PrintNewBombsCount(int bombsCount)
        {
            Text scoresTextPanel = GameObject.Find("TextPanel").GetComponentsInChildren<Text>()[2];
            scoresTextPanel.text = "Available Active Bombs :" + (bombsCount);
        }
        static public void PrintNewWallsStatus(string status)
        {
            Text scoresTextPanel = GameObject.Find("TextPanel").GetComponentsInChildren<Text>()[3];
            scoresTextPanel.text = "Walls " + status;
        }
        static public void PrintNewBombsPower(int bombsPower)
        {
            Text scoresTextPanel = GameObject.Find("TextPanel").GetComponentsInChildren<Text>()[4];
            scoresTextPanel.text = "Power of a Bomb :" + (bombsPower);
        }
        static public void PrintNewSpeed(float speed)
        {
            Text scoresTextPanel = GameObject.Find("TextPanel").GetComponentsInChildren<Text>()[5];
            scoresTextPanel.text = "Speed :" + speed.ToString("0.0");
        }
        static public void ClearAllText()
        {
            Text[] scoresTextPanel = GameObject.Find("TextPanel").GetComponentsInChildren<Text>();
            if (scoresTextPanel != null) {
                scoresTextPanel[0].text = "Scores :0";
                scoresTextPanel[1].text = default(string);
                scoresTextPanel[2].text = "Available Active Bombs :1";
                scoresTextPanel[3].text = "Walls Active";
                scoresTextPanel[4].text = "Power of a Bomb :1";
                scoresTextPanel[5].text = "Speed :2.0";
                //scoresTextPanel[1].enabled = false;
            }
        }
        // Your game is finished! Click "R" to begin a new game or "Esc" to quit the program.
        static public void PrintEndOfTheGameMessage()
        {
            Text[] allPanels = GameObject.Find("TextPanel").GetComponentsInChildren<Text>();
            Text scoresTextPanel = GameObject.Find("TextPanel").GetComponentsInChildren<Text>()[1];
            if (allPanels != null)
                if (allPanels.Length > 1)
                {
                    //allPanels[1].enabled = true;
                    allPanels[1].text = "Your game is finished! Click \"R\" to begin a new game or \"Esc\" to quit the program.";
                }
        }
    }
}
