using GrimdarkFuture.Mechanics.Interfaces;

namespace GrimdarkFuture.Entities.Interfaces
{
	public interface IMeleeWeapon : IWeapon
	{
		int GetAP(IModel shooter, IUnit target);

		int GetAttacks(IModel attacker, IUnit target);

		int GetDamage(IModel shooter, IUnit target);
	}
}