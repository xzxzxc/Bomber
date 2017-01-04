using BomberLib.Bombs;
using BomberLib.Cells;

namespace BomberLib.Levels
{
    public static class BombPlanter
    {
        public static bool TryPlant(Cell cell, Bomb bomb)
        {
            bool result = cell.TryPlantBomb(bomb);

            if (result)
            {
                cell.Bomb.Boom += () =>
                {
                    cell.ClearBomb();
                    cell.Boom();
                    BoomNear(bomb); 
                };
            }

            return result;
        }

        private static void BoomNear(Bomb bomb)
        {
            Cell currentLeftCell = bomb.Cell;
            Cell currentRightCell = bomb.Cell;
            Cell currentUpperCell = bomb.Cell;
            Cell currentLowerCell = bomb.Cell;
            for (int i = 0; i < bomb.Radious; i++)
            {
                currentLeftCell = currentLeftCell == null || currentLeftCell is RockCell ? null :
                    GameData.CurrentMap.GetLeftCell(currentLeftCell);
                currentLeftCell?.Boom();

                currentRightCell = currentRightCell == null || currentRightCell is RockCell ? null :
                    GameData.CurrentMap.GetRightCell(currentRightCell);
                currentRightCell?.Boom();

                currentUpperCell = currentUpperCell == null || currentUpperCell is RockCell ? null :
                    GameData.CurrentMap.GetUpperCell(currentUpperCell);
                currentUpperCell?.Boom();
                
                currentLowerCell = currentLowerCell == null || currentLowerCell is RockCell ? null :
                    GameData.CurrentMap.GetLowerCell(currentLowerCell);
                currentLowerCell?.Boom();
            }
        }

    }
}