using BomberLibrary.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberMonoLibrary.Graphics
{
    public class GameDrawableText:DrawableText
    {
        private static float _textDeltaY = 8f;
        private static SpriteFont Font => BomerGame.Font;
        private static SpriteBatch SpriteBatch => BomerGame.SpriteBatch;

        public GameDrawableText(float x, float y, string text = "") : base(x, y, text)
        {
        }

        public override void Draw()
        {
            SpriteBatch.DrawString(Font, Text, new Vector2(X, Y + _textDeltaY), Color.Black, 0,
                Font.MeasureString(Text) / 2, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}