using System;
using System.Runtime.Serialization;
using BomberLib.Bombs;
using BomberLib.Graphics;
using BomberLib.Interfaces;

namespace BomberLib.Cells
{
    [Serializable]
    public abstract class Cell : IDrawable, IMovable, ISerializable
    {
        public Sprite Sprite { get; protected set; }

        [NonSerialized] public Bomb Bomb;
        public bool IsMovable { get; protected set; }

        public virtual float X
        {
            get { return Sprite.X; }
            set
            {
                Sprite.X = value;
                if (Bomb != null) Bomb.X = value;
            }
        }

        public virtual float Y
        {
            get { return Sprite.Y; }
            set
            {
                Sprite.Y = value;
                if (Bomb != null) Bomb.Y = value;
            }
        }

        protected Cell(Sprite sprite)
        {
            Sprite = sprite;
            IsMovable = false;
        }

        protected void ChangeSprite(Sprite newSprite)
        {
            Sprite.StopAnimation();
            float x = X;
            float y = Y;
            Sprite = newSprite;
            Sprite.X = x;
            Sprite.Y = y;
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
                if (this == enemie.Cell)
                    enemie.Kill();
            }

            if (this == GameData.Player.Cell)
            {
                Sprite.StartDrawingAnimationToEnd(0); // Boom Animation
                GameData.Player.Kill();
            }
        }

        public virtual void UnBoom()
        {
            Bomb?.StopClock();
            ClearBomb();
        }

        public virtual void Draw()
        {
            Sprite.Draw();
            Bomb?.Draw();
        }

        public virtual void MoveLeft(float speed)
        {
            X -= speed;
        }

        public virtual void MoveRight(float speed)
        {
            X += speed;
        }

        public virtual void MoveUp(float speed)
        {
            Y -= speed;
        }

        public virtual void MoveDown(float speed)
        {
            Y += speed;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IsMovable", IsMovable);
            info.AddValue("X", X);
            info.AddValue("Y", Y);
        }

        protected Cell(SerializationInfo propertyBag, StreamingContext context)
        {
            IsMovable = propertyBag.GetBoolean("IsMovable");
        }
    }
}