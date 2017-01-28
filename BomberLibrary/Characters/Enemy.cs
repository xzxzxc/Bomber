using System;
using System.Collections.Generic;

namespace BomberLibrary.Characters
{

    public class Enemy : Charackter
    {
        private DateTime _previousTime;
        private TimeSpan _sleepTime;
        private int _moveCounter;
        private int _moveDir;
        private static readonly Random Rnd = new Random();
        private bool _stopMoving = false;

        public Enemy(float x, float y) : base(GameData.GraphicsFactory.CreateEnemySprite(x, y))
        {
            _previousTime = DateTime.Now;
            _sleepTime = TimeSpan.FromMilliseconds(50);
            _moveCounter = 0;
            Game.UpdateEvent += Update;
        }


        public override void Kill()
        {
            base.Kill();
            KilledManager.KilledEnemies.Enqueue(this);
            _sleepTime = TimeSpan.MaxValue;
        }

        public override void Draw()
        {
            Sprite?.Draw();
        }

        private void Update()
        {
            if (_stopMoving) return;
            var now = DateTime.Now;
            if (now - _previousTime < _sleepTime) return;
            _previousTime = now;
            if (IsPlayerVisible())
                MoveToPlayer();
            else
                MoveSomewhere();
        }

        private bool IsPlayerVisible()
        {
            return GameData.Player.Cell.X == Cell?.X || GameData.Player.Cell.Y == Cell.Y;
        }

        private void MoveToPlayer()
        {
            if (GameData.Player.Cell.X == Cell.X)
            {
                MoveInDirection(GameData.Player.Cell.Y <= Cell.Y ? 0 : 2);
                _sleepTime = TimeSpan.FromMilliseconds(50);
            }
            if (GameData.Player.Cell.Y == Cell.Y)
            {
                MoveInDirection(GameData.Player.Cell.X <= Cell.X ? 1 : 3);
                _sleepTime = TimeSpan.FromMilliseconds(50);
            }
        }

        public void AbortMoving()
        {
            Sprite.StopAnimation();
            _stopMoving = true;
        }

        private void MoveSomewhere()
        {
            if (_moveCounter == 0)
            {
                _moveCounter = Rnd.Next(50);
                _moveDir = Rnd.Next(4);
            }
            MoveInDirection(_moveDir);
            _moveCounter--;
        }

        private void MoveInDirection(int moveDirection)
        {
            switch (moveDirection)
            {
                case 0:
                    MoveUp();
                    break;
                case 1:
                    MoveLeft();
                    break;
                case 2:
                    MoveDown();
                    break;
                case 3:
                    MoveRight();
                    break;
            }
            Sprite.StartDrawingAnimationInCycle(0);
        }

        public static class KilledManager
        {
            internal static readonly Queue<Enemy> KilledEnemies = new Queue<Enemy>();

            public static void RemoveFromGameData()
            {
                GameData.Enemies.Remove(KilledEnemies.Dequeue());
            }
        }
    }
}