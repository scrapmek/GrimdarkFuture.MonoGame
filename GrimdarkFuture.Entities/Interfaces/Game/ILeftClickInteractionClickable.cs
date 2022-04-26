using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrimdarkFuture.Entities.Interfaces
{
	public interface IGlobalLeftClickInteraction : IMouseInteraction
	{
		void OnLeftMouseClick(Point point, IEnumerable<IMouseInteraction> mouseOver);
	}

	public interface IGlobalRightClickInteraction : IMouseInteraction
	{
		void OnRightMouseClick(Point point, IEnumerable<IMouseInteraction> mouseOver);
	}
}