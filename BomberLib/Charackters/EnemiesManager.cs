namespace BomberLib.Charackters
{
    public static class EnemiesManager
    {
        public static void StopLive()
        {
            foreach (var enemy in GameData.Enemies)
            {
                enemy.StopLive();
            }
        }

        public static void StartLive()
        {
            foreach (var enemy in GameData.Enemies)
            {
                enemy.StartLive();
            }
        }
    }
}