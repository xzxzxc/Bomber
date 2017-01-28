using BomberMonoLibrary.Graphics;
using BomberMonoLibrary.Sound;
using BomberLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game = BomberLibrary.Game;

namespace BomberMonoLibrary
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
	public abstract class BomerGame : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphics;
        public static SpriteBatch SpriteBatch;

        public static SpriteFont Font;

        public BomerGame()
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
            
            GameData.CellHeight = GameData.CellWidth = 50;
            GameData.GraphicsFactory = new GameGraphicsFactory(Content, GraphicsDevice);
            GameData.SoundFactory = new GameSoundFactory(Content);
			InitializeInput();
            GameData.WindowWidth = _graphics.PreferredBackBufferWidth;
            GameData.WindowHeight = _graphics.PreferredBackBufferHeight;
        }

		protected abstract void InitializeInput();

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
			Game.Update();
			base.Update(gameTime);
		}
        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            GameData.CurrentScreen.Draw();
            
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
