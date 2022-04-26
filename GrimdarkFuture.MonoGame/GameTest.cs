using System.Collections.Generic;
using System.Linq;
using GrimdarkFuture.Application;
using GrimdarkFuture.Application.Helpers;
using GrimdarkFuture.Entities;
using GrimdarkFuture.Entities.Interfaces;
using GrimdarkFuture.Mechanics.Models;
using GrimdarkFuture.MonoGame.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrimdarkFuture.MonoGame
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
			this.store.Create(new List<IGameEntity>() { 
				new SwordBrethren(
					new Vector2(
						new InchMeasure(10).GameDistance,
						new InchMeasure(10).GameDistance)
					), 
				new ChaosSpaceMarineHavoc(
					new Vector2(
						new InchMeasure(30).GameDistance,
						new InchMeasure(20).GameDistance)) });
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

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

			this.spriteBatch.Begin();
			foreach (var item in Enumerable.Range(0,100))
			{
				spriteBatch.DrawLine(new Vector2(new InchMeasure(item).GameDistance,0),new Vector2(new InchMeasure(item).GameDistance,int.MaxValue), Color.Gray);
				spriteBatch.DrawLine(new Vector2( 0,new InchMeasure(item).GameDistance), new Vector2(int.MaxValue, new InchMeasure(item).GameDistance), Color.Gray);
			}

			renderer.Render(this.spriteBatch);
			spriteBatch.End();
			base.Draw(gameTime);
        }
    }
}
