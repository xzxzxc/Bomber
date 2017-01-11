using System.Collections.Generic;

namespace BomberLib.Characters
{
    public static class EnemiesManager
    {
        internal static readonly Queue<Enemy> KilledEnemies = new Queue<Enemy>();
        public static void UpdateAll()
        {
            foreach (var enemy in GameData.Enemies.ToArray())
            {
                enemy.Update();
            }
        }

        public static void RemoveKilledEnemyFromGameData()
        {
            GameData.Enemies.Remove(KilledEnemies.Dequeue());
        }
    }
}