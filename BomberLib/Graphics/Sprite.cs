using System.Collections.Generic;
using BomberLib.Interfaces;

namespace BomberLib.Graphics
{
    public abstract class Sprite : ICoordinateDrawable, ICoordinateMovable
    {
        public abstract float Width { get; }
        public abstract float Height { get; }
        public abstract float X { get; set; }
        public abstract float Y { get; set; }
        public List<Animation> Animations;
        public Animation ActtiveAnimation;

        public abstract void Draw(float x, float y);

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
            animation.EndAnimation += () => { ActtiveAnimation = null; };
            Animations.Add(animation);
        }

        public void DrawAnimationInCycle(int num, float x, float y)
        {
            if (ActtiveAnimation != Animations[num])
            {
                ActtiveAnimation?.Stop();
                ActtiveAnimation = Animations[num];
                //ActtiveAnimation.X = x;
                //ActtiveAnimation.Y = y;
                ActtiveAnimation.StartInCycle();
            }
            
            //ActtiveAnimation.Draw();
        }

        public void DrawAnimatonOneTime(int num, float x, float y)
        {
            if (ActtiveAnimation != Animations[num])
            {
                ActtiveAnimation?.Stop();
                ActtiveAnimation = Animations[num];
                //ActtiveAnimation.X = x;
                //ActtiveAnimation.Y = y;
                ActtiveAnimation.StartToEnd();
            }
            
            // ActtiveAnimation.Draw();
        }

        public void StopAnimation()
        {
            ActtiveAnimation?.Stop();
            ActtiveAnimation = null;
        }
    }
}