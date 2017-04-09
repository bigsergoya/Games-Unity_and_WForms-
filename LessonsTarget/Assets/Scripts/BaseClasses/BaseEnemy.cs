using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.BaseClasses
{
    abstract class BaseEnemy : BaseActiveModels
    {

        protected abstract void CreateEnemyModel(float i, float j);
        protected abstract bool CanExecute();
        protected bool CollisionWithEnemy()
        {
            //Задел на будущее
            return false;
        }

        protected override void CreateModel(float i, float j)
        {
            CreateEnemyModel(i, j);
        }

    }
}
