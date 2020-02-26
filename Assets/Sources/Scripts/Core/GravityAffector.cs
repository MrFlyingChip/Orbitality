using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Scripts.Core
{
    public class GravityAffector : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private readonly HashSet<Rigidbody> _affectedBodies = new HashSet<Rigidbody>();
        
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody)
            {
                _affectedBodies.Add(other.attachedRigidbody);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.attachedRigidbody)
            {
                _affectedBodies.Remove(other.attachedRigidbody);
            }
        }

        private void FixedUpdate()
        {
            foreach (var body in _affectedBodies)
            {
                if (!body) continue;
                Vector3 direction = (transform.position - body.position).normalized;
                float distance = (transform.position - body.position).sqrMagnitude;

                float force = 3 * body.mass * _rigidbody.mass / distance;
                body.AddForce(direction * force);
            }

            _affectedBodies.Remove(null);
        }
    }
}
