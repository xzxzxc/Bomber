using BomberMonoLibrary;
using BomberLibrary;
using BomberIOS.Controls;
using BomberLibrary.GameInterface;

namespace BomberIOS
{
	public class BomberGameIOS : BomberMonoLibrary.MonoGame
	{
		protected override void Initialize()
		{
			base.Initialize();
			GameData.CellHeight = GameData.CellWidth = 100;
			GameData.ButtonFactory = new TouchButtonFactory(Content);
			Game.Load();
			GameData.Screens.InGameScreen = new IOSProxyInGameScreen() as IScreen;
		}
	}
}
