using System.Runtime.Serialization;
using BomberLibrary.Characters;
using BomberLibrary.Levels;
using Newtonsoft.Json;
using PCLStorage;
using FileNotFoundException = System.IO.FileNotFoundException;

namespace BomberLibrary
{
    [DataContract]
    internal class SaveManager
    {
        private static SaveManager _loadedSaveManager;
        [DataMember]
        private readonly Player _player = GameData.Player;
        [DataMember]
        private readonly Level _level = GameData.CurrentLevel;
        [DataMember]
        private readonly int _levelNum = GameData.CurrentLevelNum;
        [DataMember]
        private readonly float _xMapOffset = GameData.XMapOffset;
        [DataMember]
        private readonly float _yMapOffset = GameData.YMapOffset;

        private SaveManager()
        { }

        public static async void Save()
        {
            IFile file = FileSystem.Current.LocalStorage.CreateFileAsync("Save",
			                                                             CreationCollisionOption.ReplaceExisting).Result;

            var ser = JsonConvert.SerializeObject(new SaveManager(), new JsonSerializerSettings
            { TypeNameHandling = TypeNameHandling.All } );

            await file.WriteAllTextAsync(ser);
        }

        public static void Load()
        {
            if (FileSystem.Current.LocalStorage.CheckExistsAsync("Save").Result ==
                ExistenceCheckResult.NotFound)
                throw new FileNotFoundException(string.Empty);

            IFile file = FileSystem.Current.LocalStorage.GetFileAsync("Save").Result;

            var jsonSave = file.ReadAllTextAsync().Result;

            if (string.IsNullOrEmpty(jsonSave))
                throw new FileNotFoundException();

            _loadedSaveManager = JsonConvert.DeserializeObject<SaveManager>(jsonSave, new JsonSerializerSettings
            { TypeNameHandling = TypeNameHandling.All});
            CopyLoadedDataToGameData();
        }

        private static void CopyLoadedDataToGameData()
        {
            GameData.Player = _loadedSaveManager._player;
            GameData.CurrentLevel = _loadedSaveManager._level;
            GameData.XMapOffset = _loadedSaveManager._xMapOffset;
            GameData.YMapOffset = _loadedSaveManager._yMapOffset;
            GameData.CurrentLevelNum = _loadedSaveManager._levelNum;
        }
    }
}