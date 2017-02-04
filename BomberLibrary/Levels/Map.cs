using System;
using System.Collections;
using System.Runtime.Serialization;
using BomberLibrary.Interfaces;
using BomberLibrary.Levels.Cells;

namespace BomberLibrary.Levels
{
    [DataContract]
    public struct Map : IDrawable, IMovable, IEnumerable
    {
        [DataMember]
        internal readonly Cell[,] Cells;
        public int CellsLengthX => Cells.GetLength(0);
        public int CellsLengthY => Cells.GetLength(1);

        public Cell this[int x, int y] => Cells[x, y];

        public Map(Cell[,] cells)
        {
            Cells = cells;
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
            int xNum = (int) ((x - GameData.XMapOffset) / GameData.CellWidth);
            int yNum = (int) ((y - GameData.YMapOffset) / GameData.CellHeight);
            return Cells[Math.Min(Math.Max(0, xNum), CellsLengthX - 1), Math.Min(Math.Max(0, yNum), CellsLengthY - 1)];

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

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
                for (int j = 0; j < Cells.GetLength(1); j++)
                    yield return Cells[i, j];
        }
    }
}
