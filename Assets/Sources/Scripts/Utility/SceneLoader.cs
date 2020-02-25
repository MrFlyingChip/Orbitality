using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Scripts.Utility
{
    public delegate void CallbackFloat(float value);
    public class SceneLoader : MonoBehaviour
    {
        public CallbackFloat onLoadingProgress;
        
        private static SceneLoader _instance;
        public static SceneLoader Instance => _instance;

        private void Awake()
        {
            if (!_instance)
            {
                _instance = this;
            }
            
            DontDestroyOnLoad(this);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene("LoadingScene");
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            var loadingTask = SceneManager.LoadSceneAsync(sceneName);
            while (!loadingTask.isDone)
            {
                onLoadingProgress?.Invoke(loadingTask.progress);
                if (loadingTask.progress >= 0.9f)
                {
                    loadingTask.allowSceneActivation = true;
                }
                
                yield return null;
            }
        }
    }
}
