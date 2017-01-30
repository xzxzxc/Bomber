using BomberLibrary.Controls;
using BomberLibrary.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace BomberIOS.Controls
{
	public class TouchButton : Button
	{
		public TouchButton(float x, float y, ButtonDelegate buttonClickedAction, Sprite backgroundSprite = null,
						   string text = null) :
		base(x, y, buttonClickedAction, backgroundSprite, text) { }

		protected override bool IsEntered()
		{
			var touchCollection = TouchPanel.GetState();
			foreach (var touch in touchCollection)
			{
				if (touch.Position.X < X + Width && touch.Position.X > X && touch.Position.Y < Y + Height &&
					touch.Position.Y > Y)
					return true;
			}
			return false;
		}
	}
}
