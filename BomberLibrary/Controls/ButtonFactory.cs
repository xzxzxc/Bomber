namespace BomberLibrary.Controls
{
    public abstract class ButtonFactory
    {
        public abstract Button CreateWhiteMenuButton(float x, float y, string text, ButtonDelegate action);
        public abstract Button CreateBlackMenuButton(float x, float y, string text, ButtonDelegate action);
    }
}