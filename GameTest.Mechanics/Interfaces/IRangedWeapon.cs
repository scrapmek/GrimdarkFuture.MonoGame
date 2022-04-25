namespace GameTest.Mechanics.Interfaces
{
	public interface IRangedWeapon : IWeapon
	{
		int MaxRange { get; }

		int GetShots(IModel shooter, IUnit target, int range);
		int GetAttacks(IModel attacker, IUnit target, int range);
		int GetDamage(IModel shooter, IUnit target, int range);
		int GetAP(IModel shooter, IUnit target, int range);
	}
}