﻿using System;
using System.Collections.Generic;
using BomberLibrary.Characters;
using BomberLibrary.GameInterface;
using FileNotFoundException = System.IO.FileNotFoundException;

namespace BomberLibrary
{
    public delegate void GameDelegate();
    public static class Game
    {
        internal static event GameDelegate PauseEvent;
        internal static event GameDelegate ContinueEvent;
        internal static event GameDelegate UpdateEvent;

        public static void Update()
        {
            //if (GameData.GameStatus == GameStatus.InGame || GameData.GameStatus == GameStatus.PlayerDead)
                UpdateEvent?.Invoke();
        }

        public static void StartNew()
        {
            GameData.Player = new Player(GameData.CellWidth + 5, GameData.CellHeight + 5);
            GameData.CurrentLevelNum = 0;
            LoadLevel(0);
        }

        public static void NextLevel()
        {
            if (GameData.CurrentLevelNum + 1 == GameData.MaxLevelNum)
                GameData.CurrentScreen = GameData.Screens.GameWonScreen;
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
                throw new ArgumentOutOfRangeException("There is no level number " + levelNum);
            GameData.CurrentLevel = GameData.Levels[levelNum];
            GameData.Levels[levelNum].Create();
            GameData.ClearMapOffset();
            GameData.SetPlayerPositionToStart();
            //GameData.GameStatus = GameStatus.InGame;
            GameData.Screens.StartScreen.UnLoad();
            GameData.CurrentScreen = GameData.Screens.InGameScreen;
            
        }

        public static void RestartLevel()
        {
            GameData.CurrentLevel.ReLoad();
            GameData.ClearMapOffset();
            GameData.SetPlayerPositionToStart();
            GameData.SetMinimalPlayerBombNumber();
            GameData.CurrentScreen = GameData.Screens.InGameScreen;
        }

        public static void PlayerDie()
        {
            GameData.Enemies = new List<Enemy>();
            GameData.CurrentScreen = GameData.Player.Life == 0 ? GameData.Screens.GameOverScreen :
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
    }
}