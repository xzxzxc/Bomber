using BomberCrossPlatform.Controls;
using BomberLibrary;
using BomberLibrary.GameInterface;
using BomberMonoLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Game = BomberLibrary.Game;

namespace BomberCrossPlatform
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class BomerGameCrossPlatform : BomberMonoLibrary.MonoGame
	{
		private KeyboardState _oldState;
		private KeyboardState _newState;

		protected override void Initialize()
		{
			base.Initialize();
            GameData.ButtonFactory = new DesktopButtonFactory();
			IsMouseVisible = true;
            Game.Load();
		}

		protected override void InitializeInput()
		{
			_oldState = Keyboard.GetState();
		}

		delegate void KeyDeleate();
		private void KeyMagic(Keys key, KeyDeleate downDeleate = null, KeyDeleate pressDeleate = null,
			KeyDeleate upDeleate = null)
		{
			// Is key down?
			if (_newState.IsKeyDown(key))
			{
				downDeleate?.Invoke();
				// If not down last update, key has just been pressed.
				if (!_oldState.IsKeyDown(key))
				{
					pressDeleate?.Invoke();
				}
			}
			else if (_oldState.IsKeyDown(key))
			{
				// Key was down last update, but not down now, so
				// it has just been released.
				upDeleate?.Invoke();
			}
		}

	    protected override void Update(GameTime gameTime)
	    {
	        base.Update(gameTime);
            UpdateKeyInput();
	    }

	    private void UpdateKeyInput()
	    {
	        _newState = Keyboard.GetState();

	        if (GameData.CurrentScreen is StartScreen)
	        {
	            UpdateStartNewGameInput();
	            UpdateLoadGameInput();
	            UpdateMuteSoundInput();
	        }
	        else if (GameData.CurrentScreen is GameOverScreen)
	            UpdateStartNewGameInput();
	        else if (GameData.CurrentScreen is DieScreen)
	            UpdateRestartLevelInput();
	        else if (GameData.CurrentScreen is GameWonScreen)
	            UpdateStartNewGameInput();
	        else if (GameData.CurrentScreen is PauseScreen)
	        {
	            UpdateSaveGameInput();
	            UpdateContinueInput();
	            UpdateLoadGameInput();
	            UpdateMuteSoundInput();
	        }
	        else if (GameData.CurrentScreen is ProxyInGameScreen)
	        {
	            if (!GameData.Player.Killed)
	            {
	                UpdateGoToMenu();
	                UpdateMoveControl();
	                UpdateBombPlantingControl();
	            }
	        }

	        // Update saved state.
	        _oldState = _newState;
	    }

	    private void UpdateLoadGameInput()
		{
			KeyMagic(Keys.L, pressDeleate: Game.LoadSavedGame);
		}

		private void UpdateSaveGameInput()
		{
			KeyMagic(Keys.S, pressDeleate: Game.SaveGame);
		}

		private void UpdateContinueInput()
		{
			KeyMagic(Keys.Escape, pressDeleate: Game.Continue);
		}

		private void UpdateRestartLevelInput()
		{
			KeyMagic(Keys.Enter, pressDeleate: Game.RestartLevel);
		}

		private void UpdateMuteSoundInput()
		{
			KeyMagic(Keys.M, pressDeleate: GameData.GameMusic.PauseOrResume);
		}

		private void UpdateGoToMenu()
		{
			KeyMagic(Keys.Escape, pressDeleate: Game.Pause);
		}

		private void UpdateBombPlantingControl()
		{
			KeyMagic(Keys.Space, pressDeleate: GameData.Player.PlantBomb);
		}

		private void UpdateMoveControl()
		{
			KeyMagic(Keys.Up, GameData.Player.MoveUp, upDeleate: GameData.Player.StopMoving);

			KeyMagic(Keys.Down, GameData.Player.MoveDown, upDeleate: GameData.Player.StopMoving);

			KeyMagic(Keys.Left, GameData.Player.MoveLeft, upDeleate: GameData.Player.StopMoving);

			KeyMagic(Keys.Right, GameData.Player.MoveRight, upDeleate: GameData.Player.StopMoving);
		}

		private void UpdateStartNewGameInput()
		{
			KeyMagic(Keys.Enter, pressDeleate: Game.StartNew);
		}

	}
}

