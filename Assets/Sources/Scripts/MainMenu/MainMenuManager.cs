using System;
using System.Collections.Generic;
using Sources.Scripts.Core;
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

        public List<GameObject> objectsToEnable;
        
        private void Awake()
        {
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
            newGameButton.onClick.AddListener(OnNewGameButtonClick);
            loadGameButton.onClick.AddListener(OnLoadGameButtonClick);
            quitButton.onClick.AddListener(OnQuitButtonClick);

            loadGameButton.interactable = SaveGameManager.Instance.GameSaved;
        }

        private void OnQuitButtonClick()
        {
            Application.Quit();
        }

        private void OnLoadGameButtonClick()
        {
            throw new NotImplementedException();
        }

        private void OnNewGameButtonClick()
        {
            throw new NotImplementedException();
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
