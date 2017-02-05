using BomberLibrary.Graphics;

namespace BomberLibrary.GameInterface
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

        public static void Draw()
        {
            _whiteLine.Draw();
            _heartSprite.Draw();
            _lifesText.Text = "x" + Lifes.ToString();
            _lifesText.Draw();

            _bomb1Sprite.Draw();
            _bomb1Text.Text = "x" + Bomb1Num.ToString();
            _bomb1Text.Draw();

            _bomb2Sprite.Draw();
            _bomb2Text.Text = "x" + Bomb2Num.ToString();
            _bomb2Text.Draw();

            _bomb3Sprite.Draw();
            _bomb3Text.Text = "x" + Bomb3Num.ToString();
            _bomb3Text.Draw();
        }

        public static void Load(float x, float y)
        {
            _whiteLine = GameData.GraphicsFactory.CreateRectangleSprite(x, y, "TransparentWhite",
                6f * GameData.CellWidth, 0.6f * GameData.CellHeight);
            _heartSprite = GameData.GraphicsFactory.CreateHeartSprite(x, y);
            _bomb1Sprite = GameData.GraphicsFactory.CreateBombSmallSprite(x + 1.5f* GameData.CellWidth, y, 1);
            _bomb2Sprite = GameData.GraphicsFactory.CreateBombSmallSprite(x + 3f * GameData.CellWidth, y, 2);
            _bomb3Sprite = GameData.GraphicsFactory.CreateBombSmallSprite(x + 4.5f * GameData.CellWidth, y, 3);
			_lifesText = GameData.GraphicsFactory.CreateDrawableText(x + 1f * GameData.CellWidth, y 
			                                                         + 0.3f * GameData.CellHeight, "x");
            _bomb1Text = GameData.GraphicsFactory.CreateDrawableText(x + 2.5f * GameData.CellWidth, y
			                                                         + 0.3f * GameData.CellHeight, "x");
            _bomb2Text = GameData.GraphicsFactory.CreateDrawableText(x + 4f * GameData.CellWidth, y
			                                                         + 0.3f * GameData.CellHeight, "x");
            _bomb3Text = GameData.GraphicsFactory.CreateDrawableText(x + 5.5f * GameData.CellWidth, y
			                                                         + 0.3f * GameData.CellHeight, "x");
        }
    }
}