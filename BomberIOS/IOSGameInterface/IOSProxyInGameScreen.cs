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
				buttonFactory.CreateMoveLeftButton(0.01f * GameData.WindowWidth, 0.8f * GameData.WindowHeight),
				buttonFactory.CreateMoveRightButton(0.16f * GameData.WindowWidth, 0.8f * GameData.WindowHeight),
				buttonFactory.CreateMoveUpButton(0.085f * GameData.WindowWidth, 0.7f * GameData.WindowHeight),
				buttonFactory.CreateMoveDownButton(0.085f * GameData.WindowWidth, 0.9f * GameData.WindowHeight),
				buttonFactory.CreatePlantBombButton(0.85f * GameData.WindowWidth, 0.8f * GameData.WindowHeight),
				buttonFactory.CreatePauseButton(0.9f * GameData.WindowWidth, 0.02f * GameData.WindowHeight)
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
			SubscribeButtons();
		}

		public override void UnLoad()
		{
			base.UnLoad();
			UnSubscribeButtons();
		}

		private void SubscribeButtons()
		{
			for (int i = 0; i < _buttons.Length; ++i)
				_buttons[i].Subscribe();
		}

		void UnSubscribeButtons()
		{
			for (int i = 0; i < _buttons.Length; ++i)
				_buttons[i].UnSubscribe();
		}

		void UnSubscribeMoveButtons()
		{
			for (int i = 0; i < _buttons.Length - 1; ++i)
				_buttons[i].UnSubscribe();
		}
	}
}
