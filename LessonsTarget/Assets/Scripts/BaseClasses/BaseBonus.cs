using Assets.Scripts.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    abstract class BaseBonus : MonoBehaviour
    {
        protected float rotationalSpeed;
        protected abstract void PlaceBonus(float x, float z);
        private void Update()
        {
            transform.Rotate(Vector3.up*20*Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
