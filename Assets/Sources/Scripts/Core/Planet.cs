using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Sources.Scripts.Core
{
    public class PlanetCallback : UnityEvent<Planet>
    {
    }

    public class Planet : MonoBehaviour
    {
        public Renderer myRenderer;
        public Transform planetTransform;
        public HPBar hpBar;
        public GameObject directionSprite;
        public Transform rocketLaunchPoint;
        public GameObject rocketPrefab;
        public AudioSource rocketLaunchAudioSource;
        public readonly PlanetCallback OnPlanetDestroyed = new PlanetCallback();

        private float _smallRadius;
        private float _bigRadius;
        private float _currentAngle;
        private float _rotationSpeed;
        private float _changeAngleSpeed;
        private int _materialIndex;

        private bool _moving;
        private bool _isPlayer;

        private PlanetPreset _planetPreset;
        private WaitForSeconds _waitForReloading;
        private bool _reloading;
        private float _currentHealth;

        public void Init(float smallRadius, float bigRadius, float scale, float startAngle,
            float rotationSpeed, float changeAngleSpeed, Material rendererMaterial, int materialIndex, bool showHpBar)
        {
            _smallRadius = smallRadius;
            _bigRadius = bigRadius;

            _currentAngle = startAngle;
            _rotationSpeed = rotationSpeed;
            _changeAngleSpeed = changeAngleSpeed;

            _materialIndex = materialIndex;
            
            transform.localScale = new Vector3(scale, scale, scale);

            myRenderer.material = rendererMaterial;
            hpBar.gameObject.SetActive(showHpBar);

            MovePlanet();
        }

        public void SetPreset(PlanetPreset planetPreset)
        {
            _planetPreset = planetPreset;
            hpBar.MaxValue = _planetPreset.maxHealth;
            CurrentHealth = _planetPreset.maxHealth;

            _waitForReloading = new WaitForSeconds(_planetPreset.reloadingTime);
        }

        public void StartMoving()
        {
            _moving = true;
        }

        public void StopMoving()
        {
            _moving = false;
        }

        public bool IsPlayer
        {
            get => _isPlayer;
            set
            {
                _isPlayer = value;
                directionSprite.SetActive(_isPlayer);
            }
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
            planetTransform.Rotate(0, 0, Time.deltaTime * _rotationSpeed);
        }

        private void Update()
        {
            if (!_moving)
            {
                return;
            }

            RotatePlanet();
            MovePlanet();

            if (IsPlayer && Input.GetMouseButtonDown(0))
            {
                LaunchRocket();
            }
        }

        public void LaunchRocket()
        {
            if (_reloading)
            {
                return;
            }

            GameObject rocketGameObject =
                Instantiate(rocketPrefab, rocketLaunchPoint.position, rocketLaunchPoint.rotation);
            rocketGameObject.GetComponent<Rigidbody>().AddForce(rocketLaunchPoint.up * _planetPreset.rocketForce);
            Rocket rocket = rocketGameObject.GetComponent<Rocket>();
            if (rocket)
            {
                rocket.Init(_planetPreset.rocketDamage, gameObject);
            }

            rocketLaunchAudioSource.Play();

            StartCoroutine(Reload());
        }

        private IEnumerator Reload()
        {
            _reloading = true;
            yield return _waitForReloading;
            _reloading = false;
        }

        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                hpBar.CurrentValue = _currentHealth;
                if (_currentHealth <= 0)
                {
                    DestroyPlanet();
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            Rocket rocket = other.gameObject.GetComponent<Rocket>();
            if (rocket && rocket.rocketSource != gameObject)
            {
                CurrentHealth -= rocket.damage;
            }
        }

        private void DestroyPlanet()
        {
            OnPlanetDestroyed.Invoke(this);
            Destroy(gameObject);
        }

        public PlanetData GetPlanetData()
        {
            return new PlanetData
            {
                smallRadius = _smallRadius,
                bigRadius = _bigRadius,
                currentAngle = _currentAngle,
                rotationSpeed = _rotationSpeed,
                changeAngleSpeed = _changeAngleSpeed,
                isPlayer = IsPlayer,
                scale = transform.localScale.x,
                preset = _planetPreset,
                materialIndex = _materialIndex,
                currentHealth = CurrentHealth
            };
        }
    }
}