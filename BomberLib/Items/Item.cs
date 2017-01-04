using System;
using BomberLib.Graphics;
using BomberLib.Interfaces;

namespace BomberLib.Items
{
    public abstract class Item:ICoordinateDrawable, ICoordinateMovable
    {
        //public float Width => _sprite.Width;
        //public float Height => _sprite.Height;
        private readonly Sprite _sprite;
        //public float X { get; set; }
        //public float Y { get; set; }

        protected Item(Sprite sprite)
        {
            _sprite = sprite;
        }

        public static Item CreateByHashCode(int hashCode)
        {
            switch (hashCode)
            {
                case 0:
                    return new BombItem(0);
                case 1:
                    return new BombItem(1);
                case 2:
                    return new BombItem(2);
                case 3:
                    return new Door();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public abstract override int GetHashCode();

        public void Draw(float x, float y)
        {
            _sprite.Draw(x, y);
        }

        public void MoveLeft(float speed)
        {
            _sprite.MoveLeft(speed);
        }

        public void MoveRight(float speed)
        {
            _sprite.MoveRight(speed);
        }

        public void MoveUp(float speed)
        {
            _sprite.MoveUp(speed);
        }

        public void MoveDown(float speed)
        {
            _sprite.MoveDown(speed);
        }
    }
}