namespace BomberLibrary.GameInterface
{
    public class DieScreen:RealScreen
    {
        public DieScreen() : base(GameData.GraphicsFactory.CreateDieScreenSprite(),
            new []{GameData.ButtonFactory.CreateMenuButton(0.5f * GameData.WindowWidth,
                0.5f * GameData.WindowHeight, "Restart level", Game.RestartLevel)})
        {
        }
    }
}