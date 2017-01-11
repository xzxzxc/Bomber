using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberWindowsGame.Graphics
{
    public class GameRectangleSprite:GameSprite
    {
        private readonly float _width;
        private readonly float _height;
        private Rectangle _rectangle;

        public GameRectangleSprite(Texture2D texture2D, float x, float y, float width, float height) : base(x, y, texture2D)
        {
            _width = width;
            _height = height;
        }

        public override void Draw()
        {
            _rectangle = new Rectangle((int) X, (int) Y, (int) _width, (int) _height);
            BomerWindowsGame.SpriteBatch.Draw(MyTexture, _rectangle, Color.White);
        }
    }
}