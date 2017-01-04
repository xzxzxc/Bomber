using System;
using System.Collections;
using BomberLib.Cells;
using BomberLib.Interfaces;

namespace BomberLib.Levels
{
    [Serializable]
    public struct Map : IDrawable, ICoordinateMovable, IEnumerable, IEnumerator
    {
        internal readonly Cell[,] Cells;
        public int CellsLengthX => Cells.GetLength(0);
        public int CellsLengthY => Cells.GetLength(1);

        public Cell this[int x, int y] => Cells[x, y];

        public Map(Cell[,] cells)
        {
            Cells = cells;
            _xEnumeratorPosition = -1;
            _yEnumeratorPosition = -1;
        }

        public void Draw()
        {
            for (int i = 0; i < CellsLengthX; i++)
            {
                for (int j = 0; j < CellsLengthY; j++)
                {
                    Cells[i, j].Draw();
                }
            }
        }

        internal Cell GetCell(float x, float y)
        {
            try
            {
                return
                    Cells[
                        (int) ((x - GameData.XMapOffset)/GameData.CellWidth),
                        (int) ((y - GameData.YMapOffset)/GameData.CellHeight)];

            }
            catch (Exception)
            {

                return null;
            }
        }

        internal Cell GetUpperCell(Cell cell)
        {
            return GetCell(cell.X, cell.Y - GameData.CellHeight);
        }

        internal Cell GetLowerCell(Cell cell)
        {
            return GetCell(cell.X, cell.Y + GameData.CellHeight);
        }

        internal Cell GetLeftCell(Cell cell)
        {
            return GetCell(cell.X - GameData.CellWidth, cell.Y);
        }

        internal Cell GetRightCell(Cell cell)
        {
            return GetCell(cell.X + GameData.CellWidth, cell.Y);
        }

        public void MoveLeft(float speed)
        {
            GameData.XMapOffset -= speed;
            foreach (var cell in Cells)
            {
                cell.MoveLeft(speed);
            }
            foreach (var enemy in GameData.Enemies)
            {
                enemy.MoveLeft(speed);
            }
        }

        public void MoveRight(float speed)
        {
            GameData.XMapOffset += speed;
            foreach (var cell in Cells)
            {
                cell.MoveRight(speed);
            }
            foreach (var enemy in GameData.Enemies)
            {
                enemy.MoveRight(speed);
            }
        }

        public void MoveUp(float speed)
        {
            GameData.YMapOffset -= speed;
            foreach (var cell in Cells)
            {
                cell.MoveUp(speed);
            }
            foreach (var enemy in GameData.Enemies)
            {
                enemy.MoveUp(speed);
            }
        }

        public void MoveDown(float speed)
        {
            GameData.YMapOffset += speed;
            foreach (var cell in Cells)
            {
                cell.MoveDown(speed);
            }
            foreach (var enemy in GameData.Enemies)
            {
                enemy.MoveDown(speed);
            }
        }

        private int _xEnumeratorPosition;
        private int _yEnumeratorPosition;
        public IEnumerator GetEnumerator()
        {
            return this;
        }
        
        public bool MoveNext()
        {
            if (_xEnumeratorPosition < CellsLengthX - 1)
            {
                _xEnumeratorPosition++;
                if (_yEnumeratorPosition == -1) _yEnumeratorPosition++;
                return true;
            }
            if (_yEnumeratorPosition < CellsLengthY - 1)
            {
                _yEnumeratorPosition++;
                _xEnumeratorPosition = 0;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _xEnumeratorPosition = -1;
            _yEnumeratorPosition = -1;
        }

        public object Current => Cells[_xEnumeratorPosition, _yEnumeratorPosition];
    }
}
