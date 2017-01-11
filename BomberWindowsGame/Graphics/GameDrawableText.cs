using BomberLib.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberWindowsGame.Graphics
{
    public class GameDrawableText:DrawableText
    {
        private static float TextDeltaY = 12f;

        public GameDrawableText(float x, float y, string text = "") : base(x, y, text)
        {
        }

        public override void Draw()
        {
            BomerWindowsGame.SpriteBatch.DrawString(BomerWindowsGame.Font, Text, new Vector2(X, Y + TextDeltaY), Color.Black, 0, BomerWindowsGame.Font.MeasureString(Text) / 2, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}