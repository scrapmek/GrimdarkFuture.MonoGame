namespace GrimdarkFuture.Mechanics.Interfaces
{
	public interface IMeleeWeapon : IWeapon
	{
		int GetAttacks(IModel attacker, IUnit target);
		int GetDamage(IModel shooter, IUnit target);
		int GetAP(IModel shooter, IUnit target);

	}
}