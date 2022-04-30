using System.Collections.Generic;
using GrimdarkFuture.Mechanics.Models;

namespace GrimdarkFuture.Entities.Interfaces

{
	public interface IStatBlock
	{
		int? Defense { get; }

		GameMeasure MoveStat { get; }
		int Quality { get; }
	}
}