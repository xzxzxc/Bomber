using BomberLibrary.Graphics;
using BomberLibrary.Interfaces;

namespace BomberLibrary.Controls
{
    public delegate void ButtonDelegate();
    public abstract class Button:IDrawable, ISubscribable
    {
        private Sprite backgroundSprite;
        private DrawableText text;
        protected float X=>backgroundSprite.X;
        protected float Y=>backgroundSprite.Y;        
        protected float Width => backgroundSprite.Width;
        protected float Height => backgroundSprite.Height;

        public event ButtonDelegate buttonClicked;

        protected internal Button(float xCenter, float yCenter, string text, ButtonDelegate buttonClickedAction)
        {
            backgroundSprite = GameData.GraphicsFactory.CreateButtonBackgroundSprite(xCenter, yCenter);
            this.text = GameData.GraphicsFactory.CreateDrawableText(xCenter, yCenter, text);
            buttonClicked += buttonClickedAction;
        }

        public void Subscribe()
        {
            Game.UpdateEvent += Update;
        }

        ~Button()
        {
            UnSubscribe();
        }

        public void Draw()
        {
            backgroundSprite.Draw();
            text.Draw();
        }

        private void Update()
        {
            if (IsEntered())
            {
                buttonClicked.Invoke();
            }
        }

        protected abstract bool IsEntered();

        public void UnSubscribe()
        {
            Game.UpdateEvent -= Update;
        }
    }

    public interface ISubscribable
    {
        void Subscribe();
        void UnSubscribe();
    }
}