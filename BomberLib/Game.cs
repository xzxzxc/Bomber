using System;
using System.Collections.Generic;
using System.IO;
using BomberLib.Characters;
using BomberLib.GameInterface;

namespace BomberLib
{
    public static class Game
    {
        public static void StartNew()
        {
            GameData.Player = new Player(GameData.CellWidth + 5, GameData.CellHeight + 5);
            GameData.CurrentLevelNum = 0;
            LoadLevel(0);
        }

        public static void NextLevel()
        {
            if (GameData.CurrentLevelNum + 1 == GameData.MaxLevelNum)
                GameData.GameStatus = GameStatus.GameWin;
            else
            {
                //EnemiesManager.StopLive();
                LoadLevel(++GameData.CurrentLevelNum);
                GameData.Player.Bomb1Num += 10;
            }
        }

        private static void LoadLevel(int levelNum)
        {
            if (levelNum > GameData.MaxLevelNum)
                throw new ArgumentOutOfRangeException("There is no level number " + levelNum.ToString());
            GameData.CurrentLevel = GameData.Levels[levelNum];
            GameData.Levels[levelNum].Create();
            //GameData.Levels[levelNum].Load();
            GameData.ClearMapOffset();
            GameData.SetPlayerPositionToStart();
            GameData.GameStatus = GameStatus.InGame;
        }

        public static void RestartLevel()
        {
            GameData.CurrentLevel.ReLoad();
            GameData.ClearMapOffset();
            GameData.SetPlayerPositionToStart();
            GameData.SetMinimalPlayerBombNumber();
            GameData.GameStatus = GameStatus.InGame;
        }

        public static void PlayerDie()
        {
            GameData.Enemies = new List<Enemy>();
            GameData.GameStatus = GameData.Player.Life == 0 ? GameStatus.GameOverScreen : GameStatus.PlayerDeadScreen;
        }

        public static void Pause()
        {
            //EnemiesManager.StopLive();
            GameData.GameStatus = GameStatus.Pause;
        }

        public static void Continue()
        {
            //EnemiesManager.StartLive();
            GameData.GameStatus = GameStatus.InGame;
        }

        public static void Load(string startText, string pauseText, string gameOverText, string winText, string dieText)
        {
            
            GameData.GameMusic = GameData.SoundFactory.CreateMusic();
            GameData.GameMusic.Play();
            GameData.GameStatus = GameStatus.StartScreen;

            StartScreen.Load(startText);
            PauseScreen.Load(pauseText);
            GameOverScreen.Load(gameOverText);
            GameWonScreen.Load(winText);
            DieScreen.Load(dieText);
            StatusLine.Load(5, 5);
        }

        public static void SaveGame()
        {
            SaveManager.Save();
        }

        public static void LoadSavedGame()
        {
            if (!File.Exists("Save")) return;

            SaveManager.Load();
            
            GameData.CurrentLevel.CreateEnemies();
            //GameData.CurrentLevel.Load();
            GameData.GameStatus = GameStatus.InGame;
        }
    }
}