﻿using System.Linq;
using GameTest.Entities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameTest.Application
{
	public class Renderer
	{
		private readonly EntityStore entityStore;

		public Renderer(EntityStore entityStore)
		{
			this.entityStore = entityStore;
		}

		public void Render(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			foreach (var item in entityStore.GetEntitiesByType<IRenderableEntity>())
				spriteBatch.Draw(item.Texture, item.Position - item.TexturePositionOffset, Color.White);
			spriteBatch.End();
		}
	}

}