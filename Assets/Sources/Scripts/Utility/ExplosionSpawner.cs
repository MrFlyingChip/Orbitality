using System;
using UnityEngine;

namespace Sources.Scripts.Utility
{
    public class ExplosionSpawner : MonoBehaviour
    {
        public GameObject explosionPrefab;
        
        private void OnDestroy()
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
    }
}
