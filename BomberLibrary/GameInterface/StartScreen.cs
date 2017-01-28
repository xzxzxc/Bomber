namespace BomberLibrary.GameInterface
{
    public class StartScreen : RealScreen
    {
        public StartScreen() : base(GameData.GraphicsFactory.CreateStartScreenSprite(), new[]
        {
            GameData.ButtonFactory.CreateMenuButton(0.5f * GameData.WindowWidth,
                0.25f * GameData.WindowHeight, "Start new Game", Game.StartNew),

            GameData.ButtonFactory.CreateMenuButton(0.5f * GameData.WindowWidth,
                0.5f * GameData.WindowHeight, "Load", Game.LoadSavedGame),

            GameData.ButtonFactory.CreateMenuButton(0.5f * GameData.WindowWidth,
                0.75f * GameData.WindowHeight, "Stop/Resume Music", GameData.GameMusic.PauseOrResume)}) { }
    }
}
