using System.Collections.Generic;
using UnityEngine;

namespace Sources.Scripts.Core
{
    public class PlanetPreset
    {
        public float maxHealth;
        public float reloadingTime;
        public float rocketDamage;
        public float rocketForce;
    }
    
    public static class PlanetsSettings
    {
        public static List<PlanetPreset> planetPresets = new List<PlanetPreset>
        {
            new PlanetPreset{maxHealth = 20, reloadingTime = 2, rocketDamage = 4, rocketForce = 10000},
            new PlanetPreset{maxHealth = 30, reloadingTime = 3, rocketDamage = 2, rocketForce = 7500},
            new PlanetPreset{maxHealth = 15, reloadingTime = 1.5f, rocketDamage = 2, rocketForce = 15000},
            new PlanetPreset{maxHealth = 20, reloadingTime = 5, rocketDamage = 8, rocketForce = 20000},
            new PlanetPreset{maxHealth = 25, reloadingTime = 4, rocketDamage = 6, rocketForce = 12000},
        };

        public static PlanetPreset GetRandomPreset()
        {
            return planetPresets[Random.Range(0, planetPresets.Count)];
        }
    }
}