using System;
using System.IO;
using Sources.Scripts.Utility;
using UnityEngine;

namespace Sources.Scripts.Core
{
    public class SaveGameManager
    {
        private const string GameSavedStorage = "storage.gamesaved";
        private string _gameSaveDataPath = Application.persistentDataPath + "/saveload.json";
        
        private bool _gameSaved;

        public bool shouldLoadGameFromSave;
        
        public void Init()
        {
            LoadGameSaved();
        }

        private void LoadGameSaved()
        {
            _gameSaved = PlayerPrefs.HasKey(GameSavedStorage);
        }

        public void SaveGame(SaveData saveData)
        {
            string jsonData = JsonUtility.ToJson(saveData, false);
            File.WriteAllText(_gameSaveDataPath, jsonData);
            PlayerPrefs.SetInt(GameSavedStorage, 1);
        }

        public SaveData LoadGame()
        {
            try
            {
                return JsonUtility.FromJson<SaveData>( File.ReadAllText(_gameSaveDataPath));
            }
            catch (Exception e)
            {
                SceneLoader.Instance.LoadScene("MainMenuScene");
                return null;
            }
        }
        
        public bool GameSaved => _gameSaved;
        
        //Singleton
        private static SaveGameManager _instance;
        public static SaveGameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SaveGameManager();
                }

                return _instance;
            }
        }
    }
}
