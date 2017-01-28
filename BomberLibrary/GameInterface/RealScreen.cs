using BomberLibrary.Controls;
using BomberLibrary.Graphics;

namespace BomberLibrary.GameInterface
{
    public abstract class RealScreen:IScreen
    {
        private readonly Button[] _buttons;
        private readonly Sprite _sprite;

        protected RealScreen(Sprite sprite, Button[] buttons)
        {
            _sprite = sprite;
            _buttons = buttons;
        }

        public void Load()
        {
            foreach (var button in _buttons)
                button.Subscribe();
        }

        public void UnLoad()
        {
            foreach (var button in _buttons)
                button.UnSubscribe();
        }

        public void Draw()
        {
            _sprite.Draw();
            foreach (var button in _buttons)
                button.Draw();
        }
    }
}