using GrimdarkFuture.Mechanics.Interfaces;
using GrimdarkFuture.Mechanics.Models;

namespace GrimdarkFuture.Entities.Interfaces
{
	public interface IRangedWeapon : IWeapon
	{
		GameMeasure MaxRange { get; }

		int GetAP(IModel shooter, IUnit target, GameMeasure range);

		int GetDamage(IModel shooter, IUnit target, GameMeasure range);

		int GetShots(IModel shooter, IUnit target, GameMeasure range);
	}
}