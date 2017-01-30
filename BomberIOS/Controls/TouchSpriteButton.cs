using BomberLibrary.Controls;
using Microsoft.Xna.Framework.Input;
using BomberLibrary.Graphics;


namespace BomberIOS.Controls
{
	public class TouchSpriteButton:TouchButton
    {
		public TouchSpriteButton(float x, float y, ButtonDelegate buttonClickedAction,
								 Sprite backgroundSprite) : base(x, y, buttonClickedAction, backgroundSprite)
		{ }
    }
}