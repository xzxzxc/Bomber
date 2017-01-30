using BomberLibrary.Controls;
using BomberLibrary;
using BomberMonoLibrary.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BomberIOS.Controls
{
    public class TouchButtonFactory:ButtonFactory
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
			return new TouchSpriteButton(x, y, Game.Player.MoveLeft, 
			                             new GameSprite(x, y, _content.Load<Texture2D>("Images\\move_left")));
		}

		public TouchSpriteButton CreateMoveRightButton(float x, float y)
		{
			return new TouchSpriteButton(x, y, Game.Player.MoveRight,
										 new GameSprite(x, y, _content.Load<Texture2D>("Images\\move_right")));
		}

		public TouchSpriteButton CreateMoveUpButton(float x, float y)
		{
			return new TouchSpriteButton(x, y, Game.Player.MoveUp,
										 new GameSprite(x, y, _content.Load<Texture2D>("Images\\move_up")));
		}

		public TouchSpriteButton CreateMoveDownButton(float x, float y)
		{
			return new TouchSpriteButton(x, y, Game.Player.MoveDown,
										 new GameSprite(x, y, _content.Load<Texture2D>("Images\\move_down")));
		}

		public TouchSpriteButton CreatePlantBombButton(float x, float y)
		{
			return new TouchSpriteButton(x, y, Game.Player.PlantBomb,
										 new GameSprite(x, y, _content.Load<Texture2D>("Images\\_boom")));
		}

		public TouchSpriteButton CreatePauseButton(float x, float y)
		{
			return new TouchSpriteButton(x, y, Game.Pause,
										 new GameSprite(x, y, _content.Load<Texture2D>("Images\\pause")));
		}
    }
}