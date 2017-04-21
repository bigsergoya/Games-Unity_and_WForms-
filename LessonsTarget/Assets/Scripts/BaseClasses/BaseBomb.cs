using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    abstract class BaseBomb : MonoBehaviour
    {
        protected float explosionTimer; 
        protected int explosionRadius;
        protected bool ExposionFlag = true;
        public AudioClip fusionSound;
        public AudioClip explosionSound;
        protected const float soundVolume = 0.5f;
        protected AudioSource source;
        protected abstract void SetStartParameters();

        protected void Start()
        {
            source = GetComponent<AudioSource>();
            source.PlayOneShot(fusionSound, soundVolume);
            SetStartParameters();
            StartExplosion();
        }
        protected void Update()
        {
            explosionTimer -= Time.deltaTime;
            if ((explosionTimer < 0)&&(ExposionFlag))
            {
                ExposionFlag = false;
                source.PlayOneShot(explosionSound, soundVolume);
                Destroy(this.gameObject, 1);
                //gameObject.SetActive(false);
            }
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
        }
        private void OnDestroy()
        {
        }
    }

}
