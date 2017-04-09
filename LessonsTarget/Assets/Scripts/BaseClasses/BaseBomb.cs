using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    abstract class BaseBomb : MonoBehaviour
    {
        protected float explosionTimer; //запускается несколько отсчетов!!! ПОФИКСИТЬ!!!!!!
        protected int explosionRadius;
        protected bool ExposionFlag = true;

        protected abstract void SetStartParameters();

        protected void Start()
        {
            SetStartParameters();
            StartExplosion();
        }
        /*protected void DestroyAllObjectsOnLine(Vector3 transformPositions, Vector3 targetPositions) //таргет через форвард
        {
            RaycastHit[] hits = Physics.RaycastAll(transformPositions, targetPositions, explosionRadius);
            foreach(RaycastHit hit in hits)
                if ((hit.collider.gameObject.tag != "UnbreakingCube")&&(hit.collider.gameObject.tag != "Bomb"))
                {
                   // print("Booom!");
                    Destroy(hit.collider.gameObject);
                }
        }*/
        protected void Update()
        {
            /*explosionTimer -= Time.deltaTime;
            if ((explosionTimer < 0)&&(ExposionFlag))
            {
                ExposionFlag = false;
                Explosion();

            }*/
        }
        protected Vector3 SetTargetPosition(Vector3 transformPosition, float x, float y, float z)
        {
            return new Vector3(
                transformPosition.x + x,
                transformPosition.y + y,
                transformPosition.z + z);
        }
        protected void StartExplosion()
        {
            BaseExplosion.MainExplosionEvent(transform.position,explosionTimer,explosionRadius);
            //DestroyAllObjectsOnLine(transform.position, Vector3.forward);
            //DestroyAllObjectsOnLine(transform.position, Vector3.right);
            //DestroyAllObjectsOnLine(transform.position, Vector3.back);
            //DestroyAllObjectsOnLine(transform.position, Vector3.left);

            //checkPlayersInsideExposion();
            Destroy(this.gameObject,explosionTimer);
        }
        /*protected void checkPlayersInsideExposion()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player!=null)
                if (player.transform.position == this.transform.position)
                    player.gameObject.active = false;
        }*/
    }

}
