using System;
using System.Threading;
using BomberLib.Cells;
using BomberLib.Graphics;
using BomberLib.Interfaces;
using BomberLib.Sound;

namespace BomberLib.Bombs
{
    public delegate void Boom();
    public abstract class Bomb:ICoordinateDrawable
    {
        private readonly Sprite _sprite;
        private readonly SoundEffect _soundEffect;
        private readonly TimeSpan _boomAfter;
        public event Boom Boom;
        public readonly int Radious;
        private readonly Thread _clockThread;
        public Cell Cell => GameData.CurrentMap.GetCell(_sprite.X, _sprite.Y);

        protected Bomb(Sprite sprite, SoundEffect soundEffect, TimeSpan time, int radious)
        {
            _sprite = sprite;
            _soundEffect = soundEffect;
            _boomAfter = time;
            Radious = radious;

            _clockThread = new Thread(StartClock) {IsBackground = true};
            _clockThread.Start();
        }

        public void Draw(float x, float y)
        {
            _sprite.Draw(x, y);
            _sprite.DrawAnimationInCycle(0, x, y);
        }

        private void StartClock()
        {
            Thread.Sleep(_boomAfter);
            _soundEffect.Play();
            Boom?.Invoke();
            StopClock();
        }

        public void StopClock()
        {
            _clockThread.Abort();
        }
    }
}