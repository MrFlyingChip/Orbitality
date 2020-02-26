using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Scripts.Core
{
    public class BotsController : MonoBehaviour
    {
        public static BotsController Instance;

        private List<Planet> _planets;

        private const float ChanceToShoot = 0.0005f;
        
        private void Awake()
        {
            Instance = this;
        }

        public void StartBots(List<Planet> planets)
        {
            _planets = planets;
        }

        private void Update()
        {
            if (_planets == null)
            {
                return;
            }
            
            foreach (var planet in _planets)
            {
                if (!planet.IsPlayer)
                {
                    float chance = Random.Range(0, 1);
                    if (chance <= ChanceToShoot)
                    {
                        planet.LaunchRocket();
                    }
                }
            }
        }
    }
}
