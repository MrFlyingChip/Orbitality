using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sources.Scripts.Core
{
    public delegate void CallbackVoid();
    public class GameController
    {
        public readonly UnityEvent OnGameWin = new UnityEvent();
        public readonly UnityEvent OnGameLose = new UnityEvent();
        
        private const int MinPlanetsNumber = 3;
        private const int MaxPlanetsNumber = 5;
        
        private PlanetsSpawner _planetsSpawner;

        private List<Planet> _planets;
        private Planet _playerPlanet;
        private SaveData _saveData;
        
        public void LaunchGame(PlanetsSpawner planetsSpawner)
        {
            _planetsSpawner = planetsSpawner;

            if (SaveGameManager.Instance.shouldLoadGameFromSave)
            {
                _saveData = SaveGameManager.Instance.LoadGame();
                _planets = new List<Planet>(_planetsSpawner.SpawnPlanetsFromData(_saveData));
                for (int i = 0; i < _saveData.planets.Count; i++)
                {
                    Debug.Log(_saveData.planets[i].bigRadius);
                    if (_saveData.planets[i].isPlayer)
                    {
                        _playerPlanet = _planets[i];
                    }
                }
            }
            else
            {
                _planets = new List<Planet>(_planetsSpawner.SpawnPlanets(Random.Range(MinPlanetsNumber, MaxPlanetsNumber), 
                    4, 10));
                _playerPlanet = _planets[Random.Range(0, _planets.Count)];
            }
            
            _playerPlanet.IsPlayer = true;
        }

        public void StartGame()
        {
            for (int i = 0; i < _planets.Count; i++)
            {
                Planet planet = _planets[i];
                if (_saveData != null)
                {
                    planet.SetPreset(_saveData.planets[i].preset);
                    planet.CurrentHealth = _saveData.planets[i].currentHealth;
                }
                else
                {
                    planet.SetPreset(PlanetsSettings.GetRandomPreset());
                }
                
                planet.OnPlanetDestroyed.AddListener(OnPlanetDestroyed);
            }

            _planetsSpawner.StartMovingPlanets();
            BotsController.Instance.StartBots(_planets);
        }

        public SaveData GetLevelSaveData()
        {
            SaveData saveData = new SaveData();
            foreach (var planet in _planets)
            {
                PlanetData planetData = planet.GetPlanetData();
                saveData.planets.Add(planetData);
            }

            return saveData;
        }

        private void OnPlanetDestroyed(Planet planet)
        {
            _planets.Remove(planet);
            
            if (_playerPlanet == planet)
            {
                LoseGame();
            } 
            else if (_planets.Count == 1)
            {
                WinGame();
            }
        }

        private void WinGame()
        {
            OnGameWin.Invoke();
        }

        private void LoseGame()
        {
            OnGameLose.Invoke();
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
