using System;
using UnityEngine;

namespace Sources.Scripts.Core
{
    public class Rocket : MonoBehaviour
    {
        public float damage;
        public GameObject rocketSource;
        
        public void Init(float myDamage, GameObject source)
        {
            damage = damage;
            rocketSource = source;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject != rocketSource)
            {
                Destroy(gameObject);
            }
        }
    }
}