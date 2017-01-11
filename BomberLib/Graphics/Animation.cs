using System;
using BomberLib.Interfaces;

namespace BomberLib.Graphics
{
    public delegate void AnimationDelegate();
    public abstract class Animation: IDrawable
    {
        private bool InCycle;
        private bool IsEnd;
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
            IsEnd = false;
            InCycle = false;
        }

        /// <summary>
        /// Drawing Current frame
        /// </summary>
        public virtual void Draw()
        {
            Update();
            if (IsEnd && !InCycle)
                throw new EndException();
        }

        public void Update()
        {
            var now = DateTime.Now;
            if (now - _prevTime < _animationTime) return;
            NextFrame();
            _prevTime = now;
        }

        public void StartToEnd()
        {
            IsEnd = false;
            InCycle = false;
        }
        
        private void NextFrame()
        {
            CurrentFrame++;
            if (CurrentFrame != _totalFrames) return;
            CurrentFrame = 0;
            IsEnd = true;
            EndAnimation?.Invoke();
        }

        public void StartInCycle()
        {
            InCycle = true;
            IsEnd = false;
        }

        public void Stop()
        {
            IsEnd = true;
            InCycle = false;
        }

    }

    public class EndException : Exception { }
}