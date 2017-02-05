using BomberLibrary.Controls;
using BomberLibrary.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace BomberIOS.Controls
{
	public class TouchButton : Button
	{
		private bool? isStopBePressed;

		public TouchButton(float x, float y, ButtonDelegate buttonClickedAction, Sprite backgroundSprite = null,
		                   string text = null, ButtonDelegate buttonReleasedAction = null, bool drawInBlack = false) :
		base(x, y, buttonClickedAction, backgroundSprite, text, buttonReleasedAction, drawInBlack:drawInBlack)
		{
		}

		protected override bool IsEntered()
		{
			var touchCollection = TouchPanel.GetState();
			foreach (var touch in touchCollection)
			{
				if (touch.Position.X < X + Width && touch.Position.X > X && touch.Position.Y < Y + Height &&
					touch.Position.Y > Y)
				{
					isStopBePressed = false;
					return true;
				}
			}
			if (isStopBePressed != null && isStopBePressed == false)
				isStopBePressed = true;
			else
				isStopBePressed = null;
			return false;
		}

		protected override bool IsReleased()
		{
			if (isStopBePressed == null || isStopBePressed == false)
				return false;
			else
				return true;
		}
	}
}
