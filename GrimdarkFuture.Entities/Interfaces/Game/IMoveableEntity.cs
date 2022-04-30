using Microsoft.Xna.Framework;

namespace GrimdarkFuture.Entities.Interfaces.Game
{
	public interface IMoveableEntity : IPositionableEntity
	{
		public Vector2 Destination { get; set; }
	}
}