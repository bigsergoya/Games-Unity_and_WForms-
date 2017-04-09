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
    //Через коллайдер мы понимаем столкновение частиц взрыва с объектами на поле
    //Также как в примере реализовать взрыв в классе бэйс экспложион.
    class SimpleEnemy : BaseEnemy
    {
        System.Random rnd;

        protected override void CreateEnemyModel(float i, float j)
        {
            GameObject gameModelsObject;
            gameModelsObject = EnemyLoader.GetEnemyPrefab();
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
        }
        protected override bool CanExecute()
        {
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
        private void Execute()
        {
            isMoving = true;
            StartCoroutine(onCoroutine());
        }
        void Update()
        {
            if (CanExecute())
            {
                Execute();
            }
        }
        IEnumerator onCoroutine()
        {
            
            //print("Enemy Step");
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition, 2f * Time.deltaTime);
            //print(" 2 " + transform.position.z);
            if (CollisionWithEnemy())
            {
                // Дополнить. Убить
            }
            if (transform.position == targetPosition)
            {
                isMoving = false;
                //print(" 2 ");
            }
            yield return new WaitForEndOfFrame();  //или Нуль, подумать.
                                                   //yield return null;
        }

        protected override void OnDestroy()
        {
            //BaseExplosion.EnemyDestroyEvent(transform.position);
        }
    }
}
