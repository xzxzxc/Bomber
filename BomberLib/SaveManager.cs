using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BomberLib.Charackters;
using BomberLib.Levels;

namespace BomberLib
{
    [Serializable]
    internal class SaveManager:ISerializable
    {
        private static SaveManager _loadedSaveManager;
        private readonly Player _player = GameData.Player;
        private readonly Level _level = GameData.CurrentLevel;
        private readonly int _levelNum = GameData.CurrentLevelNum;
        private readonly float _xMapOffset = GameData.XMapOffset;
        private readonly float _yMapOffset = GameData.YMapOffset;

        private SaveManager()
        { }

        private SaveManager(SerializationInfo propertyBag, StreamingContext context)
        {
            _player = (Player)propertyBag.GetValue("Player", typeof(Player));
            _level = (Level)propertyBag.GetValue("Level", typeof(Level));
            _xMapOffset = propertyBag.GetSingle("xMapOffset");
            _yMapOffset = propertyBag.GetSingle("yMapOffset");
            _levelNum = propertyBag.GetInt32("LevelNum");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Player", _player);
            info.AddValue("Level", _level);
            info.AddValue("xMapOffset", _xMapOffset);
            info.AddValue("yMapOffset", _yMapOffset);
            info.AddValue("LevelNum", _levelNum);
        }

        public static void Save()
        {
            FileStream stream = new FileStream("Save", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, new SaveManager());
            stream.Close();
        }

        public static void Load()
        {
            FileStream stream = new FileStream("Save", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            _loadedSaveManager = (SaveManager)formatter.Deserialize(stream);
            stream.Close();
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