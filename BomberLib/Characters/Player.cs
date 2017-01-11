using System;
using System.Runtime.Serialization;
using BomberLib.Bombs;
using BomberLib.Characters;
using BomberLib.Levels;

namespace BomberLib.Characters
{
    [Serializable]
    public class Player : Charackter
    {
        public byte Life = 5;

        public Player(int x, int y, int bomb1Num=10, int bomb2Num=2, int bomb3Num=0) : base(GameData.GraphicsFactory.CreatePlayerSprite(x, y))
        {
            Bomb1Num = bomb1Num;
            Bomb2Num = bomb2Num;
            Bomb3Num = bomb3Num;
        }

        public override void Kill()
        {
            if (GameData.GameStatus == GameStatus.PlayerDead) return;
            base.Kill();
            GameData.GameStatus = GameStatus.PlayerDead;
            Life--;
        }

        public void PlantBomb()
        {
            if (Bomb1Num + Bomb2Num + Bomb3Num == 0) return;

            if (Bomb3Num > 0)
            {
                if (BombPlanter.TryPlant(Cell, new Bomb3(Cell.X + GameData.XStandartOffset, 
                    Cell.Y + GameData.YStandartOffset)))
                    Bomb3Num--;
            }
            else if (Bomb2Num > 0)
            {
                if (BombPlanter.TryPlant(Cell, new Bomb2(Cell.X + GameData.XStandartOffset, 
                    Cell.Y + GameData.YStandartOffset)))
                    Bomb2Num--;
            }
            else
            {
                if (BombPlanter.TryPlant(Cell, new Bomb1(Cell.X + GameData.XStandartOffset,
                    Cell.Y + GameData.YStandartOffset)))
                    Bomb1Num--;
            }
        }
        
        public override void MoveUp()
        {
            if (Y < GameData.WindowHeight*0.25 && GameData.CurrentMap[0, 0].Y <= -_currentSpeed)
            {
                GameData.CurrentMap.MoveDown(_currentSpeed);
                CheckInTouchUpperCell(_currentSpeed);
                if (CheckItem())
                {
                    GetItem();
                }
            }
            else
            {
                base.MoveUp();
            }
            Sprite.StartDrawingAnimationInCycle(0);
        }

        public override void MoveDown()
        {
            if (Y > GameData.WindowHeight*0.75 && GameData.CurrentMap[0, GameData.CurrentMap.CellsLengthY - 1].Y
                >= GameData.WindowHeight - GameData.CellHeight + _currentSpeed)
            {
                GameData.CurrentMap.MoveUp(_currentSpeed);
                CheckInTouchLowerCell(_currentSpeed);
                if (CheckItem())
                {
                    GetItem();
                }
            }
            else
            {
                base.MoveDown();
            }
            Sprite.StartDrawingAnimationInCycle(0);
        }

        public override void MoveLeft()
        {
            if (X < GameData.WindowWidth*0.25 && GameData.CurrentMap[0, 0].X <= -_currentSpeed)
            {
                GameData.CurrentMap.MoveRight(_currentSpeed);
                CheckInTouchLeftCell(_currentSpeed);
                if (CheckItem())
                {
                    GetItem();
                }
            }
            else
            {
                base.MoveLeft();
            }
            Sprite.StartDrawingAnimationInCycle(0);
        }

        public override void MoveRight()
        {
            if (X > GameData.WindowWidth*0.75 && GameData.CurrentMap[GameData.CurrentMap.CellsLengthX - 1, 0].X
                >= GameData.WindowWidth - GameData.CellWidth + _currentSpeed)
            {
                GameData.CurrentMap.MoveLeft(_currentSpeed);
                CheckInTouchRightCell(_currentSpeed);
                if (CheckItem())
                {
                    GetItem();
                }
            }
            else
            {
                base.MoveRight();
            }
            Sprite.StartDrawingAnimationInCycle(0);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Life", Life);
        }

        private Player(SerializationInfo propertyBag, StreamingContext context):base(propertyBag, context)
        {
            Life = propertyBag.GetByte("Life");
            Sprite = GameData.GraphicsFactory.CreatePlayerSprite(propertyBag.GetSingle("X"), propertyBag.GetSingle("Y"));
        }
    }
}