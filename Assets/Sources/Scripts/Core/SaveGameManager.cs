using UnityEngine;

namespace Sources.Scripts.Core
{
    public class SaveGameManager
    {
        private const string GameSavedStorage = "storage.gamesaved";

        private bool _gameSaved;
        
        public void Init()
        {
            LoadGameSaved();
        }

        private void LoadGameSaved()
        {
            _gameSaved = PlayerPrefs.HasKey(GameSavedStorage);
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
