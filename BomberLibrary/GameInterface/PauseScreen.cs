namespace BomberLibrary.GameInterface
{
    public class PauseScreen:RealScreen
    {
        public PauseScreen() : base(GameData.GraphicsFactory.CreatePauseScreenSprite(),
            new[]{GameData.ButtonFactory.CreateWhiteMenuButton(0.5f * GameData.WindowWidth,
                0.5f * GameData.WindowHeight, "> Continue", Game.Continue),
            GameData.ButtonFactory.CreateWhiteMenuButton(0.5f * GameData.WindowWidth,
                0.6f * GameData.WindowHeight, "> Save", Game.SaveGame),
            GameData.ButtonFactory.CreateWhiteMenuButton(0.5f * GameData.WindowWidth,
                0.7f * GameData.WindowHeight, "> Load", Game.LoadSavedGame),
            GameData.ButtonFactory.CreateWhiteMenuButton(0.5f * GameData.WindowWidth,
                0.8f * GameData.WindowHeight, "> Stop/Resume Music", GameData.GameMusic.PauseOrResume)})
        {
        }
    }
}