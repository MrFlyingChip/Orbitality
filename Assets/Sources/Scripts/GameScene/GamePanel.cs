using Sources.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Scripts.GameScene
{
    public class GamePanel : MonoBehaviour
    {
        public Button hidePanel;
        private CallbackVoid _onHide;

        public class GamePanelParams
        {
        }
        
        public void ShowPanel(CallbackVoid onHide)
        {
            _onHide = onHide;
            if (hidePanel)
            {
                hidePanel.onClick.AddListener(OnHide);
            }
            
            gameObject.SetActive(true);
        }

        public virtual void Init(GamePanelParams panelParams)
        {
            
        }

        private void OnHide()
        {
            _onHide?.Invoke();
            HidePanel();
        }

        public void HidePanel()
        {
            if (hidePanel)
            {
                hidePanel.onClick.RemoveListener(OnHide);
            }
           
            gameObject.SetActive(false);
        }
    }
}
