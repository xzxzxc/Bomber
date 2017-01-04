using System;
using BomberLib.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BomberWindowsGame.Graphics
{
    public class GameGraphicsFactory:GraphicsFactory
    {
        private readonly ContentManager _content;
        private readonly GraphicsDevice _graphicsDevice;

        public override Sprite CreateRectangleSprite(string colorName, float width, float height)
        {
            Texture2D dummyTexture = new Texture2D(_graphicsDevice, 1, 1);
            var color = new[] { ColorsManager.FromName(colorName) };
            dummyTexture.SetData(color);
            return new GameRectangleSprite(dummyTexture, width, height);
        }

        public override Sprite CreatePlayerSprite()
        {
            var playerSprite =  new GameSprite(_content.Load<Texture2D>("Images\\player"));
            playerSprite.AddAnimations(CreatePlayerMoveAnimation());
            playerSprite.AddAnimations(CreatePlayerDieAnimation());
            return playerSprite;
        }

        public override Sprite CreateEnemySprite()
        {
            var enemySprite = new GameSprite(_content.Load<Texture2D>("Images\\enemy"));
            enemySprite.AddAnimations(CreateEnemyMoveAnimation());
            enemySprite.AddAnimations(CreateEnemyDieAnimation());
            return enemySprite;
        }

        public override Sprite CreateTreeSprite()
        {
            var treeSprite =  new GameSprite(_content.Load<Texture2D>("Images\\tree"));
            treeSprite.AddAnimations(CreateExplotionAnimation());
            return treeSprite;
        }

        public override Sprite CreateRockSprite()
        {
            return new GameSprite(_content.Load<Texture2D>("Images\\rock"));
        }

        public override Sprite CreateGrassSprite()
        {
            var grassSprite =  new GameSprite(_content.Load<Texture2D>("Images\\grass"));
            grassSprite.AddAnimations(CreateExplotionAnimation());
            return grassSprite;
        }

        public override Sprite CreateGrassAdterBoomSprite()
        {
            var grassSprite = new GameSprite(_content.Load<Texture2D>("Images\\grass_after_boom"));
            grassSprite.AddAnimations(CreateExplotionAnimation());
            return grassSprite;
        }

        public override Sprite CreateDoorSprite()
        {
            return new GameSprite(_content.Load<Texture2D>("Images\\door"));
        }

        public override Sprite CreateBombItemSprite(int bombNum)
        {
            return new GameSprite(_content.Load<Texture2D>($"Images\\bomb{bombNum}"));
        }

        public override Sprite CreateBombSprite(int bombNum)
        {
            Sprite bombSprite = CreateBombItemSprite(bombNum);
            bombSprite.AddAnimations(CreateBombAnimation(bombNum));
            return bombSprite;
        }

        public override Sprite CreateHeartSprite()
        {
            return new GameSprite(_content.Load<Texture2D>("Images\\heart"));
        }

        public override Sprite CreateBombSmallSprite(int bombNum)
        {
            return new GameSprite(_content.Load<Texture2D>($"Images\\bomb{bombNum}_small"));
        }

        public override Sprite CreatePauseScreenSprite()
        {
            return new GameSprite(_content.Load<Texture2D>("Images\\pause_screen"));
        }

        public override Sprite CreateStartScreenSprite()
        {
            return new GameSprite(_content.Load<Texture2D>("Images\\start_screen"));
        }

        public override Sprite CreateGameOverScreenSprite()
        {
            return new GameSprite(_content.Load<Texture2D>("Images\\game_over_screen"));
        }

        public override Sprite CreateGameWinScreenSprite()
        {
            return new GameSprite(_content.Load<Texture2D>("Images\\game_win_screen"));
        }

        public override Sprite CreateDieScreenSprite()
        {
            return new GameSprite(_content.Load<Texture2D>("Images\\die_screen"));
        }

        public override DrawableText CreateDrawableText(String text="")
        {
            return new GameDrawableText(text);
        }

        protected override Animation CreatePlayerMoveAnimation()
        {
            return new GameAnimation(_content.Load<Texture2D>("Images\\player_moving"), 2, 2, TimeSpan.FromMilliseconds(150));
        }

        protected override Animation CreatePlayerDieAnimation()
        {
            return new GameAnimation(_content.Load<Texture2D>("Images\\player_die"), 2, 2, TimeSpan.FromMilliseconds(250));
        }

        protected override Animation CreateEnemyMoveAnimation()
        {
            return new GameAnimation(_content.Load<Texture2D>("Images\\enemy_moving"), 2, 2, TimeSpan.FromMilliseconds(150));
        }

        protected override Animation CreateEnemyDieAnimation()
        {
            return new GameAnimation(_content.Load<Texture2D>("Images\\enemy_die"), 2, 2, TimeSpan.FromMilliseconds(150));
        }

        protected override Animation CreateBombAnimation(int bombNum)
        {
            return new GameAnimation(_content.Load<Texture2D>($"Images\\bomb{bombNum}_animation"), 1, 2, 
                TimeSpan.FromMilliseconds(100));
        }

        protected override Animation CreateExplotionAnimation()
        {
            return new GameAnimation(_content.Load<Texture2D>("Images\\explosion"), 5, 5, TimeSpan.FromMilliseconds(50));
        }

        public GameGraphicsFactory(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content;
            _graphicsDevice = graphicsDevice;
        }
    }
}