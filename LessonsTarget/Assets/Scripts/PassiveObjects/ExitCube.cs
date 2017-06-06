using Assets.Scripts.BaseClasses;
using Assets.Scripts.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.PassiveObjects
{
    class ExitCube: GameObjectLoader
    {
        public Material exitModeMaterial;
        Renderer rend;
        bool exitModeOn = false;

        public bool ExitModeOn
        {
            get
            {
                return exitModeOn;
            }
        }

        public ExitCube(float x, float z)
        {
            GameObject gameModelsObject = GameObjectLoader.GetObjectsPrefabByName("ExitCube");
            gameModelsObject.transform.position = new Vector3(x, 0.5f, z);
        }
        private void Start()
        {
            rend = GetComponent<Renderer>();
            rend.enabled = true;
        }
        private void OnParticleCollision(GameObject other)
        {
            rend.sharedMaterial = exitModeMaterial;
            exitModeOn = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                
                //ExitCube[] comParent = gameObject.GetComponentsInChildren<ExitCube>();
                ExitCube[] comParent = gameObject.GetComponentsInParent<ExitCube>();
                if (comParent[1].ExitModeOn) 
                //if (comParent.Length>1)
                {
                    //Destroy(this.gameObject);
                    string[] tagsOfDeletedObjects = { "Enemy", "Bomb", "BombParth", "PS", "Player"};
                    BaseWorkingWithGame.DeleteAllObjects(tagsOfDeletedObjects); //enemies and bombs
                    BaseWorkingWithGame.PrintEndOfTheGameMessage();
                    //PrintEndOfTheGameMarker();
                }
            }
        }

        /*private void DeleteAllActiveObjects()
        {

            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            List<GameObject> objectsToDisable = new List<GameObject>(allObjects);
            foreach (GameObject a in allObjects)
            {
                foreach (GameObject b in objectsToDisable)
                {
                    if (a.name == b.name)
                        objectsToDisable.Remove(a);
                }
            }
            foreach (GameObject a in objectsToDisable)
                a.SetActive(false);
        }*/
    }
}
