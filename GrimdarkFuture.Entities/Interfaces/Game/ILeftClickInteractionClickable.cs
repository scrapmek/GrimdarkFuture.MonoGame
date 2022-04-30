using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrimdarkFuture.Entities.Interfaces.Game
{
	public interface IGlobalLeftClickInteraction : IMouseInteraction
	{
		void OnGlobalLeftMouseClick(Point point, IEnumerable<IMouseInteraction> mouseOver);
	}

	public interface IGlobalRightClickInteraction : IMouseInteraction
	{
		void OnGlobalRightMouseClick(Point point, IEnumerable<IMouseInteraction> mouseOver);
	}
}