using System;
using System.Linq;
using GrimdarkFuture.Entities.Interfaces;
using GrimdarkFuture.Mechanics.Interfaces;
using GrimdarkFuture.Mechanics.Models;

namespace GrimdarkFuture.Mechanics.Utilities

{
	public static class ShootingAttackCalculator
	{
		public static void DefaultShootingAttack(IModel shooter, IUnit targetUnit, IRangedWeapon weapon, GameMeasure range)
		{
			if (shooter == null)
				throw new ArgumentNullException(nameof(shooter));

			if (weapon == null)
				throw new ArgumentNullException(nameof(weapon));

			if (targetUnit == null)
				throw new ArgumentNullException(nameof(targetUnit));

			int count = weapon.GetShots(shooter, targetUnit, range);
			Console.WriteLine($"Firing {count} shots with {weapon}");
			var hits = Enumerable.Range(0, count)
							.Select(_ => DiceRoller.RollQualityCheck(shooter.Quality))
							.ToList()
							.Count(x => x);

			if (hits > 0)
			{
				Console.WriteLine($"{hits} shots hit with {weapon}");

				var wounds = Enumerable.Range(0, hits)
								.Select(_ => !DiceRoller.RollDefense(targetUnit.Quality, weapon.GetAP(shooter, targetUnit, range)))
								.ToList()
								.Count(x => x);

				if (wounds > 0)
				{
					Console.WriteLine($"{wounds} shots wounded with {weapon}");
					var damage = Enumerable.Range(0, wounds)
						.Select(_ => weapon.GetDamage(shooter, targetUnit, range))
						.ToList();

					Console.WriteLine($"[{string.Join(", ", damage)}] damage dealt with {weapon}");
				}
			}
		}
	}
}