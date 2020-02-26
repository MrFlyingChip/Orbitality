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
                    planet.OnPlanetDestroyed.AddListener(OnPlanetDestroy);
                }
            }

            return _planets;
        }

        public List<Planet> SpawnPlanetsFromData(SaveData saveData)
        {
            for (int i = 0; i < saveData.planets.Count; i++)
            {
                Planet planet = CreatePlanetFromData(saveData.planets[i]);
                if (planet)
                {
                    _planets.Add(planet);
                    planet.OnPlanetDestroyed.AddListener(OnPlanetDestroy);
                }
            }

            return _planets;
        }

        private void OnPlanetDestroy(Planet planet)
        {
            _planets.Remove(planet);
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
                int materialIndex = Random.Range(0, materialsForPlanets.Count);
                Material material = materialsForPlanets[materialIndex];
                planet.Init(smallRadius, bigRadius, scale, startAngle, rotationSpeed, movingSpeed, material, materialIndex, showHPBar);
                return planet;
            }

            return null;
        }
        
        private Planet CreatePlanetFromData(PlanetData planetData)
        {
            GameObject planetGameObject = Instantiate(planetPrefab, transform);
            Planet planet = planetGameObject.GetComponent<Planet>();
            if (planet)
            {
                float scale = planetData.scale;
                float movingSpeed = planetData.changeAngleSpeed;
                float rotationSpeed = planetData.rotationSpeed;
                float startAngle = planetData.currentAngle;
                Material material = materialsForPlanets[planetData.materialIndex];
                planet.Init(planetData.smallRadius, planetData.bigRadius, scale, startAngle, rotationSpeed, movingSpeed, material, planetData.materialIndex, showHPBar);
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
