using BomberLibrary.Characters;

namespace BomberLibrary.GameInterface
{
    public class ProxyInGameScreen:IScreen
    {
		public virtual void Draw()
        {
            GameData.CurrentLevel.Draw();
            StatusLine.Draw();
        }

        public virtual void Load()
        {
            //GameData.Player.Killed = false;
            Game.UpdateEvent += Player.PlayerTouchEnemyChecker.Check;
        }

		public virtual void UnLoad()
        {
            //Game.UpdateEvent -= Player.PlayerTouchEnemyChecker.Check;
        }
    }
}