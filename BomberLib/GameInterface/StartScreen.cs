using BomberLib.Graphics;

namespace BomberLib.GameInterface
{
    public static class StartScreen
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
            _sprite = GameData.GraphicsFactory.CreateStartScreenSprite();
            _text = GameData.GraphicsFactory.CreateDrawableText(0.5f * GameData.WindowWidth, 0.5f * GameData.WindowHeight, text);
        }
    }
}