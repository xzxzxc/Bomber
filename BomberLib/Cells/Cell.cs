using System;
using System.Runtime.Serialization;
using BomberLib.Bombs;
using BomberLib.Graphics;
using BomberLib.Interfaces;

namespace BomberLib.Cells
{
    [Serializable]
    public abstract class Cell : IDrawable, ICoordinateMovable, ISerializable
    {
        [NonSerialized] protected Sprite _sprite;
        public Sprite Sprite => _sprite;
        [NonSerialized] public Bomb Bomb;
        protected bool _isMovable;
        public bool IsMovable => _isMovable;

        public float X
        {
            get { return _sprite.X; }
            set { _sprite.X = value; }
        }

        public float Y
        {
            get { return _sprite.Y; }
            set { _sprite.Y = value; }
        }

        protected Cell(Sprite sprite)
        {
            _sprite = sprite;
            _isMovable = false;
        }

        protected void ChangeSprite(Sprite newSprite)
        {
            _sprite.StopAnimation();
            float x = X;
            float y = Y;
            _sprite = newSprite;
            _sprite.X = x;
            _sprite.Y = y;
        }

        public bool TryPlantBomb(Bomb bomb)
        {
            if (Bomb != null)
                return false;
            Bomb = bomb;
            return true;
        }

        public void ClearBomb()
        {
            Bomb = null;
        }

        public virtual void Boom()
        {
            foreach (var enemie in GameData.Enemies.ToArray())
            {
                if (this == GameData.CurrentMap.GetCell(enemie.X, enemie.Y))
                    enemie.Kill();
            }

            if (this == GameData.CurrentMap.GetCell(GameData.Player.X, GameData.Player.Y))
            {
                _sprite.DrawAnimatonOneTime(0, X, Y); // Boom Animation
                GameData.Player.Kill();
            }
        }

        public virtual void UnBoom()
        {
            Bomb?.StopClock();
            ClearBomb();
        }


        public void Draw()
        {
            Draw(X, Y);
        }

        protected virtual void Draw(float x, float y)
        {
            _sprite.Draw(x, y);
            Bomb?.Draw(x + 0.1f*GameData.CellWidth, y + 0.1f*GameData.CellHeight);
        }

        public virtual void MoveLeft(float speed)
        {
            _sprite.MoveLeft(speed);
        }

        public virtual void MoveRight(float speed)
        {
            _sprite.MoveRight(speed);
        }

        public virtual void MoveUp(float speed)
        {
            _sprite.MoveUp(speed);
        }

        public virtual void MoveDown(float speed)
        {
            _sprite.MoveDown(speed);
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IsMovable", _isMovable);
            info.AddValue("X", X);
            info.AddValue("Y", Y);
        }

        protected Cell(SerializationInfo propertyBag, StreamingContext context)
        {
            _isMovable = propertyBag.GetBoolean("IsMovable");
        }
    }
}