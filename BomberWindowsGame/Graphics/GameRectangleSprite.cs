using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberWindowsGame.Graphics
{
    public class GameRectangleSprite:GameSprite
    {
        private float _width;
        private float _height;
        public Rectangle Rectangle;

        public GameRectangleSprite(Texture2D texture2D, float width, float height) : base(texture2D)
        {
            _width = width;
            _height = height;
        }

        public override void Draw(float x, float y)
        {
            Rectangle = new Rectangle((int) x, (int) y, (int) _width, (int) _height);
            base.Draw(x, y);
        }
    }
}