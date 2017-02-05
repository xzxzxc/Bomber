namespace BomberLibrary.Graphics
{
    public abstract class GraphicsFactory
    {
        //
        // Sprites
        //

        public abstract Sprite CreateRectangleSprite(float x, float y, string colorName, float width, float height);

        // Charackters
        public abstract Sprite CreatePlayerSprite(float x, float y);
        public abstract Sprite CreateEnemySprite(float x, float y);

        // Cells
        public abstract Sprite CreateTreeSprite(float x, float y);
        public abstract Sprite CreateRockSprite(float x, float y);
        public abstract Sprite CreateGrassSprite(float x, float y);
        public abstract Sprite CreateGrassAdterBoomSprite(float x, float y);

        // Items
        public abstract Sprite CreateDoorSprite(float x, float y);
        public abstract Sprite CreateBombItemSprite(float x, float y, int num);

        // Bombs
        public abstract Sprite CreateBombSprite(float x, float y, int num);

        // Status Line
        public abstract Sprite CreateHeartSprite(float x, float y);
        public abstract Sprite CreateBombSmallSprite(float x, float y, int num);

        // Screens
        public abstract Sprite CreatePauseScreenSprite();
        public abstract Sprite CreateStartScreenSprite();
        public abstract Sprite CreateGameOverScreenSprite();
        public abstract Sprite CreateGameWinScreenSprite();
        public abstract Sprite CreateDieScreenSprite();

        public abstract Sprite CreateButtonBackgroundSprite(float xCenter, float yCenter);

        //
        // Text
        //
        public abstract DrawableText CreateDrawableText(float xCener, float yCenter, string content = ""
            , bool drawInBlack = false);

		//
		// Animations
		//

		// Charackters
		protected abstract Animation CreatePlayerAnimation(float x, float y);
        protected abstract Animation CreatePlayerMoveAnimation(float x, float y);
        protected abstract Animation CreatePlayerDieAnimation(float x, float y);
		protected abstract Animation CreateEnemyAnimation(float x, float y);
        protected abstract Animation CreateEnemyMoveAnimation(float x, float y);
        protected abstract Animation CreateEnemyDieAnimation(float x, float y);

        // Cells
        protected abstract Animation CreateExplotionAnimation(float x, float y);

        // Bombs
        protected abstract Animation CreateBombAnimation(float x, float y, int num);
    }
}