using BomberLib.Interfaces;

namespace BomberLib.Graphics
{
    public abstract class DrawableText:IDrawable
    {
        public string Text;
        protected float X;
        protected float Y;

        public DrawableText(float x, float y, string text)
        {
            X = x;
            Y = y;
            Text = text;
        }

        public abstract void Draw();
    }
}