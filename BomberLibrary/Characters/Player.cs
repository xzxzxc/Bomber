using System.Runtime.Serialization;
using BomberLibrary.Bombs;
using BomberLibrary.Levels;

namespace BomberLibrary.Characters
{
    [DataContract]
    public class Player : Charackter
    {
        [DataMember]
        public byte Life = 5;
		public event GameDelegate PlayerDieEvent;

        public bool Killed;

        public Player(float x, float y, int bomb1Num=10, int bomb2Num=2, int bomb3Num=0) : base(GameData.GraphicsFactory.CreatePlayerSprite(x, y))
        {
            Bomb1Num = bomb1Num;
            Bomb2Num = bomb2Num;
            Bomb3Num = bomb3Num;
            Killed = false;
        }

        public override void Kill()
        {
			if (Killed) return;
			Killed = true;
			PlayerDieEvent?.Invoke();
			Life--;
            base.Kill();
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
            if (Y < GameData.WindowHeight*0.25 && GameData.CurrentMap[0, 0].Y <= -CurrentSpeed)
            {
                GameData.CurrentMap.MoveDown(CurrentSpeed);
                CheckInTouchUpperCell(CurrentSpeed);
                if (CheckItem())
                {
                    GetItem();
                }//TODO:Change to CheckInTouchItem();
            }
            else
            {
                base.MoveUp();
            }
            Sprite.StartDrawingAnimationInCycle(1);
        }

        public override void MoveDown()
        {
			if (Y + Sprite.Height > GameData.WindowHeight*0.75 && GameData.CurrentMap[0, GameData.CurrentMap.CellsLengthY - 1].Y
                >= GameData.WindowHeight - GameData.CellHeight + CurrentSpeed)
            {
                GameData.CurrentMap.MoveUp(CurrentSpeed);
                CheckInTouchLowerCell(CurrentSpeed);
                if (CheckItem())
                {
                    GetItem();
                }
            }
            else
            {
                base.MoveDown();
            }
            Sprite.StartDrawingAnimationInCycle(1);
        }

        public override void MoveLeft()
        {
            if (X < GameData.WindowWidth*0.25 && GameData.CurrentMap[0, 0].X <= -CurrentSpeed)
            {
                GameData.CurrentMap.MoveRight(CurrentSpeed);
                CheckInTouchLeftCell(CurrentSpeed);
                if (CheckItem())
                {
                    GetItem();
                }
            }
            else
            {
                base.MoveLeft();
            }
            Sprite.StartDrawingAnimationInCycle(1);
        }

        public override void MoveRight()
        {
            if (X > GameData.WindowWidth*0.75 && GameData.CurrentMap[GameData.CurrentMap.CellsLengthX - 1, 0].X
                >= GameData.WindowWidth - GameData.CellWidth + CurrentSpeed)
            {
                GameData.CurrentMap.MoveLeft(CurrentSpeed);
                CheckInTouchRightCell(CurrentSpeed);
                if (CheckItem())
                {
                    GetItem();
                }
            }
            else
            {
                base.MoveRight();
            }
            Sprite.StartDrawingAnimationInCycle(1);
        }

        public static class PlayerTouchEnemyChecker
        {
            public static void Check()
            {
                if (!CheckPlayerTouchEnemie()) return;
                foreach (var enemy in GameData.Enemies)
                    enemy.AbortMoving();
				Game.UpdateEvent -= PlayerTouchEnemyChecker.Check;
				GameData.Player.Kill();
            }

            private static bool CheckPlayerTouchEnemie()
            {
                foreach (var enemie in GameData.Enemies.ToArray())
                {
                    if (enemie.Cell == GameData.Player.Cell)
                        return true;
                }
                return false;
            }
        }
    }
}