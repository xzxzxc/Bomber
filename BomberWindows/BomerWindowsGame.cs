using System;
using BomberLibrary;
using BomberLibrary.GameInterface;
using BomberWindows.Graphics;
using BomberWindows.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game = BomberLibrary.Game;

namespace BomberWindows
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BomerWindowsGame : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphics;
        public static SpriteBatch SpriteBatch;

        private KeyboardState _oldState;
        private KeyboardState _newState;

        public static SpriteFont Font;

        public BomerWindowsGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            //GameData.SerializableGameData = new SerializableGameData();
            GameData.CellHeight = GameData.CellWidth = 50;
            GameData.GraphicsFactory = new GameGraphicsFactory(Content, GraphicsDevice);
            GameData.SoundFactory = new GameSoundFactory(Content);
            _oldState = Keyboard.GetState();
            GameData.WindowWidth = _graphics.PreferredBackBufferWidth;
            GameData.WindowHeight = _graphics.PreferredBackBufferHeight;
            Game.Load("Press ENTER to Start\n\nPress L to load last game\n\nPress M to Mute Music",
                "Press S to save\n\nPress L to load\n\nPress M to Mute Music\n\nPress Esc to Continue",
                "Press ENTER to start new game", "Press ENTER to start new game", "Press ENTER to restart level");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Font = Content.Load<SpriteFont>("Courier New");
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            UpdateInput();
            Game.Update();
            base.Update(gameTime);
        }

        delegate void KeyDeleate();
        private void KeyMagic(Keys key, KeyDeleate downDeleate = null, KeyDeleate pressDeleate = null, 
            KeyDeleate upDeleate = null)
        {
            // Is the SPACE key down?
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

        private void UpdateInput()
        {
            _newState = Keyboard.GetState();

            switch (GameData.GameStatus)
            {
                case GameStatus.InGame:
                    UpdateMoveControl();
                    UpdateBombPlantingControl();
                    UpdateGoToMenu();
                    break;
                case GameStatus.Pause:
                    UpdateContinueInput();
                    UpdateLoadGameInput();
                    UpdateSaveGameInput();
                    UpdateMuteSoundInput();
                    break;
                case GameStatus.StartScreen:
                    UpdateStartNewGameInput();
                    UpdateLoadGameInput();
                    UpdateMuteSoundInput();
                    break;
                case GameStatus.GameOverScreen:
                    UpdateStartNewGameInput();
                    break;
                case GameStatus.PlayerDeadScreen:
                    UpdateRestartLevelInput();
                    break;
                case GameStatus.GameWin:
                    UpdateStartNewGameInput();
                    break;
                case GameStatus.PlayerDead:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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
            KeyMagic(Keys.Space, pressDeleate: () => { GameData.Player.PlantBomb(); });
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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            switch (GameData.GameStatus)
            {
                case GameStatus.InGame:
                    GameData.CurrentLevel?.Draw();
                    StatusLine.Draw();
                    break;
                case GameStatus.PlayerDead:
                    GameData.CurrentLevel?.Draw();
                    StatusLine.Draw();
                    break;
                case GameStatus.Pause:
                    PauseScreen.Draw();
                    break;
                case GameStatus.StartScreen:
                    StartScreen.Draw();
                    break;
                case GameStatus.GameOverScreen:
                    GameOverScreen.Draw();
                    break;
                case GameStatus.PlayerDeadScreen:
                    DieScreen.Draw();
                    break;
                case GameStatus.GameWin:
                    GameWonScreen.Draw();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
