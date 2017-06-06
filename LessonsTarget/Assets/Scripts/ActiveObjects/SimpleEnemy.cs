using Assets.Scripts;
using Assets.Scripts.BaseClasses;
using Assets.Scripts.Loaders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class SimpleEnemy : BaseEnemy
    {
        System.Random rnd;

        protected override void CreateEnemyModel(float i, float j)
        {
            GameObject gameModelsObject;
            gameModelsObject = GameObjectLoader.GetObjectsPrefabByName("KindSkeleton");
            gameModelsObject.transform.position = new Vector3(i, 1.0f, j);
        }

        public SimpleEnemy(float i, float j)
        {
            CreateModel(i, j);
            isMoving = false;
            rnd = new System.Random();
        }
        private void Start()
        {
            isMoving = false;
            rnd = new System.Random();
            animationController = GetComponent<Animator>();
            banMoving = false;
            source = GetComponent<AudioSource>();
        }
        protected override bool CanExecute()
        {
            if (banMoving)
            {
                return false;
            }
            if (isMoving)
            {
                return true;
            }

            direction = (directionType)Enum.ToObject(typeof(directionType), rnd.Next(0, 3));

            if (direction == directionType.Forward)
            {
                return SetTargetPositionAndCheckCollisions(Vector3.forward, directionType.Forward);
            }
            if (direction == directionType.Left)
            {
                return SetTargetPositionAndCheckCollisions(Vector3.left, directionType.Left);
            }
            if (direction == directionType.Right)
            {
                return SetTargetPositionAndCheckCollisions(Vector3.right, directionType.Right);
            }
            if (direction == directionType.Reverse)
            {
                return SetTargetPositionAndCheckCollisions(Vector3.back, directionType.Reverse);
            }

            return false;
        }
        protected override void Execute()
        {
            isMoving = true;
            StartCoroutine(onCoroutine());
        }
        IEnumerator onCoroutine()
        {
            //print("Enemy Step");
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition, 2f * Time.deltaTime);
            //print(" 2 " + transform.position.z);
            if (transform.position == targetPosition)
            {
                isMoving = false;
                animationController.SetFloat("Speed", 0);
                //print(" 2 ");
            }
            yield return new WaitForEndOfFrame();  
        }
        protected override void OnDestroy()
        {
            //BaseExplosion.EnemyDestroyEvent(transform.position);
        }

        protected override void Die()
        {
            source.PlayOneShot(dieSound, soundVolume);
            animationController.SetTrigger("Die");
            banMoving = true;
            gameObject.SetActive(false);
        }
    }
}
