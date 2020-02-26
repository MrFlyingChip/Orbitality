using System;
using Sources.Scripts.Core;
using Sources.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Scripts.GameScene
{
    public class GameSceneManager : MonoBehaviour
    {
        public PlanetsSpawner planetsSpawner;
        public GamePanel startGamePanel;
        public PauseGamePanel pauseGamePanel;
        public GamePanel winGamePanel;
        public GamePanel loseGamePanel;

        public Button pauseButton;

        private void Awake()
        {
            GameController.Instance.LaunchGame(planetsSpawner);
            pauseButton.onClick.AddListener(OnPauseButtonClick);
            ShowStartGamePanel(GameController.Instance.StartGame);
            
            GameController.Instance.OnGameWin.AddListener(OnWinGame);
            GameController.Instance.OnGameLose.AddListener(OnLoseGame);
        }

        private void ShowStartGamePanel(CallbackVoid onStartPanelHide)
        {
            startGamePanel.ShowPanel(onStartPanelHide);
        }

        private void OnPauseButtonClick()
        {
            PauseGame(true);
            pauseGamePanel.Init(new PauseGamePanel.PauseGamePanelParams
            {
                OnQuitButtonClick = QuitGame,
                OnSaveQuitButtonClick = SaveAndQuitGame
            });
            pauseGamePanel.ShowPanel(() => PauseGame(false));
        }

        private void QuitGame()
        {
            PauseGame(false);
            SceneLoader.Instance.LoadScene("MainMenuScene");
        }

        private void SaveAndQuitGame()
        {
            SaveGameManager.Instance.SaveGame(GameController.Instance.GetLevelSaveData());
            QuitGame();
        }

        private void OnWinGame()
        {
            PauseGame(true);
            winGamePanel.ShowPanel(QuitGame);
        }

        private void OnLoseGame()
        {
            PauseGame(true);
            loseGamePanel.ShowPanel(QuitGame);
        }

        private void PauseGame(bool pause)
        {
            Time.timeScale = pause ? 0 : 1;
        }
    }
}
