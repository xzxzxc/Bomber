using System;
using System.Collections.Generic;
using BomberLib.Interfaces;
// ReSharper disable VirtualMemberCallInConstructor

namespace BomberLib.Graphics
{
    public abstract class Sprite : IDrawable, IMovable
    {
        public abstract float Width { get; }
        public abstract float Height { get; }

        public virtual float X
        {
            get { throw new NotImplementedException(); } // acessor must have body, but will never be used
            set
            {
                foreach (var animation in _animations) animation.X = value;
            }
        }

        public virtual float Y
        {
            get { throw new NotImplementedException(); } // acessor must have body, but will never be used
            set { foreach (var animation in _animations) animation.Y = value; }
        }

        private readonly List<Animation> _animations;
        private Animation _acttiveAnimation;

        protected Sprite(float x, float y)
        {
            _animations = new List<Animation>();
            X = x;
            Y = y;
        }

        public virtual void Draw()
        {
            try
            {
                _acttiveAnimation?.Draw();
            }
            catch (EndException)
            {
                _acttiveAnimation = null;
            }
        }

        public bool IsInTouchAbove(Sprite sprite)
        {
            return sprite.Y <= Y + Height && sprite.Y > Y && ((X < sprite.X + sprite.Width && sprite.X + sprite.Width < X + Width) || (sprite.X < X + Width && X < sprite.X));
        }

        public bool IsInTouchBelow(Sprite sprite)
        {
            return sprite.Y + sprite.Height >= Y && sprite.Y + sprite.Height < Y + Height && ((X < sprite.X + sprite.Width && sprite.X + sprite.Width < X + Width) || (sprite.X < X + Width && X < sprite.X));
        }

        public bool IsInTouchLeft(Sprite sprite)
        {
            return sprite.X <= X + Width && sprite.X > X && ((Y < sprite.Y + sprite.Height && sprite.Y + sprite.Height < Y + Height) || (sprite.Y < Y + Height && Y < sprite.Y));
        }

        public bool IsInTouchRight(Sprite sprite)
        {
            return sprite.X + sprite.Width >= X && sprite.X + sprite.Width < X + Width && ((Y < sprite.Y + sprite.Height && sprite.Y + sprite.Height < Y + Height) || (sprite.Y < Y + Height && Y < sprite.Y));
        }

        public abstract void MoveLeft(float speed);

        public abstract void MoveRight(float speed);

        public abstract void MoveUp(float speed);

        public abstract void MoveDown(float speed);

        public void AddAnimations(Animation animation)
        {
            SetAnimationPosition(animation);
            _animations.Add(animation);
        }

        private void SetAnimationPosition(Animation animation)
        {
            animation.X = X;
            animation.Y = Y;
        }

        public void StartDrawingAnimationInCycle(int num)
        {
            if (_acttiveAnimation != _animations[num])
            {
                _acttiveAnimation?.Stop();
                _acttiveAnimation = _animations[num];
                _acttiveAnimation.StartInCycle();
            }
        }

        public void StartDrawingAnimationToEnd(int num)
        {
            if (_acttiveAnimation != _animations[num])
            {
                _acttiveAnimation?.Stop();
                _acttiveAnimation = _animations[num];
                _acttiveAnimation.StartToEnd();
            }
        }

        public void StopAnimation()
        {
            _acttiveAnimation?.Stop();
            _acttiveAnimation = null;
        }
    }
}