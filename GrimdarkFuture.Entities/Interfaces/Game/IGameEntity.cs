using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GrimdarkFuture.Entities.Interfaces
{
	public interface IGameEntity
	{
		GameTime LastUpdated { get; }

		/// <summary>
		/// Responsible for performing all update logic (Moving, Animation state procession)
		/// </summary>
		/// <param name="time"></param>
		void Update(GameTime time);

		void Draw(SpriteBatch spriteBatch);

		void LoadAssets(ContentManager content);
		void UnloadAssets();

		Vector2 Position { get; }
	}
}

