using BomberLibrary.Controls;

namespace BomberIOS.Controls
{
	public class TouchTextButton:TouchButton
	{
		public TouchTextButton(float x, float y, ButtonDelegate buttonClickedAction, string text) : 
		base(x, y, buttonClickedAction, text:text)
		{ }
	}
}
