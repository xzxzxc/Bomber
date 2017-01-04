using System;
using System.Threading;
using BomberLib.Interfaces;

namespace BomberLib.Graphics
{
    public delegate void  AnimationDelegate();
    public abstract class Animation: ICoordinateDrawable
    {
        private Thread _clockThread;
        public readonly TimeSpan AnimationTime;
        protected readonly int Rows;
        protected readonly int Columns;
        public readonly int TotalFrames;
        protected int CurrentFrame;
        public event AnimationDelegate EndAnimation;


        protected Animation(int rows, int columns, TimeSpan animationTime)
        {
            Rows = rows;
            Columns = columns;
            CurrentFrame = 0;
            TotalFrames = Rows * Columns;
            AnimationTime = animationTime;
        }

        /// <summary>
        /// Drawing Current frame
        /// </summary>
        public abstract void Draw(float x, float y);

        private void UpdateToEnd()
        {
            CurrentFrame = 0;
            while (CurrentFrame != TotalFrames + 1)
            {
                Thread.Sleep(AnimationTime);
                CurrentFrame++;
            }
            EndAnimation?.Invoke();
        }

        public void StartToEnd()
        {
            _clockThread = new Thread(UpdateToEnd) { IsBackground = true };
            _clockThread.Start();
        }

        private void UpdateInCycle()
        {
            CurrentFrame = 0;
            while (true)
            {
                Thread.Sleep(AnimationTime);
                NextFrame();
            }
        }


        private void NextFrame()
        {
            CurrentFrame++;
            if (CurrentFrame == TotalFrames)
                CurrentFrame = 0;
        }

        public void StartInCycle()
        {
            _clockThread = new Thread(UpdateInCycle) { IsBackground = true };
            _clockThread.Start();
        }

        public void Stop()
        {
             _clockThread.Abort();
        }
    }
}