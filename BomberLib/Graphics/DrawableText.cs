using BomberLib.Interfaces;

namespace BomberLib.Graphics
{
    public abstract class DrawableText:ICoordinateDrawable
    {
        public string Text;

        public DrawableText(string text)
        {
            Text = text;
        }

        public abstract void Draw(float x, float y);
    }
}