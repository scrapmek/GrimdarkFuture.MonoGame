using GrimdarkFuture.Mechanics.Models;

namespace GrimdarkFuture.Mechanics.Interfaces
{
	public interface IRangedWeapon : IWeapon
	{
		InchMeasure MaxRange { get; }

		int GetShots(IModel shooter, IUnit target, int range);
		int GetDamage(IModel shooter, IUnit target, int range);
		int GetAP(IModel shooter, IUnit target, int range);
	}
}