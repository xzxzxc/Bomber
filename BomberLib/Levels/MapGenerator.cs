using System;
using BomberLib.Cells;
using BomberLib.Charackters;

namespace BomberLib.Levels
{
    public static class MapGenerator
    {
        private static Map _currentMap = GameData.CurrentMap;
        private static int Width => _currentMap.CellsLengthX;
        private static int Height => _currentMap.CellsLengthY;
        private static Random _rnd;

        public static Map GenerateMap(int width, int height, int treesNum, int[] bombsNums)
        {
            Cell[,] cells = new Cell[width, height];
            _currentMap = new Map(cells);
            if (_rnd==null) _rnd = new Random();
            MakeBorders();
            MakeRocks();
            MakeExit();
            MakeBombsItems(bombsNums);
            MakeTrees(treesNum);
            MakeGrass();
            SetCellsPositions();
            return _currentMap;
        }

        private static void SetCellsPositions()
        {
            for (int i = 0; i < _currentMap.CellsLengthX; i++)
            {
                for (int j = 0; j < _currentMap.CellsLengthY; j++)
                {
                    _currentMap.Cells[i, j].X = i * GameData.CellWidth;
                    _currentMap.Cells[i, j].Y = j * GameData.CellHeight;
                }
            }
        }
        
        private static void MakeBombsItems(int[] bombsNums)
        {
            for (int i = 0; i < bombsNums.Length; i++)
            {
                while (bombsNums[i] > 0)
                {
                    int x;
                    int y;
                    GenerateRandomPos(out x, out y);
                    if (IsAllowed(x, y))
                    {
                        _currentMap.Cells[x, y] = new BombCell(i + 1);
                        bombsNums[i]--;
                    }
                }
            }
        }

        public static Enemy GenerateEnemy()
        {
            if (_rnd == null) _rnd = new Random();
            int x;
            int y;
            GenerateRandomPos(out x, out y);
            return IsAllowedForEnemy(x, y) ? new Enemy(x * GameData.CellWidth, y * GameData.CellHeight) : GenerateEnemy();
        }

        private static bool IsAllowedForEnemy(int x, int y)
        {
            if (!(_currentMap[x, y] is GrassCell)) return false; // Enemy can stand only on grass
            
            // Start pos of player isn't allowed
            if (x == 1 && y == 1) return false;

            // We don't want to block player
            if (x < 5 && y == 1)
                return false;
            if (x == 1 && y < 5)
                return false;

            return true;
        }

        private static bool IsAllowed(int x, int y)
        {
            if (_currentMap[x, y] != null) return false; // Cell already defined
            
            // Start pos of player isn't allowed
            if (x == 1 && y == 1) return false;

            // We don't want to block player
            if (x < 5 && y == 1 && (_currentMap[1, 2] != null || _currentMap[1, 3] != null || _currentMap[1, 4] != null))
                return false;
            if (x == 1 && y < 5 && (_currentMap[2, 1] != null || _currentMap[3, 1] != null || _currentMap[4, 1] != null))
                return false;

            return true;
        }

        private static void MakeExit()
        {
            int x;
            int y;
            GenerateRandomPos(out x, out y);
            if (!IsAllowed(x, y))
            {
                MakeExit();
                return;
            }
            _currentMap.Cells[x, y] = new ExitCell();
        }

        private static void GenerateRandomPos(out int x, out int y)
        {
            x = 1 + _rnd.Next(Width - 2);
            y = 1 + _rnd.Next(Height - 2);
        }

        private static void MakeTrees(int treesNum)
        {
            while (treesNum > 0)
            {
                int x;
                int y;
                GenerateRandomPos(out x, out y);
                if (IsAllowed(x, y))
                {
                    _currentMap.Cells[x, y] = new TreeCell();
                    treesNum--;
                }
            }
        }

        private static void MakeGrass()
        {
            for (int i = 1; i < Width - 1; i++)
            {
                for (int j = 1; j < Height - 1; j++)
                {
                    if (_currentMap.Cells[i, j] == null)
                        _currentMap.Cells[i, j] = new GrassCell();
                }
            }
        }

        private static void MakeRocks()
        {
            for (int i = 1; i < Width - 1; i++)
            {
                for (int j = 1; j < Height - 1; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0)
                        _currentMap.Cells[i, j] = new RockCell();
                }
            }
        }

        private static void MakeBorders()
        {
            for (int i = 0; i < Width; i++)
            {
                _currentMap.Cells[i, 0] = new RockCell();
                _currentMap.Cells[i, Height - 1] = new RockCell();
            }

            for (int i = 0; i < Height; i++)
            {
                _currentMap.Cells[0, i] = new RockCell();
                _currentMap.Cells[Width - 1, i] = new RockCell();
            }
        }
    }
}