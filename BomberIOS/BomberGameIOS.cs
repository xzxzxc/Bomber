using BomberMonoLibrary;
using BomberLibrary;
using Microsoft.Xna.Framework.Input.Touch;

namespace BomberIOS
{
	public class BomberGameIOS : BomerGame
	{
		private TouchCollection touchCollection;

		protected override void Initialize()
		{
			base.Initialize();
			Game.Load("","","","","");
		}

		protected override void InitializeInput()
		{
			//throw new NotImplementedException();
		}

		protected override void UpdateInput()
		{
			touchCollection = TouchPanel.GetState();

			if (touchCollection.Count == 0)
				return;

			switch (GameData.GameStatus)
			{
				case GameStatus.StartScreen:
					if (touchCollection.Count > 0)
						Game.StartNew();
					break;
				case GameStatus.InGame:
					if (touchCollection[0].Position.X < GameData.Player.X)
						GameData.Player.MoveLeft();
					if (touchCollection[0].Position.X > GameData.Player.X)
						GameData.Player.MoveRight();
					
					if (touchCollection[0].Position.Y < GameData.Player.Y)
						GameData.Player.MoveUp();
					if (touchCollection[0].Position.Y > GameData.Player.Y)
						GameData.Player.MoveDown();

					if (touchCollection.Count == 2)
						GameData.Player.PlantBomb();
					break;
				case GameStatus.PlayerDeadScreen:
					if (touchCollection.Count > 0)
						Game.RestartLevel();
					break;
			}
		}
	}
}
