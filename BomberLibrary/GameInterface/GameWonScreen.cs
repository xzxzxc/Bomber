namespace BomberLibrary.GameInterface
{
    public class GameWonScreen:RealScreen
    {
        public GameWonScreen() : base(GameData.GraphicsFactory.CreateGameWinScreenSprite(),
            new[]{GameData.ButtonFactory.CreateMenuButton(0.5f * GameData.WindowWidth,
                0.5f * GameData.WindowHeight, "Start New Game", Game.StartNew)})
        {
        }
    }
}