using GrimdarkFuture.Entities.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrimdarkFuture.Entities.Interfaces
{
	public interface IRenderableEntity : IPositionableEntity
	{
		Texture2D Texture { get; }
		Vector2 TexturePositionOffset { get; }
		string TextureName { get; }
	}
}