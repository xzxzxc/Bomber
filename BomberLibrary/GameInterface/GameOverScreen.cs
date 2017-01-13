using BomberLibrary.Graphics;

namespace BomberLibrary.GameInterface
{
    public static class GameOverScreen
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
            _sprite = GameData.GraphicsFactory.CreateGameOverScreenSprite();
            _text = GameData.GraphicsFactory.CreateDrawableText(0.5f * GameData.WindowWidth, 0.5f * GameData.WindowHeight, text);
        }
    }
}