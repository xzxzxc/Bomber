using System;
using BomberLibrary.Interfaces;

namespace BomberLibrary.Graphics
{
    public delegate void AnimationDelegate();
    public abstract class Animation: IDrawable
    {
        private bool _inCycle;
        private bool _isEnd;
        private DateTime _prevTime;
        private readonly TimeSpan _animationTime;
        protected readonly int Rows;
        protected readonly int Columns;
        private readonly int _totalFrames;
        protected int CurrentFrame;
        protected internal float X;
        protected internal float Y;
        public event AnimationDelegate EndAnimation; 

        protected Animation(float x, float y, int rows, int columns, TimeSpan animationTime)
        {
            X = x;
            Y = y;
            Rows = rows;
            Columns = columns;
            CurrentFrame = 0;
            _totalFrames = Rows * Columns;
            _animationTime = animationTime;
            _isEnd = false;
            _inCycle = false;
        }

        /// <summary>
        /// Drawing Current frame
        /// </summary>
        public virtual void Draw()
        {
            if (_isEnd && !_inCycle)
                throw new EndException();
        }

        private void Update()
        {
            if (_isEnd && !_inCycle) return;
            var now = DateTime.Now;
            if (now - _prevTime < _animationTime) return;
            NextFrame();
            _prevTime = now;
        }

        public void StartToEnd()
        {
            Game.UpdateEvent += Update;
            _isEnd = false;
            _inCycle = false;
        }
        
        private void NextFrame()
        {
            CurrentFrame++;
            if (CurrentFrame != _totalFrames) return;
            CurrentFrame = 0;
            _isEnd = true;
            EndAnimation?.Invoke();
            //EndAnimation = null;
        }

        public void StartInCycle()
        {
            Game.UpdateEvent += Update;
            _inCycle = true;
            _isEnd = false;
        }

        public void Stop()
        {
            Game.UpdateEvent -= Update;
            _isEnd = true;
            _inCycle = false;
        }

    }

    public class EndException : Exception { }
}