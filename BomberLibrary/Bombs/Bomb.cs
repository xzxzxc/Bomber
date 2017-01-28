using System;
using System.Threading;
using System.Threading.Tasks;
using BomberLibrary.Graphics;
using BomberLibrary.Interfaces;
using BomberLibrary.Levels.Cells;
using BomberLibrary.Sound;

namespace BomberLibrary.Bombs
{
    public delegate void Boom();
    public abstract class Bomb:IDrawable
    {
        private readonly Sprite _sprite;
        private readonly SoundEffect _soundEffect;
        private DateTime _clockStartedTime;
        private TimeSpan _beforeBoom;
        public event Boom Boom;
        public readonly int Radious;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        public Cell Cell => GameData.CurrentMap.GetCell(_sprite.X, _sprite.Y);
        public float X { set { _sprite.X = value; } }
        public float Y { set { _sprite.Y = value; } }

        protected Bomb(Sprite sprite, SoundEffect soundEffect, TimeSpan time, int radious)
        {
            _sprite = sprite;
            _soundEffect = soundEffect;
            _beforeBoom = time;
            Radious = radious;
            _sprite.StartDrawingAnimationInCycle(0);
            Game.PauseEvent += StopClock;
            Game.ContinueEvent += ContinueClock;
            StartNewClockTask();
        }

        private void StartNewClockTask()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Task.Factory.StartNew(StartClock, _cancellationToken);
        }

        public void Draw()
        {
            _sprite.Draw();
        }

        private async Task StartClock()
        {
            _clockStartedTime = DateTime.Now;
            try
            {
                await Task.Delay(_beforeBoom, _cancellationToken);
            }
            catch (TaskCanceledException)
            {
                return;
            }
            _soundEffect.Play();
            Boom?.Invoke();
            _sprite.StopAnimation();
            Finialize();
        }

        public void StopClock()
        {
            _sprite.StopAnimation();
            _cancellationTokenSource.Cancel();
            _beforeBoom = DateTime.Now - _clockStartedTime;
        }

        private void ContinueClock()
        {
            _sprite.StartDrawingAnimationInCycle(0);
            StartNewClockTask();
        }

        private void Finialize()
        {
            Game.PauseEvent -= StopClock;
            Game.ContinueEvent -= ContinueClock;
        }
    }
}