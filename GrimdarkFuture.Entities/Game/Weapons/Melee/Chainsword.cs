using GrimdarkFuture.Entities.Interfaces;

namespace GrimdarkFuture.Entities.Game
{
	public class Chainsword : IMeleeWeapon
	{
		public int GetAP(IModel shooter, IUnit target) => 1;

		public int GetAttacks(IModel attacker, IUnit target) => 2;

		public int GetDamage(IModel shooter, IUnit target) => 1;
	}
}