using System.Collections.Generic;
using UnityEngine;

namespace Sources.Scripts.Core
{
    public class PlanetsSpawner : MonoBehaviour
    {
        public List<Material> materialsForPlanets;
        public GameObject planetPrefab;

        public float minPlanetScale;
        public float maxPlanetScale;

        public float minPlanetRotationSpeed;
        public float maxPlanetRotationSpeed;
        
        public float minPlanetMovingSpeed;
        public float maxPlanetMovingSpeed;

        public bool showHPBar;
        
        private readonly List<Planet> _planets = new List<Planet>();

        public List<Planet> SpawnPlanets(int planetsNumber, float smallRadiusDelta, float bigRadiusDelta)
        {
            for (int i = 0; i < planetsNumber; i++)
            {
                Planet planet = CreatePlanet((i + 1) * smallRadiusDelta, (i + 1) * bigRadiusDelta);
                if (planet)
                {
                    _planets.Add(planet);
                }
            }

            return _planets;
        }

        private Planet CreatePlanet(float smallRadius, float bigRadius)
        {
            GameObject planetGameObject = Instantiate(planetPrefab, transform);
            Planet planet = planetGameObject.GetComponent<Planet>();
            if (planet)
            {
                float scale = Random.Range(minPlanetScale, maxPlanetScale);
                float movingSpeed = Random.Range(minPlanetMovingSpeed, maxPlanetMovingSpeed);
                float rotationSpeed = Random.Range(minPlanetRotationSpeed, maxPlanetRotationSpeed);
                float startAngle = Random.Range(0, Mathf.PI * 2);
                Material material = materialsForPlanets[Random.Range(0, materialsForPlanets.Count)];
                planet.Init(smallRadius, bigRadius, scale, startAngle, rotationSpeed, movingSpeed, material, showHPBar);
                return planet;
            }

            return null;
        }

        public void StartMovingPlanets()
        {
            foreach (var planet in _planets)
            {
                planet.StartMoving();
            }
        }

        public void StopMovingPlanets()
        {
            foreach (var planet in _planets)
            {
                planet.StopMoving();
            }
        }
    }
}
