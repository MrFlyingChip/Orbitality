using System;
using Sources.Scripts.Core;
using UnityEngine;

namespace Sources.Scripts.GameScene
{
    public class GameSceneManager : MonoBehaviour
    {
        public PlanetsSpawner planetsSpawner;
        
        private void Awake()
        {
            GameController.Instance.StartGame(planetsSpawner);
        }
    }
}
