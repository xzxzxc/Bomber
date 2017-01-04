using System;
using System.Threading;

namespace BomberLib.Charackters
{
    public class Enemy : Charackter
    {
        private Thread _thread;
        private static Random _rnd = new Random();
        public Enemy(int xPos, int yPos) : base(GameData.GraphicsFactory.CreateEnemySprite(), xPos, yPos)
        {
            StartLive();
        }

        public void StartLive()
        {
            _thread = new Thread(StartAI) {IsBackground = true};
            _thread.Start();
        }

        public override void Kill()
        {
            base.Kill();
            _thread.Abort();
            GameData.Enemies.Remove(this);
        }

        protected override void Draw(float x, float y)
        {
            Sprite?.Draw(x, y);
        }

        private void StartAI()
        {
            while (true)
            {
                MoveSomewhere();
                Thread.Sleep(_rnd.Next(1000));
            }
        }

        public void AbortMoving()
        {
            _thread.Abort();
        }

        private void MoveSomewhere()
        {
            var moveDirection = _rnd.Next(4);
            for (int i = 0; i < _rnd.Next(500); i++)
            {
                MoveInDirection(moveDirection);
                Thread.Sleep(50);
            }
            
        }

        private void MoveInDirection(int moveDirection)
        {
            switch (moveDirection)
            {
                case 0:
                    MoveUp(_currentSpeed);
                    break;
                case 1:
                    MoveLeft(_currentSpeed);
                    break;
                case 2:
                    MoveDown(_currentSpeed);
                    break;
                case 3:
                    MoveRight(_currentSpeed);
                    break;
            }
        }

        public void StopLive()
        {
            _thread.Abort();
        }
    }
}