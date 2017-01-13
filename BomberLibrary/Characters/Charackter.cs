using System;
using System.Runtime.Serialization;
using BomberLibrary.Cells;
using BomberLibrary.Graphics;
using BomberLibrary.Interfaces;
using BomberLibrary.Items;

namespace BomberLibrary.Characters
{
    [DataContract]
    public abstract class Charackter : IDrawable, IMovable
    {
        protected Sprite Sprite;
        [DataMember]
        public float X
        {
            protected get { return Sprite.X; }
            set
            {
                if (value - GameData.XMapOffset < 0)
                    throw new Exception();
                Sprite.X = value;
            }
        }
        [DataMember]
        public float Y
        {
            protected get { return Sprite.Y; }
            set
            {
                if (value - GameData.YMapOffset < 0)
                    throw new Exception();
                Sprite.Y = value;
            }
        }
        [DataMember]
        protected float CurrentSpeed = 2.5f;

        internal Cell Cell
        {
            get
            {
                try
                {
                    return GameData.CurrentMap.GetCell(Sprite.X + Sprite.Width / 2, Sprite.Y + Sprite.Height / 2);
                }
                catch (Exception)
                {
                    return GameData.CurrentMap[0, 0];
                }
            }
        }
        [DataMember]
        public int Bomb1Num;
        [DataMember]
        public int Bomb2Num;
        [DataMember]
        public int Bomb3Num;
        
        public virtual void Kill()
        {
            Sprite.StartDrawingAnimationToEnd(1); // Die animation
        }

        public virtual void MoveLeft()
        {
            MoveLeft(CurrentSpeed);
        }

        public virtual void MoveRight()
        {
            MoveRight(CurrentSpeed);
        }

        public virtual void MoveUp()
        {
            MoveUp(CurrentSpeed);
        }

        public virtual void MoveDown()
        {
            MoveDown(CurrentSpeed);
        }

        protected Charackter(Sprite sprite)
        {
            Sprite = sprite;
        }

        protected bool CheckItem()
        {
            var decCell = Cell as DecoratedCell;
            return decCell?.Item != null;
        }

        protected void GetItem()
        {
            var decCell = Cell as DecoratedCell;
            var item = decCell.Item;

            switch (item.GetHashCode())
            {
                case (int) ItemsHashCodes.Bomb1:
                    Bomb1Num++;
                    decCell.ClearItem();
                    break;
                case (int)ItemsHashCodes.Bomb2:
                    Bomb2Num++;
                    decCell.ClearItem();
                    break;
                case (int)ItemsHashCodes.Bomb3:
                    Bomb3Num++;
                    decCell.ClearItem();
                    break;
                case (int)ItemsHashCodes.Door:
                    if (this is Player)
                        Game.NextLevel();
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public virtual void Draw()
        {
            Sprite.Draw();
        }

        private void CheckInTouchItem()
        {
            if (CheckItem())
            {
                GetItem();
            }
        }


        public void MoveLeft(float speed)
        {
            Sprite.MoveLeft(speed);
            CheckInTouchLeftCell(speed);
            CheckInTouchItem();
        }

        protected void CheckInTouchLeftCell(float speed)
        {
            foreach (Cell cell in GameData.CurrentMap)
            {
                if (!cell.IsMovable && cell.Sprite.IsInTouchLeft(Sprite))
                {
                    Sprite.X += speed;
                    return;
                }
            }
        }

        public void MoveRight(float speed)
        {
            Sprite.MoveRight(speed);
            CheckInTouchRightCell(speed);
            CheckInTouchItem();
        }

        protected void CheckInTouchRightCell(float speed)
        {
            foreach (Cell cell in GameData.CurrentMap)
            {
                if (!cell.IsMovable && cell.Sprite.IsInTouchRight(Sprite))
                {
                    Sprite.X -= speed;
                }
            }
        }

        public void MoveUp(float speed)
        {
            Sprite.MoveUp(speed);
            CheckInTouchUpperCell(speed);
            CheckInTouchItem();
        }

        protected void CheckInTouchUpperCell(float speed)
        {
            foreach (Cell cell in GameData.CurrentMap)
            {
                if (!cell.IsMovable && cell.Sprite.IsInTouchAbove(Sprite))
                {
                    Sprite.Y += speed;
                    return;
                }
            }
        }

        public void MoveDown(float speed)
        {
            Sprite.MoveDown(speed);
            CheckInTouchLowerCell(speed);
            CheckInTouchItem();
        }

        protected void CheckInTouchLowerCell(float speed)
        {
            foreach (Cell cell in GameData.CurrentMap)
            {
                if (!cell.IsMovable && cell.Sprite.IsInTouchBelow(Sprite))
                {
                    Sprite.Y -= speed;
                    return;
                }
            }
        }

        public void StopMoving()
        {
            Sprite.StopAnimation();
        }
    }
}