using BomberLib.Graphics;

namespace BomberLib.GameInterface
{
    public static class StatusLine
    {
        private static int? Bomb1Num => GameData.Player?.Bomb1Num;
        private static int? Bomb2Num => GameData.Player?.Bomb2Num;
        private static int? Bomb3Num => GameData.Player?.Bomb3Num;
        private static int? Lifes => GameData.Player?.Life;
        private static Sprite _heartSprite;
        private static Sprite _bomb1Sprite;
        private static Sprite _bomb2Sprite;
        private static Sprite _bomb3Sprite;
        private static DrawableText _lifesText;
        private static DrawableText _bomb1Text;
        private static DrawableText _bomb2Text;
        private static DrawableText _bomb3Text;
        private static Sprite _whiteLine;

        public static void Draw(float x, float y)
        {
            _whiteLine.Draw(x, y);
            _heartSprite.Draw(x, y);
            _lifesText.Text = "x" + Lifes.ToString();
            _lifesText.Draw(x + 0.6f * GameData.CellWidth, y);

            _bomb1Sprite.Draw(x + GameData.CellWidth, y);
            _bomb1Text.Text = "x" + Bomb1Num.ToString();
            _bomb1Text.Draw(x + 1.6f * GameData.CellWidth, y);

            _bomb2Sprite.Draw(x + 2f * GameData.CellWidth, y);
            _bomb2Text.Text = "x" + Bomb2Num.ToString();
            _bomb2Text.Draw(x + 2.6f * GameData.CellWidth, y);

            _bomb3Sprite.Draw(x + 3f * GameData.CellWidth, y);
            _bomb3Text.Text = "x" + Bomb3Num.ToString();
            _bomb3Text.Draw(x + 3.6f * GameData.CellWidth, y);
        }

        public static void Load()
        {
            _whiteLine = GameData.GraphicsFactory.CreateRectangleSprite("TransparentWhite",
                4f * GameData.CellWidth, 0.4f * GameData.CellHeight);
            _heartSprite = GameData.GraphicsFactory.CreateHeartSprite();
            _bomb1Sprite = GameData.GraphicsFactory.CreateBombSmallSprite(1);
            _bomb2Sprite = GameData.GraphicsFactory.CreateBombSmallSprite(2);
            _bomb3Sprite = GameData.GraphicsFactory.CreateBombSmallSprite(3);
            _lifesText = GameData.GraphicsFactory.CreateDrawableText("x");
            _bomb1Text = GameData.GraphicsFactory.CreateDrawableText("x");
            _bomb2Text = GameData.GraphicsFactory.CreateDrawableText("x");
            _bomb3Text = GameData.GraphicsFactory.CreateDrawableText("x");
        }
    }
}