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
    class Player : BaseActiveModels
    {

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
            GameObject gameModelsObject = BombLoader.GetSimpleBomb();
            gameModelsObject.transform.position = new Vector3(i, 1.0f, j);

        }
        private void Start()
        {
            isMoving = false;
        }
        private bool CanExecute()
        {
            if (isMoving)
            {
                return true;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                /*targetPosition = SetTargetPosition(transform.position, Vector3.forward);
                if (!IsCollisionWithWallOrCube(transform.position, targetPosition))
                {
                    TurnFace(directionType.Forward);
                    return true;
                }
                return false;*/
                return SetTargetPositionAndCheckCollisions(Vector3.forward, directionType.Forward);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                /*targetPosition = SetTargetPosition(transform.position, Vector3.left);
                if (!IsCollisionWithWallOrCube(transform.position, targetPosition))
                {
                    TurnFace(directionType.Left);
                    return true;
                }
                return false;*/
                return SetTargetPositionAndCheckCollisions(Vector3.left, directionType.Left);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                /*targetPosition = SetTargetPosition(transform.position, Vector3.right);
                if (!IsCollisionWithWallOrCube(transform.position, targetPosition))
                {
                    TurnFace(directionType.Right);
                    return true;
                }
                return false;*/
                return SetTargetPositionAndCheckCollisions(Vector3.right, directionType.Right);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                /*targetPosition = SetTargetPosition(transform.position, Vector3.back);
                if (!IsCollisionWithWallOrCube(transform.position, targetPosition))
                {
                    TurnFace(directionType.Reverse);
                    return true;
                }
                return false;*/
                return SetTargetPositionAndCheckCollisions(Vector3.back, directionType.Reverse);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                PlaceBomb(transform.position.x, transform.position.z);
            }
            return false;
        }

        private bool CollisionsChecker(Vector3 transformPositions, Vector3 targetPositions)
        {
            RaycastHit hit;
            if (Physics.Linecast(transformPositions, targetPositions, out hit))
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    //print("U dead...");
                    CollisionWithGameObject();
                }
                if (hit.collider.gameObject.tag == "Bonus")
                {
                    //print("Take this bonus, dude");
                    CollisionWithBonus();
                }
                /*if ((hit.collider.gameObject.tag == "UnbreakingCube") || (hit.collider.gameObject.tag == "BreakingCube"))
                {
                    print("Stooop!");
                    CollisionWithGameObject();
                }*/
                return false;
            }
            else
                return true;
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
                 1.5f * Time.deltaTime);
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
            if (other.tag == "Enemy") { 
                Destroy(this.gameObject);
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
