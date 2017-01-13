using System;
using System.Collections.Generic;
using BomberLibrary.Characters;
using BomberLibrary.Graphics;
using BomberLibrary.Levels;
using BomberLibrary.Sound;

namespace BomberLibrary
{
    public static class GameData
    {
        public static GameStatus GameStatus;
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
        public static float YStandartOffset => CellHeight * 0.10f;

        public static void SetPlayerPositionToStart()
        {
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
    }
}