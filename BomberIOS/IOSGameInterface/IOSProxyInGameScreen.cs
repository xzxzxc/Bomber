using BomberLibrary.GameInterface;
using BomberLibrary.Controls;
using BomberIOS.Controls;
using BomberLibrary;

namespace BomberIOS
{
	public class IOSProxyInGameScreen:ProxyInGameScreen
	{
		private Button[] _buttons;

		public IOSProxyInGameScreen()
		{
			var buttonFactory = GameData.ButtonFactory as TouchButtonFactory;
			_buttons = new Button[]
			{
				buttonFactory.CreateMoveLeftButton(0.1f * GameData.WindowWidth, 0.7f * GameData.WindowHeight),
				buttonFactory.CreateMoveRightButton(0.3f * GameData.WindowWidth, 0.7f * GameData.WindowHeight),
				buttonFactory.CreateMoveUpButton(0.2f * GameData.WindowWidth, 0.6f * GameData.WindowHeight),
				buttonFactory.CreateMoveDownButton(0.2f * GameData.WindowWidth, 0.8f * GameData.WindowHeight),
				buttonFactory.CreatePlantBombButton(0.8f * GameData.WindowWidth, 0.75f * GameData.WindowHeight),
				buttonFactory.CreatePauseButton(0.8f * GameData.WindowWidth, 0.02f * GameData.WindowHeight)
			};
		}

		public override void Draw()
		{
			base.Draw();
			for (int i = 0; i < _buttons.Length; ++i)
				_buttons[i].Draw();
		}

		public override void Load()
		{
			base.Load();
			for (int i = 0; i < _buttons.Length; ++i)
				_buttons[i].Subscribe();
		}

		public override void UnLoad()
		{
			base.Load();
			for (int i = 0; i < _buttons.Length; ++i)
				_buttons[i].UnSubscribe();
		}
	}
}
