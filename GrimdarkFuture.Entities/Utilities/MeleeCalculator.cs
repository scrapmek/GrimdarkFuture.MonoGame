using System;
using System.Linq;
using GrimdarkFuture.Entities.Interfaces;
using GrimdarkFuture.Mechanics.Interfaces;

namespace GrimdarkFuture.Mechanics.Utilities
{
	public static class MeleeCalculator
	{
		public static void DefaultRangedAttack(IModel attacker, IUnit targetUnit, IMeleeWeapon weapon)
		{
			if (attacker == null)
				throw new ArgumentNullException(nameof(attacker));

			if (weapon == null)
				throw new ArgumentNullException(nameof(weapon));

			if (targetUnit == null)
				throw new ArgumentNullException(nameof(targetUnit));

			Enumerable.Range(0, weapon.GetAttacks(attacker, targetUnit))
				.Select(x => DiceRoller.RollQualityCheck(attacker.Quality))
				.Where(x => x)
				.Select(x => !DiceRoller.RollDefense(targetUnit.Quality, weapon.GetAP(attacker, targetUnit)))
				.Where(x => x)
				.Select(x => weapon.GetDamage(attacker, targetUnit));
		}
	}
}