using Sources.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Scripts.GameScene
{
    public class PauseGamePanel : GamePanel
    {
        public Button quitButton;
        public Button saveQuitButton;

        public class PauseGamePanelParams : GamePanelParams
        {
            public CallbackVoid OnQuitButtonClick;
            public CallbackVoid OnSaveQuitButtonClick;
        }

        private PauseGamePanelParams _params;
        
        public override void Init(GamePanelParams panelParams)
        {
            _params = panelParams as PauseGamePanelParams;
            if (_params != null)
            {
                quitButton.onClick.AddListener(OnQuitButtonClick);
                saveQuitButton.onClick.AddListener(OnSaveQuitButtonClick);
            }
        }

        private void OnQuitButtonClick()
        {
            _params?.OnQuitButtonClick();
            if (quitButton)
            {
                quitButton.onClick.RemoveListener(OnQuitButtonClick);
            }
            
            HidePanel();
        }

        private void OnSaveQuitButtonClick()
        {
            _params?.OnSaveQuitButtonClick();
            if (quitButton)
            {
                saveQuitButton.onClick.RemoveListener(OnSaveQuitButtonClick);
            }
            
            HidePanel();
        }
    }
}
