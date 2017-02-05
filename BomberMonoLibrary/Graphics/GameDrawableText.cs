using BomberLibrary.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberMonoLibrary.Graphics
{
    public class GameDrawableText:DrawableText
    {
		private static SpriteFont Font => MonoGame.Font;
        private static SpriteBatch SpriteBatch => MonoGame.SpriteBatch;

        private bool DrawInBlack;

        public GameDrawableText(float x, float y, string text = "", bool drawInBlack = false) : base(x, y, text)
        {
            DrawInBlack = drawInBlack;
        }

        public override void Draw()
        {
            SpriteBatch.DrawString(Font, Text, new Vector2(X, Y), DrawInBlack?Color.Black:Color.White, 0,
                Font.MeasureString(Text) / 2, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}