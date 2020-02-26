using System.Collections;
using UnityEngine;

namespace Sources.Scripts.Utility
{
    public class AutoDestroy : MonoBehaviour
    {
        public float timeBeforeDestroy;
        void Start()
        {
            StartCoroutine(DestroySelf());
        }

        private IEnumerator DestroySelf()
        {
            yield return new WaitForSeconds(timeBeforeDestroy);
            Destroy(gameObject);
        }
    }
}
