using System.Collections.Generic;
using GameTest.Application;
using GameTest.Entities;
using GameTest.Entities.Interfaces;
using GameTest.MonoGame.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTest.MonoGame
{
    public class GameTest : Game
    {
        private readonly GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private readonly Renderer renderer;
		private readonly EntityStore store;
		private readonly InputHandler inputHandler;

		/// <summary>
		/// Required by XNA framework...
		/// </summary>
		public GameTest()
		{
			this.graphics =  new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		public GameTest(Renderer renderer, EntityStore store, InputHandler inputHandler) : this()
		{
			this.renderer = renderer;
			this.store = store;
			this.inputHandler = inputHandler;
		}

		protected override void Initialize()
        {
            // TODO: Add your initialization logic here
			this.Window.AllowUserResizing = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

			this.store.Create(new List<IGameEntity>() { new SpaceMarine(), new SpaceMarine() });
			this.store.LoadAssets(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			var keyboardState = Keyboard.GetState();
			this.inputHandler.HandleInput(keyboardState);
			this.inputHandler.HandleInput(Mouse.GetState());

			this.store.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			renderer.Render(this.spriteBatch);

            base.Draw(gameTime);
        }
    }
}
