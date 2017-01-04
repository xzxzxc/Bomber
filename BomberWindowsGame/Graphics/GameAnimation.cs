using System;
using BomberLib.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberWindowsGame.Graphics
{
    public class GameAnimation:Animation
    {
        public readonly Texture2D Texture;
        public Rectangle SourceRectangle;
        public Rectangle DestinationRectangle;

        public GameAnimation(Texture2D texture, int rows, int columns, TimeSpan animationTime):base(rows, columns, animationTime)
        {
            Texture = texture;
        }

        public override void Draw(float x, float y)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)CurrentFrame / (float)Columns);
            int column = CurrentFrame % Columns;

            SourceRectangle = new Rectangle(width * column, height * row, width, height);
            DestinationRectangle = new Rectangle((int)x, (int)y, width, height);

            ToBeDrawn.Animations.Add(this);
        }
    }
}