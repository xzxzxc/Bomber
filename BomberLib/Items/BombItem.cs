using System;

namespace BomberLib.Items
{
    public class BombItem : Item
    {
        private readonly int _bombNum;
        public BombItem(int bombNum) : base(GameData.GraphicsFactory.CreateBombItemSprite(bombNum))
        {
            _bombNum = bombNum;
        }


        public override int GetHashCode()
        {
            switch (_bombNum)
            {
                case 1:
                    return (int) ItemsHashCodes.Bomb1;
                case 2:
                    return (int)ItemsHashCodes.Bomb2;
                case 3:
                    return (int)ItemsHashCodes.Bomb3;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}