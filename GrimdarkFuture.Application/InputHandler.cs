using System;
using System.Linq;
using GrimdarkFuture.Entities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrimdarkFuture.Application
{
	public class InputHandler
	{
		private readonly EntityStore entityCollection;

		private MouseState previousMouseState;

		public InputHandler(EntityStore entityCollection) => this.entityCollection = entityCollection;

		public void HandleInput(KeyboardState keyboardState)
		{
			if (keyboardState.GetPressedKeyCount() > 0)
			{
				var direction = new Vector2();

				if (keyboardState.IsKeyDown(Keys.W))
					direction = direction + new Vector2(0, -1);
				if (keyboardState.IsKeyDown(Keys.A))
					direction = direction + new Vector2(-1, 0);
				if (keyboardState.IsKeyDown(Keys.S))
					direction = direction + new Vector2(0, 1);
				if (keyboardState.IsKeyDown(Keys.D))
					direction = direction + new Vector2(1, 0);

				if (direction.Length() > 0)
					foreach (var item in entityCollection.GetEntitiesByType<ISelectable>().Where(x => x.Selected).ToList())
						if (item is IMoveableEntity moveable)
						{
							Console.WriteLine($"Moving {item} => {direction}");
							moveable.Destination = moveable.Position + direction;
						}
			}
		}

		public void HandleInput(MouseState mouseState)
		{
			var mouseOver = entityCollection.GetEntitiesByType<IMouseInteraction>().Where(x => x.IsMouseOver(mouseState.Position));

			if (this.previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
				this.HandleLeftClick(mouseState, mouseOver);

			if (this.previousMouseState.RightButton == ButtonState.Pressed && mouseState.RightButton == ButtonState.Released)
				this.HandleRightClick(mouseState, mouseOver);

			this.previousMouseState = mouseState;
		}

		private void HandleRightClick(MouseState mouseState, System.Collections.Generic.IEnumerable<IMouseInteraction> mouseOver)
		{
			foreach (var item in entityCollection.GetEntitiesByType<IGlobalRightClickInteraction>())
				item.OnRightMouseClick(mouseState.Position, mouseOver);
		}

		public void HandleLeftClick(MouseState mouseState, System.Collections.Generic.IEnumerable<IMouseInteraction> mouseOver)
		{
			foreach (var item in entityCollection.GetEntitiesByType<IGlobalLeftClickInteraction>())
				item.OnLeftMouseClick(mouseState.Position, mouseOver);
		}
	}
}