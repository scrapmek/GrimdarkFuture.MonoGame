using GrimdarkFuture.Mechanics.Models;

namespace GrimdarkFuture.Mechanics.Interfaces
{
	public interface IModel : IStatBlock
	{
		IUnit Target { get; set; }

		Faction Faction { get; }

		void Flee();

		void ApplyDamage(int damage);

		void MeleeAttack(IMeleeWeapon meleeWeapon, IUnit target);

		void FireWeapon(IRangedWeapon rangedWeapon, IUnit target, int range);
	}


}