
using Microsoft.Xna.Framework;

namespace GameTest.Entities.Interfaces
{
	public interface IMoveableEntity : IPositionableEntity
	{
		public int AnimatedMoveSpeed { get; }
		public Vector2 Destination { get; set; }
	}
}