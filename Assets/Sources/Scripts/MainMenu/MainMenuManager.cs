using System;
using System.Collections.Generic;
using Sources.Scripts.Core;
using Sources.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Scripts.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public Button mainMenuButton;
        public Button newGameButton;
        public Button loadGameButton;
        public Button quitButton;

        public PlanetsSpawner planetsSpawner;
        
        public List<GameObject> objectsToEnable;
        
        private void Awake()
        {
            SaveGameManager.Instance.Init();
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
            newGameButton.onClick.AddListener(OnNewGameButtonClick);
            loadGameButton.onClick.AddListener(OnLoadGameButtonClick);
            quitButton.onClick.AddListener(OnQuitButtonClick);

            loadGameButton.interactable = SaveGameManager.Instance.GameSaved;

            SpawnPlanets();
        }

        private void SpawnPlanets()
        {
            planetsSpawner.SpawnPlanets(4, 5.5f, 13);
            planetsSpawner.StartMovingPlanets();
        }
        
        private void OnQuitButtonClick()
        {
            Application.Quit();
        }

        private void OnLoadGameButtonClick()
        {
            SaveGameManager.Instance.shouldLoadGameFromSave = true;
            SceneLoader.Instance.LoadScene("GameScene");
        }

        private void OnNewGameButtonClick()
        {
            SaveGameManager.Instance.shouldLoadGameFromSave = false;
            SceneLoader.Instance.LoadScene("GameScene");
        }

        private void OnMainMenuButtonClick()
        {
            foreach (var objectToEnable in objectsToEnable)
            {
                objectToEnable.SetActive(!objectToEnable.activeSelf);
            }
        }
    }
}
