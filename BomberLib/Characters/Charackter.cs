using System;
using System.Runtime.Serialization;
using BomberLib.Cells;
using BomberLib.Graphics;
using BomberLib.Interfaces;
using BomberLib.Items;

namespace BomberLib.Characters
{
    [Serializable]
    public abstract class Charackter : IDrawable, IMovable, ISerializable
    {
        [NonSerialized]
        protected Sprite Sprite;
        public float X
        {
            get { return Sprite.X; }
            set
            {
                if (value - GameData.XMapOffset < 0)
                    throw new Exception();
                Sprite.X = value;
            }
        }
        public float Y
        {
            get { return Sprite.Y; }
            set
            {
                if (value - GameData.YMapOffset < 0)
                    throw new Exception();
                Sprite.Y = value;
            }
        }
        protected float _currentSpeed = 2.5f;

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

        public int Bomb1Num;
        public int Bomb2Num;
        public int Bomb3Num;

        protected Charackter(SerializationInfo propertyBag, StreamingContext context)
        {
            _currentSpeed = propertyBag.GetSingle("CurrentSpeed");
            Bomb1Num = propertyBag.GetInt32("Bomb1Num");
            Bomb2Num = propertyBag.GetInt32("Bomb2Num");
            Bomb3Num = propertyBag.GetInt32("Bomb3Num");
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CurrentSpeed", _currentSpeed);
            info.AddValue("Bomb1Num", Bomb1Num);
            info.AddValue("Bomb2Num", Bomb2Num);
            info.AddValue("Bomb3Num", Bomb3Num);
            info.AddValue("X", X);
            info.AddValue("Y", Y);
        }

        public virtual void Kill()
        {
            Sprite.StartDrawingAnimationToEnd(1); // Die animation
        }

        public virtual void MoveLeft()
        {
            MoveLeft(_currentSpeed);
        }

        public virtual void MoveRight()
        {
            MoveRight(_currentSpeed);
        }

        public virtual void MoveUp()
        {
            MoveUp(_currentSpeed);
        }

        public virtual void MoveDown()
        {
            MoveDown(_currentSpeed);
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