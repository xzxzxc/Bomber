namespace BomberLibrary.GameInterface
{
    public class GameOverScreen:RealScreen
    {
        public GameOverScreen() : base(GameData.GraphicsFactory.CreateGameOverScreenSprite(),
            new[]{GameData.ButtonFactory.CreateWhiteMenuButton(0.5f * GameData.WindowWidth,
                0.7f * GameData.WindowHeight, "> Start New Game", Game.StartNew)})
        {
        }
    }
}