namespace BomberLibrary.GameInterface
{
    public class GameOverScreen:RealScreen
    {
        public GameOverScreen() : base(GameData.GraphicsFactory.CreateGameOverScreenSprite(),
            new[]{GameData.ButtonFactory.CreateMenuButton(0.5f * GameData.WindowWidth,
                0.5f * GameData.WindowHeight, "Start New Game", Game.StartNew)})
        {
        }
    }
}