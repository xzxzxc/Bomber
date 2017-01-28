using BomberLibrary.Controls;
using Microsoft.Xna.Framework.Input;


namespace BomberCrossPlatform.Controls
{
    public class DesctopButton:Button
    {
        private ButtonState _previousMousLeftButtonState;
        public DesctopButton(float x, float y, string text, ButtonDelegate buttonClickedAction) : base(x, y, text,
            buttonClickedAction)
        {
            _previousMousLeftButtonState = Mouse.GetState().LeftButton;
        }

        protected override bool IsEntered()
        {
            ButtonState buttonState = Mouse.GetState().LeftButton;
            if (Mouse.GetState().X < X + Width && Mouse.GetState().X > X && Mouse.GetState().Y < Y + Height &&
                Mouse.GetState().Y > Y)
            {
                if (buttonState == ButtonState.Pressed && _previousMousLeftButtonState == ButtonState.Released)
                {
                    _previousMousLeftButtonState = buttonState;
                    return true;
                }
            }
            _previousMousLeftButtonState = buttonState;
            return false;
        }
    }
}