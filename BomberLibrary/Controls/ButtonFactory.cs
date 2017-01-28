namespace BomberLibrary.Controls
{
    public abstract class ButtonFactory
    {
        public abstract Button CreateMenuButton(float x, float y, string text, ButtonDelegate action);
    }
}