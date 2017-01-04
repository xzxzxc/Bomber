using System;

namespace BomberLib.Graphics
{
    public abstract class GraphicsFactory
    {
        //
        // Sprites
        //

        public abstract Sprite CreateRectangleSprite(string colorName, float width, float height);

        // Charackters
        public abstract Sprite CreatePlayerSprite();
        public abstract Sprite CreateEnemySprite();

        // Cells
        public abstract Sprite CreateTreeSprite();
        public abstract Sprite CreateRockSprite();
        public abstract Sprite CreateGrassSprite();
        public abstract Sprite CreateGrassAdterBoomSprite();

        // Items
        public abstract Sprite CreateDoorSprite();
        public abstract Sprite CreateBombItemSprite(int num);

        // Bombs
        public abstract Sprite CreateBombSprite(int num);

        // Status Line
        public abstract Sprite CreateHeartSprite();
        public abstract Sprite CreateBombSmallSprite(int num);

        // Screens
        public abstract Sprite CreatePauseScreenSprite();
        public abstract Sprite CreateStartScreenSprite();
        public abstract Sprite CreateGameOverScreenSprite();
        public abstract Sprite CreateGameWinScreenSprite();
        public abstract Sprite CreateDieScreenSprite();

        //
        // Text
        //
        public abstract DrawableText CreateDrawableText(String content = "");

        //
        // Animations
        //

        // Charackters
        protected abstract Animation CreatePlayerMoveAnimation();
        protected abstract Animation CreatePlayerDieAnimation();
        protected abstract Animation CreateEnemyMoveAnimation();
        protected abstract Animation CreateEnemyDieAnimation();

        // Cells
        protected abstract Animation CreateExplotionAnimation();

        // Bombs
        protected abstract Animation CreateBombAnimation(int num);
    }
}