using System.Runtime.Serialization;
using BomberLibrary.Bombs;
using BomberLibrary.Graphics;
using BomberLibrary.Interfaces;

namespace BomberLibrary.Cells
{
    [DataContract]
    public abstract class Cell : IDrawable, IMovable
    {
        public Sprite Sprite { get; private set; }
        public Bomb Bomb;

        [DataMember]
        public bool IsMovable { get; protected set; }

        [DataMember]
        public virtual float X
        {
            get { return Sprite.X; }
            set
            {
                Sprite.X = value;
                if (Bomb != null) Bomb.X = value + GameData.XStandartOffset;
            }
        }

        [DataMember]
        public virtual float Y
        {
            get { return Sprite.Y; }
            set
            {
                Sprite.Y = value;
                if (Bomb != null) Bomb.Y = value + GameData.YStandartOffset;
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

        public void MoveLeft(float speed)
        {
            X -= speed;
        }

        public void MoveRight(float speed)
        {
            X += speed;
        }

        public void MoveUp(float speed)
        {
            Y -= speed;
        }

        public void MoveDown(float speed)
        {
            Y += speed;
        }
    }
}