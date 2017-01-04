using BomberLib.Graphics;
using Microsoft.Xna.Framework;

namespace BomberWindowsGame.Graphics
{
    public class GameDrawableText:DrawableText
    {
        public static float TextDeltaY = 12f;
        public Vector2 TextPosition;

        public GameDrawableText(string text = "") : base(text)
        {
        }

        public override void Draw(float x, float y)
        {
            TextPosition = new Vector2(x, y + TextDeltaY);
            ToBeDrawn.Texts.Add(this);
        }
    }
}