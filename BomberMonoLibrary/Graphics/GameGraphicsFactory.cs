﻿using System;
using BomberLibrary;
using BomberLibrary.Characters;
using BomberLibrary.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BomberMonoLibrary.Graphics
{
    public class GameGraphicsFactory:GraphicsFactory
    {
        private readonly ContentManager _content;
        private readonly GraphicsDevice _graphicsDevice;

        public override Sprite CreateRectangleSprite(float x, float y, string colorName, float width, float height)
        {
            Texture2D dummyTexture = new Texture2D(_graphicsDevice, 1, 1);
            var color = new[] { ColorsManager.FromName(colorName) };
            dummyTexture.SetData(color);
            return new GameRectangleSprite(dummyTexture, x, y, width, height);
        }

        public override Sprite CreatePlayerSprite(float x, float y)
        {
            var playerSprite =  new GameSprite(x, y, _content.Load<Texture2D>("Images\\player"));
			playerSprite.AddAnimations(CreatePlayerAnimation(x, y));
            playerSprite.AddAnimations(CreatePlayerMoveAnimation(x, y));
            var dieAnimation = CreatePlayerDieAnimation(x, y);
            dieAnimation.EndAnimation += Game.PlayerDie;
            playerSprite.AddAnimations(dieAnimation);
            return playerSprite;
        }

        public override Sprite CreateEnemySprite(float x, float y)
        {
            var enemySprite = new GameSprite(x, y, _content.Load<Texture2D>("Images\\enemy"));
			enemySprite.AddAnimations(CreateEnemyAnimation(x, y));
			enemySprite.AddAnimations(CreateEnemyMoveAnimation(x, y));	
            var dieAnimation = CreateEnemyDieAnimation(x, y);
            dieAnimation.EndAnimation += Enemy.KilledManager.RemoveFromGameData;
            enemySprite.AddAnimations(dieAnimation);
            return enemySprite;
        }

		public override Sprite CreateTreeSprite(float x, float y)
        {
            var treeSprite =  new GameSprite(x, y, _content.Load<Texture2D>("Images\\tree"));
            treeSprite.AddAnimations(CreateExplotionAnimation(x, y));
            return treeSprite;
        }

        public override Sprite CreateRockSprite(float x, float y)
        {
            return new GameSprite(x, y, _content.Load<Texture2D>("Images\\rock"));
        }

        public override Sprite CreateGrassSprite(float x, float y)
        {
            var grassSprite =  new GameSprite(x, y, _content.Load<Texture2D>("Images\\grass"));
            grassSprite.AddAnimations(CreateExplotionAnimation(x, y));
            return grassSprite;
        }

        public override Sprite CreateGrassAdterBoomSprite(float x, float y)
        {
            var grassSprite = new GameSprite(x, y, _content.Load<Texture2D>("Images\\grass_after_boom"));
            grassSprite.AddAnimations(CreateExplotionAnimation(x, y));
            return grassSprite;
        }

        public override Sprite CreateDoorSprite(float x, float y)
        {
            return new GameSprite(x, y, _content.Load<Texture2D>("Images\\door"));
        }

        public override Sprite CreateBombItemSprite(float x, float y, int num)
        {
            return new GameSprite(x, y, _content.Load<Texture2D>($"Images\\bomb{num}"));
        }

        public override Sprite CreateBombSprite(float x, float y, int num)
        {
            Sprite bombSprite = CreateBombItemSprite(x, y, num);
            bombSprite.AddAnimations(CreateBombAnimation(x, y, num));
            return bombSprite;
        }

        public override Sprite CreateHeartSprite(float x, float y)
        {
            return new GameSprite(x, y, _content.Load<Texture2D>("Images\\heart"));
        }

        public override Sprite CreateBombSmallSprite(float x, float y, int num)
        {
            return new GameSprite(x, y, _content.Load<Texture2D>($"Images\\bomb{num}_small"));
        }

        public override Sprite CreatePauseScreenSprite()
        {
            return new GameSprite(0, 0, _content.Load<Texture2D>("Images\\pause_screen"));
        }

        public override Sprite CreateStartScreenSprite()
        {
            return new GameSprite(0, 0, _content.Load<Texture2D>("Images\\start_screen"));
        }

        public override Sprite CreateGameOverScreenSprite()
        {
            return new GameSprite(0, 0, _content.Load<Texture2D>("Images\\game_over_screen"));
        }

        public override Sprite CreateGameWinScreenSprite()
        {
            return new GameSprite(0, 0, _content.Load<Texture2D>("Images\\game_win_screen"));
        }

        public override Sprite CreateDieScreenSprite()
        {
            return new GameSprite(0, 0, _content.Load<Texture2D>("Images\\die_screen"));
        }

        public override Sprite CreateButtonBackgroundSprite(float xCenter, float yCenter)
        {
            var texture = _content.Load<Texture2D>("Images\\button");
            return new GameSprite(xCenter - texture.Width / 2f, yCenter - texture.Height / 2f, texture);
        }

        public override DrawableText CreateDrawableText(float x, float y, string content="", bool drawInBlack = false)
        {
            return new GameDrawableText(x, y, content, drawInBlack);
        }

		protected override Animation CreatePlayerAnimation(float x, float y)
		{
			return new GameAnimation(_content.Load<Texture2D>("Images\\player_staying"), x, y, 3, 3,
				TimeSpan.FromMilliseconds(150));
		}

        protected override Animation CreatePlayerMoveAnimation(float x, float y)
        {
            return new GameAnimation(_content.Load<Texture2D>("Images\\player_moving"), x, y, 2, 9,
                TimeSpan.FromMilliseconds(150));
        }

        protected override Animation CreatePlayerDieAnimation(float x, float y)
        {
            return new GameAnimation(_content.Load<Texture2D>("Images\\player_die"), x, y, 2, 3,
                TimeSpan.FromMilliseconds(250));
        }

		protected override Animation CreateEnemyAnimation(float x, float y)
		{
			return new GameAnimation(_content.Load<Texture2D>("Images\\enemy_staying"), x, y, 1, 1,
				TimeSpan.FromMilliseconds(150));
		}

        protected override Animation CreateEnemyMoveAnimation(float x, float y)
        {
            return new GameAnimation(_content.Load<Texture2D>("Images\\enemy_moving"), x, y, 1, 2,
                TimeSpan.FromMilliseconds(150));
        }

        protected override Animation CreateEnemyDieAnimation(float x, float y)
        {
            return new GameAnimation(_content.Load<Texture2D>("Images\\enemy_die"), x, y, 1, 5,
                TimeSpan.FromMilliseconds(150));
        }

        protected override Animation CreateBombAnimation(float x, float y, int num)
        {
            return new GameAnimation(_content.Load<Texture2D>($"Images\\bomb{num}_animation"), x, y, 1, 2, 
                TimeSpan.FromMilliseconds(100));
        }

        protected override Animation CreateExplotionAnimation(float x, float y)
        {
            return new GameAnimation(_content.Load<Texture2D>("Images\\explosion"), x, y, 4, 8,
                TimeSpan.FromMilliseconds(50));
        }

        public GameGraphicsFactory(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content;
            _graphicsDevice = graphicsDevice;
        }
    }
}