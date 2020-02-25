using Sources.Scripts.Core;
using UnityEngine;

namespace Sources.Scripts.Utility
{
    public class Launcher : MonoBehaviour
    {
        void Start()
        {
            SaveGameManager.Instance.Init();
            SceneLoader.Instance.LoadScene("MainMenuScene");
        }
    }
}
