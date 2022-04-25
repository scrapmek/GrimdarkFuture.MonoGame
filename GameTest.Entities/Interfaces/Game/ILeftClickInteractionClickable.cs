using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameTest.Entities.Interfaces
{
	public interface ILeftClickInteraction : IMouseInteraction
	{
		void OnLeftMouseClick(Point point, IEnumerable<IMouseInteraction> mouseOver);
	}

	public interface IRightClickInteraction : IMouseInteraction
	{
		void OnRightMouseClick(Point point, IEnumerable<IMouseInteraction> mouseOver);
	}
}