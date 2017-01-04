using System;
using System.Collections.Generic;
using BomberLib;
using BomberLib.GameInterface;
using BomberWindowsGame.Graphics;
using BomberWindowsGame.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game = BomberLib.Game;

namespace BomberWindowsGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BomerWindowsGame : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private KeyboardState _oldState;
        private KeyboardState _newState;

        private SpriteFont _font;

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
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("Courier New");
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            UpdateInput();

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

            ToBeDrawn.Sprites = new List<GameSprite>();
            ToBeDrawn.Animations = new List<GameAnimation>();
            ToBeDrawn.Texts = new List<GameDrawableText>();

            switch (GameData.GameStatus)
            {
                case GameStatus.InGame:
                    GameData.CurrentLevel?.Draw();
                    StatusLine.Draw(5, 5);
                    break;
                case GameStatus.PlayerDead:
                    GameData.CurrentLevel?.Draw();
                    StatusLine.Draw(5, 5);
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
                    GameWinScreen.Draw();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            DrawAll();
            base.Draw(gameTime);
        }

        private void DrawAll()
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            foreach (var sprite in ToBeDrawn.Sprites.ToArray())
            {
                if (sprite == null) continue;
                if (sprite is GameRectangleSprite)
                    _spriteBatch.Draw(sprite.myTexture, (sprite as GameRectangleSprite).Rectangle, Color.White);
                else
                    _spriteBatch.Draw(sprite.myTexture, sprite.SpritePosition, Color.White);
            }
            foreach (var animation in ToBeDrawn.Animations)
            {
                if (animation == null) continue;
                _spriteBatch.Draw(animation.Texture, animation.DestinationRectangle, animation.SourceRectangle, Color.White);
            }
            foreach (var text in ToBeDrawn.Texts)
            {
                if (text == null) continue;
                _spriteBatch.DrawString(_font, text.Text, text.TextPosition, Color.Black, 0, _font.MeasureString(text.Text)/2, 1.0f, SpriteEffects.None, 0.5f);
            }
            _spriteBatch.End();
        }
    }
}
