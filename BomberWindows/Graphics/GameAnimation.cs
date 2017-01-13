using System;
using BomberLibrary.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberWindows.Graphics
{
    public class GameAnimation:Animation
    {
        private readonly Texture2D _texture;
        private Rectangle _sourceRectangle;
        private Rectangle _destinationRectangle;
        private static SpriteBatch SpriteBatch => BomerWindowsGame.SpriteBatch;

        public GameAnimation(Texture2D texture, float x, float y,  int rows, int columns, TimeSpan animationTime):base(x, y, rows, columns, animationTime)
        {
            _texture = texture;
        }

        public override void Draw()
        {
            base.Draw();

            int width = _texture.Width / Columns;
            int height = _texture.Height / Rows;
            int row = (int)(CurrentFrame / (float)Columns);
            int column = CurrentFrame % Columns;

            _sourceRectangle = new Rectangle(width * column, height * row, width, height);
            _destinationRectangle = new Rectangle((int)X, (int)Y, width, height);

            
            SpriteBatch.Draw(_texture, _destinationRectangle, _sourceRectangle, Color.White);
         }
    }
}