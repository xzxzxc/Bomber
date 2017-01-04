using System.Threading;

namespace BomberLib.Charackters
{
    public static class PlayerTouchEnemyChecker
    {
        private static Thread _thread;

        public static void Start()
        {
            _thread = new Thread(Check) { IsBackground = true };
            _thread.Start();
        }

        public static void Stop()
        {
            _thread.Abort();
        }

        private static void Check()
        {
            while (true)
            {
                if (CheckPlayerTouchEnemie())
                {
                    foreach (var enemy in GameData.Enemies)
                        enemy.AbortMoving();
                    GameData.Player.Kill();
                    return;
                }
                Thread.Sleep(100);
            }
        }

        private static bool CheckPlayerTouchEnemie()
        {
            foreach (var enemie in GameData.Enemies.ToArray())
            {
                if (enemie.Cell == GameData.Player.Cell)
                    return true;
            }
            return false;
        }
    }
}