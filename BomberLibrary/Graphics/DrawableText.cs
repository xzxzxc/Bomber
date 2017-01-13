using BomberLibrary.Interfaces;

namespace BomberLibrary.Graphics
{
    public abstract class DrawableText:IDrawable
    {
        public string Text;
        protected float X;
        protected float Y;

        protected DrawableText(float x, float y, string text)
        {
            X = x;
            Y = y;
            Text = text;
        }

        public abstract void Draw();
    }
}