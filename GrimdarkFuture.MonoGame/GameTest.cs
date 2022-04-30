using System.Collections.Generic;
using System.Linq;
using GrimdarkFuture.Application;
using GrimdarkFuture.Application.Helpers;
using GrimdarkFuture.Entities.Game;
using GrimdarkFuture.Entities.Interfaces.Game;
using GrimdarkFuture.Mechanics.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrimdarkFuture.MonoGame
{
	public class GameTest : Game
	{
		private readonly GraphicsDeviceManager graphics;
		private readonly InputHandler inputHandler;
		private readonly Renderer renderer;
		private readonly EntityStore store;
		private SpriteBatch spriteBatch;

		/// <summary>
		/// Required by XNA framework...
		/// </summary>
		public GameTest()
		{
			this.graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		public GameTest(Renderer renderer, EntityStore store, InputHandler inputHandler) : this()
		{
			this.renderer = renderer;
			this.store = store;
			this.inputHandler = inputHandler;
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			this.spriteBatch.Begin();
			foreach (var item in Enumerable.Range(0, 100))
			{
				spriteBatch.DrawLine(new Vector2(GameMeasure.FromInches(item).GameDistance, 0), new Vector2(GameMeasure.FromInches(item).GameDistance, int.MaxValue), Color.Gray);
				spriteBatch.DrawLine(new Vector2(0, GameMeasure.FromInches(item).GameDistance), new Vector2(int.MaxValue, GameMeasure.FromInches(item).GameDistance), Color.Gray);
			}

			renderer.Render(this.spriteBatch);
			spriteBatch.End();
			base.Draw(gameTime);
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			this.Window.AllowUserResizing = true;
			this.store.Create(
				new List<IGameEntity>() {
					new Unit<SwordBrethren>(new SwordBrethren[]{
						new SwordBrethren(
							new Vector2(
								GameMeasure.FromInches(10).GameDistance,
								GameMeasure.FromInches(10).GameDistance)
							),new SwordBrethren(
							new Vector2(
								GameMeasure.FromInches(10).GameDistance,
								GameMeasure.FromInches(10).GameDistance)
							),new SwordBrethren(
							new Vector2(
								GameMeasure.FromInches(10).GameDistance,
								GameMeasure.FromInches(10).GameDistance)
							),new SwordBrethren(
							new Vector2(
								GameMeasure.FromInches(10).GameDistance,
								GameMeasure.FromInches(10).GameDistance)
							),new SwordBrethren(
							new Vector2(
								GameMeasure.FromInches(10).GameDistance,
								GameMeasure.FromInches(10).GameDistance)
							),new SwordBrethren(
							new Vector2(
								GameMeasure.FromInches(10).GameDistance,
								GameMeasure.FromInches(10).GameDistance)
							),new SwordBrethren(
							new Vector2(
								GameMeasure.FromInches(10).GameDistance,
								GameMeasure.FromInches(10).GameDistance)
							) })
					,
					new Unit<ChaosSpaceMarineHavoc>(
						new ChaosSpaceMarineHavoc(
							new Vector2(
								GameMeasure.FromInches(30).GameDistance,
								GameMeasure.FromInches(20).GameDistance))
					) 
				});
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
	}
}