using Assets.Scripts.BaseClasses;
using Assets.Scripts.Loaders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : BaseActiveModels
    {
        float currentSpeed;
        int bombCount;  //10 max
        //int placedBombs;
        bool isNoClip;  
        bool bombPower; //6 max
        

        protected override void CreateModel(float i, float j)
        {
            GameObject gameModelsObject = PlayerLoader.GetPlayerPrefab();
            gameModelsObject.transform.position = new Vector3(i, 1.0f, j);
        }
        public Player(float i, float j)
        {
            CreateModel(i, j);
            isMoving = false;
            
        }
        protected void PlaceBomb(float i, float j)
        {
            if (GameObject.FindGameObjectsWithTag("Bomb").Length < bombCount)
            { //Ставится три бомбы, нужно найти баг
                GameObject gameModelsObject;
                if (!bombPower) { 
                    gameModelsObject = BombLoader.GetSimpleBomb();
                }
                else
                    gameModelsObject = BombLoader.GetHardBomb();
                gameModelsObject.transform.position = new Vector3(i, 1.0f, j);
            }

        }
        private void Start()
        {
            isMoving = false;
            currentSpeed = 1.5f ;
            bombCount = 1;  //10 max
            isNoClip = false;
            bombPower = false; //6 max
        }
        protected override bool IsCollisionWithWallOrCube(Vector3 transformPositions, Vector3 targetPositions)
        {
            RaycastHit hit;
            if (Physics.Linecast(transformPositions, targetPositions, out hit))
            {
                if ((hit.collider.gameObject.tag == "UnbreakingCube") ||
                    ((hit.collider.gameObject.tag == "BreakingCube")&&(!isNoClip)))
                {
                    //print("No way man!");
                    return true;
                    //CollisionWithGameObject();
                }

            }
            return false;
        }



        private bool CanExecute()
        {
            if (isMoving)
            {
                return true;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                return SetTargetPositionAndCheckCollisions(Vector3.forward, directionType.Forward);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return SetTargetPositionAndCheckCollisions(Vector3.left, directionType.Left);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                return SetTargetPositionAndCheckCollisions(Vector3.right, directionType.Right);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                return SetTargetPositionAndCheckCollisions(Vector3.back, directionType.Reverse);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                    PlaceBomb(transform.position.x, transform.position.z);
            }
            return false;
        }
        private void CollisionWithGameObject()
        {
            //Comming Soon
        }
        private void CollisionWithBonus()
        {
            //Comming Soon
        }
        private void Execute()
        {
            isMoving = true;
            StartCoroutine(onCoroutine());
        }
        void Update()
        {
            if (CanExecute()) {
                Execute();
            }
        }
         IEnumerator onCoroutine()
         {
             //Предусмотреть при столкновении - откат на стартовую позицию
             //print("Step");
             transform.position = Vector3.MoveTowards(
                 transform.position, 
                 targetPosition, 2f * Time.deltaTime);
             //print(" 1 " + transform.position.z);
             Camera.main.transform.position = Vector3.MoveTowards(
                 Camera.main.transform.position,
                 new Vector3(transform.position.x,15f,transform.position.z),
                 currentSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
             {
                 isMoving = false;
                 //print(" 2 ");
             }
             yield return new WaitForEndOfFrame();  //или Нуль, подумать.
                                                    //yield return null;
        }
        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Enemy":
                    Destroy(this.gameObject);
                    break;
                case "Bonus_Radius":
                    //bombCount = (bombCount <= 10) ? ++bombCount : 10;
                    bombPower = true;
                    break;
                case "Bonus_Speed":
                    currentSpeed = 3.5f;
                    break;
                case "Bonus_NoClip":
                    isNoClip = true;
                    break;
                case "Bonus_BombCount":
                    bombCount = (bombCount <= 6) ? bombCount+1 : 6;
                    break;
                default:
                    break;
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            print("Player OnParticleCollision");
        //    BaseExplosion.PlayerDestroyEvent(transform.position);
        }
        protected override void OnDestroy()
        {
            //BaseExplosion.PlayerDestroyEvent(transform.position); 
        }
    }
}
