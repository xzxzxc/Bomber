using System;
using System.Collections.Generic;
using BomberLib.Cells;
using BomberLib.Characters;
using BomberLib.Interfaces;

namespace BomberLib.Levels
{
    [Serializable]
    public class Level:IDrawable
    {
        public Map Map;
        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly int _treesNum;
        private readonly int _bomb1OnMapNum;
        private readonly int _bomb2OnMapNum;
        private readonly int _bomb3OnMapNum;
        private readonly int _enemyNum;


        /// <summary>
        /// Create new game level
        /// </summary>
        /// <param name="mapWidth">Map width in cells</param>
        /// <param name="mapHeight">Map heighth in cells</param>
        /// <param name="treesNum">Number of "clear" trees</param>
        /// <param name="enemyNum">Number of enemies</param>
        /// <param name="bomb1OnMapNum">Number of bombs 1 decoreted by tree</param>
        /// <param name="bomb2OnMapNum">Number of bombs 2 decoreted by tree</param>
        /// <param name="bomb3OnMapNum">Number of bombs 3 decoreted by tree</param>
        public Level(int mapWidth, int mapHeight, int treesNum, int enemyNum, int bomb1OnMapNum, int bomb2OnMapNum = 0, int bomb3OnMapNum = 0)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            _treesNum = treesNum;
            _enemyNum = enemyNum;
            _bomb1OnMapNum = bomb1OnMapNum;
            _bomb2OnMapNum = bomb2OnMapNum;
            _bomb3OnMapNum = bomb3OnMapNum;
        }

        public void Create()
        {
            Map = MapGenerator.GenerateMap(_mapWidth, _mapHeight, _treesNum, new []{_bomb1OnMapNum, _bomb2OnMapNum, _bomb3OnMapNum});
            CreateEnemies();
        }

        public void CreateEnemies()
        {
            //EnemiesManager.StopLive();
            GameData.Enemies = new List<Enemy>();
            GC.Collect();
            for (int i = 0; i < _enemyNum; i++)
            {
                GameData.Enemies.Add(MapGenerator.GenerateEnemy());
            }
        }

        public void ReLoad()
        {
            foreach (Cell cell in Map)
            {
                cell.UnBoom();
                cell.X = cell.X - GameData.XMapOffset;
                cell.Y = cell.Y - GameData.YMapOffset;
            }
            CreateEnemies();
        }

        public void Draw()
        {
            Map.Draw();
            foreach (var enemy in GameData.Enemies.ToArray())
            {
                enemy.Draw();
            }
            GameData.Player.Draw();
        }
    }
}