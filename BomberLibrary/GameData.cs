using System;
using System.Collections.Generic;
using BomberLibrary.Characters;
using BomberLibrary.Controls;
using BomberLibrary.GameInterface;
using BomberLibrary.Graphics;
using BomberLibrary.Levels;
using BomberLibrary.Sound;

namespace BomberLibrary
{
    public static class GameData
    {
        private static GraphicsFactory _graphicsFactory;
        public static GraphicsFactory GraphicsFactory
        {
            get
            {
                if (_graphicsFactory == null)
                    throw new NullReferenceException("Sprite factory is null now");
                return _graphicsFactory;
            }
            set { _graphicsFactory = value; }
        }
        private static SoundFactory _soundFactory;
        public static SoundFactory SoundFactory
        {
            get
            {
                if (_soundFactory == null)
                    throw new NullReferenceException("Sound factory is null now");
                return _soundFactory;
            }
            set { _soundFactory = value; }
        }
        private static ButtonFactory _buttonFactory;
        public static ButtonFactory ButtonFactory
        {
            get
            {
                if (_buttonFactory == null)
                    throw new NullReferenceException("Button factory is null now");
                return _buttonFactory;
            }
            set { _buttonFactory = value; }
        }

        public static Music GameMusic;
        public static int CellWidth;
        public static int CellHeight;

        public static Map CurrentMap => CurrentLevel.Map;
        public static Level CurrentLevel;
        public static int CurrentLevelNum;
        public static List<Enemy> Enemies = new List<Enemy>();
        public static Player Player;

        public static Level[] Levels { get; } = { new Level(21, 11, 15, 3, 15), new Level(25, 11, 20, 6, 15, 5), new Level(21, 21, 25, 9, 15, 10, 5)};
        public static int MaxLevelNum => Levels.Length;
        public static float WindowWidth;
        public static float WindowHeight;
        public static float XMapOffset;
        public static float YMapOffset;
        public static float XStandartOffset => CellWidth * 0.15f;
        public static float YStandartOffset => CellHeight * 0.05f;

        public static void SetPlayerPositionToStart()
        {
			Player.StopMoving();
            Player.X = CellWidth + 5;
            Player.Y = CellHeight + 5;
        }

        public static void SetMinimalPlayerBombNumber()
        {
            Player.Bomb1Num = 10;
            Player.Bomb2Num = 0;
            Player.Bomb3Num = 0;
        }

        public static void ClearMapOffset()
        {
            XMapOffset = 0;
            YMapOffset = 0;
        }

        private static IScreen _screen;

        public static IScreen CurrentScreen
        {
            get { return _screen; }
            set
            {
                _screen?.UnLoad();
                _screen = value;
                _screen.Load();
            }
        }

        public static class Screens
        {
            public static IScreen StartScreen { get; } = new StartScreen() as IScreen;
            public static IScreen DieScreen { get; } = new DieScreen() as IScreen;
            public static IScreen GameOverScreen { get; } = new GameOverScreen() as IScreen;
            public static IScreen GameWonScreen { get; } = new GameWonScreen() as IScreen;
            public static IScreen PauseScreen { get; } = new PauseScreen() as IScreen;
			public static IScreen InGameScreen { get; set; } = new ProxyInGameScreen() as IScreen;
        }
    }
}