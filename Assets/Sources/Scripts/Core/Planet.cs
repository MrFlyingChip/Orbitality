using System;
using UnityEngine;

namespace Sources.Scripts.Core
{
    public class Planet : MonoBehaviour
    {
        public Renderer myRenderer;
        
        private float _smallRadius;
        private float _bigRadius;
        private float _currentAngle;
        private float _rotationSpeed;
        private float _changeAngleSpeed;

        private bool _moving;
        
        public void Init(float smallRadius, float bigRadius, float scale, float startAngle, 
            float rotationSpeed, float changeAngleSpeed, Material rendererMaterial)
        {
            _smallRadius = smallRadius;
            _bigRadius = bigRadius;

            _currentAngle = startAngle;
            _rotationSpeed = rotationSpeed;
            _changeAngleSpeed = changeAngleSpeed;
            
            transform.localScale = new Vector3(scale, scale, scale);

            myRenderer.material = rendererMaterial;
            
            MovePlanet();
        }

        public void StartMoving()
        {
            _moving = true;
        }

        public void StopMoving()
        {
            _moving = false;
        }

        private void MovePlanet()
        {
            _currentAngle += _changeAngleSpeed * Time.deltaTime;
            
            float x = _bigRadius * Mathf.Cos(_currentAngle);
            float y = _smallRadius * Mathf.Sin(_currentAngle);

            transform.localPosition = new Vector3(x, y, 0);
        }

        private void RotatePlanet()
        {
            transform.Rotate(0, 0, Time.deltaTime * _rotationSpeed);
        }

        private void Update()
        {
            if (!_moving)
            {
                return;
            }

            RotatePlanet();
            MovePlanet();
        }
    }
}
