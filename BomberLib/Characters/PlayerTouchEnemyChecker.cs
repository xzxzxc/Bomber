namespace BomberLib.Characters
{
    public static class PlayerTouchEnemyChecker
    {
        public static void Check()
        {
            if (!CheckPlayerTouchEnemie()) return;
            foreach (var enemy in GameData.Enemies)
                enemy.AbortMoving();
            GameData.Player.Kill();
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