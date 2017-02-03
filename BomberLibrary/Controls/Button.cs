using BomberLibrary.Graphics;
using BomberLibrary.Interfaces;

namespace BomberLibrary.Controls
{
	public delegate void ButtonDelegate();
	public abstract class Button : IDrawable, ISubscribable
	{
		private Sprite _backgroundSprite;
		private DrawableText _text;
		protected float X => _backgroundSprite.X;
		protected float Y => _backgroundSprite.Y;
		protected float Width => _backgroundSprite.Width;
		protected float Height => _backgroundSprite.Height;

		public event ButtonDelegate buttonClicked;
		public event ButtonDelegate buttonReleased;

		protected internal Button(float xCenter, float yCenter, ButtonDelegate buttonClickedAction,
								  Sprite backgroundSprite = null, string text = null,
								  ButtonDelegate buttonReleasedAction = null)
		{
			_backgroundSprite = backgroundSprite == null ?
				GameData.GraphicsFactory.CreateButtonBackgroundSprite(xCenter, yCenter) : backgroundSprite;
			if (text != null)
				_text = GameData.GraphicsFactory.CreateDrawableText(xCenter, yCenter, text);
			buttonClicked += buttonClickedAction;
			buttonReleased += buttonReleasedAction;
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
			_backgroundSprite.Draw();
			_text?.Draw();
		}

		private void Update()
		{
			if (IsEntered())
				buttonClicked.Invoke();
			else if (IsReleased())
				buttonReleased?.Invoke();
		}

		protected abstract bool IsEntered();

		protected virtual bool IsReleased() { return false; }

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