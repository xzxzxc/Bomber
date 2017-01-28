namespace BomberLibrary.GameInterface
{
    public class PauseScreen:RealScreen
    {
        public PauseScreen() : base(GameData.GraphicsFactory.CreatePauseScreenSprite(),
            new[]{GameData.ButtonFactory.CreateMenuButton(0.5f * GameData.WindowWidth,
                0.2f * GameData.WindowHeight, "Continue", Game.Continue),
            GameData.ButtonFactory.CreateMenuButton(0.5f * GameData.WindowWidth,
                0.4f * GameData.WindowHeight, "Save", Game.SaveGame),
            GameData.ButtonFactory.CreateMenuButton(0.5f * GameData.WindowWidth,
                0.6f * GameData.WindowHeight, "Load", Game.LoadSavedGame),
            GameData.ButtonFactory.CreateMenuButton(0.5f * GameData.WindowWidth,
                0.8f * GameData.WindowHeight, "Stop/Resume Music", GameData.GameMusic.PauseOrResume)})
        {
        }
    }
}