using System;
using System.Linq;
using GameTest.Mechanics.Interfaces;

namespace GameTest.Mechanics.Utilities

{
	public class ShootingAttackCalculator
	{
		public static void DefaultShootingAttack(IModel shooter, IUnit targetUnit, IRangedWeapon weapon, int range)
		{
			if (shooter == null)
				throw new ArgumentNullException(nameof(shooter));

			if (weapon == null)
				throw new ArgumentNullException(nameof(weapon));

			if (targetUnit == null)
				throw new ArgumentNullException(nameof(targetUnit));

			Enumerable.Range(0, weapon.GetShots(shooter, targetUnit, range))
				.Select(x => DiceRoller.RollQualityCheck(shooter.Quality))
				.Where(x => x)
				.Select(x => !DiceRoller.RollDefense(targetUnit.Quality, weapon.GetAP(shooter, targetUnit, range)))
				.Where(x => x)
				.Select(x => weapon.GetDamage(shooter,targetUnit,range));
		}
	}
}