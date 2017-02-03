using BomberLibrary.Controls;
using BomberLibrary;
using BomberLibrary.Characters;
using BomberMonoLibrary.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BomberIOS.Controls
{
	public class TouchButtonFactory : ButtonFactory
	{
		private readonly ContentManager _content;

		public TouchButtonFactory(ContentManager content)
		{
			_content = content;
		}

		public override Button CreateMenuButton(float x, float y, string text, ButtonDelegate action)
		{
			return new TouchTextButton(x, y, action, text);
		}

		public TouchSpriteButton CreateMoveLeftButton(float x, float y)
		{
			return new TouchSpriteButton(x, y, () =>
			{
				if (!Game.Player.Killed)
					Game.Player.MoveLeft();
			}, new GameSprite(x, y, _content.Load<Texture2D>("Images\\move_left")), () =>
			{
				if (!Game.Player.Killed)
					Game.Player.StopMoving();
			});
		}

		public TouchSpriteButton CreateMoveRightButton(float x, float y)
		{
			return new TouchSpriteButton(x, y, () =>
			{
				if (!Game.Player.Killed)
					Game.Player.MoveRight();
			}, new GameSprite(x, y, _content.Load<Texture2D>("Images\\move_right")), () =>
			{
				if (!Game.Player.Killed)
					Game.Player.StopMoving();
			});
		}

		public TouchSpriteButton CreateMoveUpButton(float x, float y)
		{
			return new TouchSpriteButton(x, y, () =>
			{
				if (!Game.Player.Killed)
					Game.Player.MoveUp();
			}, new GameSprite(x, y, _content.Load<Texture2D>("Images\\move_up")), () =>
			{
				if (!Game.Player.Killed)
					Game.Player.StopMoving();
			});
		}

		public TouchSpriteButton CreateMoveDownButton(float x, float y)
		{
			return new TouchSpriteButton(x, y, () =>
			{
				if (!Game.Player.Killed)
					Game.Player.MoveDown();
			}, new GameSprite(x, y, _content.Load<Texture2D>("Images\\move_down")), () =>
			{
				if (!Game.Player.Killed)
					Game.Player.StopMoving();
			});
		}

		public TouchSpriteButton CreatePlantBombButton(float x, float y)
		{
			return new TouchSpriteButton(x, y, () =>
			{
				if (!Game.Player.Killed)
					Game.Player.PlantBomb();
			}, new GameSprite(x, y, _content.Load<Texture2D>("Images\\boom")), () =>
			{
				if (!Game.Player.Killed)
					Game.Player.StopMoving();
			});
		}

		public TouchSpriteButton CreatePauseButton(float x, float y)
		{
			return new TouchSpriteButton(x, y, Game.Pause,
										 new GameSprite(x, y, _content.Load<Texture2D>("Images\\pause")));
		}
	}
}