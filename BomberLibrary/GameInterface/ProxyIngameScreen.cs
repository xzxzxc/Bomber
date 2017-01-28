using BomberLibrary.Characters;

namespace BomberLibrary.GameInterface
{
    public class ProxyInGameScreen:IScreen
    {
        public void Draw()
        {
            GameData.CurrentLevel.Draw();
            StatusLine.Draw();
        }

        public void Load()
        {
            GameData.Player.Killed = false;
            Game.UpdateEvent += Player.PlayerTouchEnemyChecker.Check;
        }

        public void UnLoad()
        {
            
        }
    }
}