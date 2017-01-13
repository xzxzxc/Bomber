using BomberLibrary.Graphics;

namespace BomberLibrary.GameInterface
{
    public static class GameWonScreen
    {
        private static Sprite _sprite;
        private static DrawableText _text;

        public static void Draw()
        {
            _sprite.Draw();
            _text.Draw();
        }

        public static void Load(string text)
        {
            _sprite = GameData.GraphicsFactory.CreateGameWinScreenSprite();
            _text = GameData.GraphicsFactory.CreateDrawableText(0.5f * GameData.WindowWidth, 0.5f * GameData.WindowHeight, text);
        }
    }
}