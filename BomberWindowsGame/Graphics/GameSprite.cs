using BomberLib.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberWindowsGame.Graphics
{
    public class GameSprite:Sprite
    {
        // This is a texture we can render.
        protected readonly Texture2D MyTexture;

        // Set the coordinates to draw the sprite at.
        private Vector2 _spritePosition;

        public override float Width => MyTexture.Width;
        public override float Height => MyTexture.Height;

        public override float X
        {
            get
            {
                return _spritePosition.X;
            }
            set
            {
                _spritePosition.X = value;
                base.X = value;
            }
        }

        public override float Y
        {
            get
            {
                return _spritePosition.Y;
            }
            set
            {
                _spritePosition.Y = value;
                base.Y = value;
            }
        }


        public GameSprite(float x, float y, Texture2D texture2D):base(x, y)
        {
            MyTexture = texture2D;
            //Animations = new List<Animation>();
        }

        public override void Draw()
        {
            BomerWindowsGame.SpriteBatch.Draw(MyTexture, _spritePosition, Color.White);
            base.Draw();
        }

        public override void MoveLeft(float speed)
        {
            X -= speed;
        }

        public override void MoveRight(float speed)
        {
            X += speed;
        }

        public override void MoveUp(float speed)
        {
            Y -= speed;
        }

        public override void MoveDown(float speed)
        {
            Y += speed;
        }
    }
}