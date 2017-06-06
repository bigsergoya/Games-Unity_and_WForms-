using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    abstract class BaseEnemy : BaseActiveModels
    {
        protected abstract void CreateEnemyModel(float i, float j);
        protected abstract bool CanExecute();
        protected abstract void Execute();
        protected const int scoresForTheEnemy = 50;
        public AudioClip battlyCry;
        protected void CollisionWithEnemy()
        {
            if(!banMoving)
                AttackTheEnemy();
        }
        protected abstract void Die();
        protected void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                CollisionWithEnemy();
            }
        }
        protected void Update()
        {
            if (CanExecute())
            {
                animationController.SetFloat("Speed", 1);
                Execute();
            }
        }
        protected void OnParticleCollision(GameObject other)
        {
            BaseWorkingWithGame.PrintNewScores(scoresForTheEnemy);
            Die();            
        }

        protected override void CreateModel(float i, float j)
        {
            CreateEnemyModel(i, j);
        }
        protected void StartBattleCry()
        {
            source.PlayOneShot(battlyCry, soundVolume);
        }
        protected virtual void AttackTheEnemy()
        {
            animationController.SetTrigger("Attack");
            banMoving = true;
        }

    }
}
