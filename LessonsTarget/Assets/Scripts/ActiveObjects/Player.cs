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
        bool isNoClip;  
        bool bombPower; //6 max
        public AudioClip bonusCollideSound;
        Camera curCam;

        protected override void CreateModel(float i, float j)
        {
            GameObject gameModelsObject = PlayerLoader.GetPlayerPrefab();
            gameModelsObject.transform.position = new Vector3(i, 0.5f, j);
        }
        public Player(float i, float j)
        {
            CreateModel(i, j);
            isMoving = false;
            banMoving = false;
        }
        protected void PlaceBomb(float i, float j)
        {
            if (GameObject.FindGameObjectsWithTag("Bomb").Length < bombCount)
            { 
                GameObject gameModelsObject;
                if (!bombPower) { 
                    gameModelsObject = BombLoader.GetSimpleBomb();
                }
                else
                    gameModelsObject = BombLoader.GetHardBomb();
                gameModelsObject.transform.position = new Vector3(i, 1.0f, j);
                animationController.SetTrigger("PlantABomb");
            }

        }
        private void Start()
        {
            curCam = gameObject.GetComponent("Main Camera") as Camera;
            isMoving = false;
            currentSpeed = 1.5f ;
            bombCount = 1;  //10 max
            isNoClip = false;
            bombPower = false; //6 max
            animationController = GetComponent<Animator>();
            banMoving = false;
            source = gameObject.GetComponentInChildren<AudioSource>();
        }
        protected override bool IsCollisionWithWallOrCube(Vector3 transformPositions, Vector3 targetPositions)
        {
            RaycastHit hit;
            if (Physics.Linecast(transformPositions, targetPositions, out hit))
            {
                if ((hit.collider.gameObject.tag == "UnbreakingCube") ||
                    ((hit.collider.gameObject.tag == "BreakingCube")&&(!isNoClip)))
                {
                    return true;
                }

            }
            return false;
        }



        private bool CanExecute()
        {
            if (banMoving)
            {
                return false;
            }
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
                //PlaceBomb(transform.position.x, transform.position.z);
                BanAnyMovement();
                animationController.SetTrigger("PlantABomb");                
            }
            return false;
        }
        private void Execute()
        {
            isMoving = true;
            StartCoroutine(onCoroutine());
        }
        void Update()
        {
            if (CanExecute()) {
                animationController.SetFloat("Speed", 1);
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
                 animationController.SetFloat("Speed", 0);
            }
             yield return new WaitForEndOfFrame();  //или Нуль, подумать.
                                                    //yield return null;
        }
        void PutBombAndPlaySound()
        {
            PlaceBomb(transform.position.x, transform.position.z);
        }
        void BanAnyMovement()
        {
            banMoving = true; //Доп флаговую переменную или переименовать эту
            print("All movement is ban");
        }
        void AvoidAnyMovement()
        {
            banMoving = false;
            animationController.ResetTrigger("PlantABomb");
            print("All movement is avoid");
        }
        void becomeDead()
        {
            animationController.SetTrigger("BecomeDead");
            print("Player OnParticleCollision");
            BanAnyMovement();
            source.PlayOneShot(dieSound, soundVolume);

            var colChildren = gameObject.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colChildren)
            {
                collider.enabled = false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            print("Player OnTriggerEnter");
            switch (other.tag)
            {
                case "Enemy":
                    //Destroy(this.gameObject);
                    becomeDead();
                    break;
                case "Bonus_Radius":
                    source.PlayOneShot(bonusCollideSound, soundVolume); 
                    //bombCount = (bombCount <= 10) ? ++bombCount : 10;
                    bombPower = true;
                    break;
                case "Bonus_Speed":
                    source.PlayOneShot(bonusCollideSound, soundVolume);
                    currentSpeed = 3.5f;
                    break;
                case "Bonus_NoClip":
                    source.PlayOneShot(bonusCollideSound, soundVolume);
                    isNoClip = true;
                    break;
                case "Bonus_BombCount":
                    source.PlayOneShot(bonusCollideSound, soundVolume);
                    bombCount = (bombCount <= 6) ? bombCount+1 : 6;
                    break;
                default:
                    break;
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            becomeDead();
        }
        protected override void OnDestroy()
        {
            becomeDead();
        }
    }
}
