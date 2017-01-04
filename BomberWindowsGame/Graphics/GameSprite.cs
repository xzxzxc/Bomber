using System.Collections.Generic;
using BomberLib.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberWindowsGame.Graphics
{
    public class GameSprite:Sprite
    {
        // This is a texture we can render.
        public Texture2D myTexture;

        // Set the coordinates to draw the sprite at.
        public Vector2 SpritePosition;

        public override float Width => myTexture.Width;
        public override float Height => myTexture.Height;
        public override float X { get { return SpritePosition.X; } set { SpritePosition.X = value; } }
        public override float Y { get { return SpritePosition.Y; } set { SpritePosition.Y = value; } }


        public GameSprite(Texture2D texture2D)
        {
            myTexture = texture2D;
            Animations = new List<Animation>();
        }

        public override void Draw(float x, float y)
        {
            SpritePosition = new Vector2(x, y);
            ToBeDrawn.Sprites.Add(this);
            ActtiveAnimation?.Draw(x, y);
        }

        public override void MoveLeft(float speed)
        {
            SpritePosition.X -= speed;
        }

        public override void MoveRight(float speed)
        {
            SpritePosition.X += speed;
        }

        public override void MoveUp(float speed)
        {
            SpritePosition.Y -= speed;
        }

        public override void MoveDown(float speed)
        {
            SpritePosition.Y += speed;
        }
    }
}