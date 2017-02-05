namespace BomberLibrary.GameInterface
{
    public class GameWonScreen:RealScreen
    {
        public GameWonScreen() : base(GameData.GraphicsFactory.CreateGameWinScreenSprite(),
            new[]{GameData.ButtonFactory.CreateBlackMenuButton(0.5f * GameData.WindowWidth,
                0.7f * GameData.WindowHeight, "> Start New Game", Game.StartNew)})
        {
        }
    }
}