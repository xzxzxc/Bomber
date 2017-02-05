namespace BomberLibrary.GameInterface
{
    public class StartScreen : RealScreen
    {
        public StartScreen() : base(GameData.GraphicsFactory.CreateStartScreenSprite(), new[]
        {
            GameData.ButtonFactory.CreateWhiteMenuButton(0.5f * GameData.WindowWidth,
                0.6f * GameData.WindowHeight, "> Start new Game", Game.StartNew),

            GameData.ButtonFactory.CreateWhiteMenuButton(0.5f * GameData.WindowWidth,
                0.7f * GameData.WindowHeight, "> Load", Game.LoadSavedGame),

            GameData.ButtonFactory.CreateWhiteMenuButton(0.5f * GameData.WindowWidth,
                0.8f * GameData.WindowHeight, "> Stop/Resume Music", GameData.GameMusic.PauseOrResume)}) { }
    }
}
