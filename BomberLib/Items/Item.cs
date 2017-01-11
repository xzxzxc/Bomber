using System;
using System.Dynamic;
using BomberLib.Graphics;
using BomberLib.Interfaces;

namespace BomberLib.Items
{
    public abstract class Item:IDrawable, IMovable
    {
        private readonly Sprite _sprite;
        public float X { set { _sprite.X = value; } }
        public float Y { set { _sprite.Y = value; } }

        protected Item(Sprite sprite)
        {
            _sprite = sprite;
        }

        public static Item CreateByHashCode(float x, float y, int hashCode)
        {
            switch (hashCode)
            {
                case 0:
                    return new BombItem(x, y, 1);
                case 1:
                    return new BombItem(x, y, 2);
                case 2:
                    return new BombItem(x, y, 3);
                case 3:
                    return new Door(x, y);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public abstract override int GetHashCode();

        public void Draw()
        {
            _sprite.Draw();
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