using System;
using System.Collections.Generic;
using BomberLibrary.Characters;
using BomberLibrary.GameInterface;
using FileNotFoundException = System.IO.FileNotFoundException;

namespace BomberLibrary
{
    public delegate void GameDelegate();
    public static class Game
    {
		public static event GameDelegate PauseEvent;
		public static event GameDelegate ContinueEvent;
		public static event GameDelegate UpdateEvent;

        public static void Update()
        {
            UpdateEvent?.Invoke();
        }

        public static void StartNew()
        {
			GameData.Player = new Characters.Player(GameData.CellWidth + 5, GameData.CellHeight + 5);
			GameData.CurrentLevelNum = 0;
            LoadLevel(0);
        }

        public static void NextLevel()
        {
            if (GameData.CurrentLevelNum + 1 == GameData.MaxLevelNum)
                GameData.CurrentScreen = GameData.Screens.GameWonScreen;
            else
            {
                LoadLevel(++GameData.CurrentLevelNum);
                GameData.Player.Bomb1Num += 10;
            }
        }

        private static void LoadLevel(int levelNum)
        {
            if (levelNum > GameData.MaxLevelNum)
                throw new ArgumentOutOfRangeException("There is no level number " + levelNum);
            GameData.CurrentLevel = GameData.Levels[levelNum];
            GameData.Levels[levelNum].Create();
            GameData.ClearMapOffset();
            GameData.SetPlayerPositionToStart();
            GameData.CurrentScreen = GameData.Screens.InGameScreen;
			GameData.Player.Killed = false;
        }

        public static void RestartLevel()
        {
			GameData.SetPlayerPositionToStart();
            GameData.CurrentLevel.ReLoad();
            GameData.ClearMapOffset();
            GameData.SetMinimalPlayerBombNumber();
            GameData.CurrentScreen = GameData.Screens.InGameScreen;
			GameData.Player.Killed = false;
        }

        public static void PlayerDie()
        {
            GameData.Enemies = new List<Enemy>();
            GameData.CurrentScreen = GameData.Player.Life < 1 ? GameData.Screens.GameOverScreen :
                GameData.Screens.DieScreen;
        }

        public static void Pause()
        {
            PauseEvent?.Invoke();
            GameData.CurrentScreen = GameData.Screens.PauseScreen;
        }

        public static void Continue()
        {
            ContinueEvent?.Invoke();
            GameData.CurrentScreen = GameData.Screens.InGameScreen;
        }

        public static void Load()
        {
            GameData.GameMusic = GameData.SoundFactory.CreateMusic();
            GameData.GameMusic.Play();
            GameData.CurrentScreen = GameData.Screens.StartScreen;
            StatusLine.Load(5, 5);
        }

        public static void SaveGame()
        {
            SaveManager.Save();
        }

        public static void LoadSavedGame()
        {
            try
            {
                SaveManager.Load();
            }
            catch (FileNotFoundException)
            {
                return;
                // ignore
            }

            GameData.CurrentLevel.CreateEnemies();
            GameData.CurrentScreen = GameData.Screens.InGameScreen;
        }

		public static class Player
		{
			public static void MoveLeft() { GameData.Player.MoveLeft(); }
			public static void MoveRight() { GameData.Player.MoveRight(); }
			public static void MoveUp() { GameData.Player.MoveUp(); }
			public static void MoveDown() { GameData.Player.MoveDown(); }
			public static void PlantBomb() { GameData.Player.PlantBomb(); }
			public static void StopMoving() { GameData.Player.StopMoving(); }
			public static bool Killed => GameData.Player.Killed;
		}
    }
}