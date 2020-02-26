using System.Collections.Generic;
using UnityEngine;

namespace Sources.Scripts.Core
{
    public delegate void CallbackVoid();
    public class GameController
    {
        private const int MinPlanetsNumber = 3;
        private const int MaxPlanetsNumber = 5;
        
        private PlanetsSpawner _planetsSpawner;

        private List<Planet> _planets;
        private Planet _playerPlanet;
        
        public void LaunchGame(PlanetsSpawner planetsSpawner)
        {
            _planetsSpawner = planetsSpawner;

            _planets = new List<Planet>(_planetsSpawner.SpawnPlanets(Random.Range(MinPlanetsNumber, MaxPlanetsNumber), 
                4, 10));
            _playerPlanet = _planets[Random.Range(0, _planets.Count)];
            _playerPlanet.IsPlayer = true;
        }

        public void StartGame()
        {
            foreach (var planet in _planets)
            {
                planet.SetPreset(PlanetsSettings.GetRandomPreset());
            }
            
            _planetsSpawner.StartMovingPlanets();
        }

        //Singleton
        private static GameController _instance;
        public static GameController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameController();
                }

                return _instance;
            }
        }
}
}
