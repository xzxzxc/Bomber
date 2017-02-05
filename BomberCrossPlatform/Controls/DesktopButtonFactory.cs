using BomberLibrary.Controls;


namespace BomberCrossPlatform.Controls
{
    public class DesktopButtonFactory:ButtonFactory
    {
        public override Button CreateWhiteMenuButton(float x, float y, string text, ButtonDelegate action)
        {
            return new DesctopButton(x, y, text, action);
        }

        public override Button CreateBlackMenuButton(float x, float y, string text, ButtonDelegate action)
        {
            return new DesctopButton(x, y, text, action, true);
        }
    }
}