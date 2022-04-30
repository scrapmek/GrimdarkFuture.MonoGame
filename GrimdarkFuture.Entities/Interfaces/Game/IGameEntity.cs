using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GrimdarkFuture.Entities.Interfaces.Game
{
	public interface IGameEntity
	{
		GameTime LastUpdated { get; }

		Vector2 Position { get; }

		void Draw(SpriteBatch spriteBatch);

		void LoadAssets(ContentManager content);

		void UnloadAssets();

		/// <summary>
		/// Responsible for performing all update logic (Moving, Animation state procession)
		/// </summary>
		/// <param name="time"></param>
		void Update(GameTime time);
	}
}