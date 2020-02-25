using UnityEngine;

namespace Sources.Scripts.Core
{
    public class GameController
    {
        private const int MinPlanetsNumber = 3;
        private const int MaxPlanetsNumber = 5;
        
        private PlanetsSpawner _planetsSpawner;
        
        public void StartGame(PlanetsSpawner planetsSpawner)
        {
            _planetsSpawner = planetsSpawner;

            _planetsSpawner.SpawnPlanets(Random.Range(MinPlanetsNumber, MaxPlanetsNumber), 4, 10);
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
