using System;
using Sources.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Scripts.LoadingScene
{
    public class LoadingSceneManager : MonoBehaviour
    {
        public Slider loadingSlider;

        private void Start()
        {
            SceneLoader.Instance.onLoadingProgress += OnLoadingProgress;
            OnLoadingProgress(0);
        }

        private void OnLoadingProgress(float progress)
        {
            loadingSlider.value = progress;
        }
    }
}
