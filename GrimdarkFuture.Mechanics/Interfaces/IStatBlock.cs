using GrimdarkFuture.Mechanics.Models;

namespace GrimdarkFuture.Mechanics.Interfaces

{
	public interface IStatBlock
	{
		InchMeasure MoveStat { get; }
		int Quality { get; }
		int? Defense { get; }
		int Health { get; }
	}
}