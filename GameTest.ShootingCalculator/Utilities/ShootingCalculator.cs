using System;
using System.Linq;
using ShootingCalculator.Interfaces;

namespace ShootingCalculator
{
	public static class ShootingCalculator
	{
		public static ShootingAttackResult DefaultShootingAttack(IModel shooter, IUnit targetUnit, IRangedWeapon weapon, int range)
		{
			if (shooter == null)
				throw new ArgumentNullException(nameof(shooter));
			if (shooter.BallisticSkill == null)
				throw new ArgumentNullException($"This attacker has no {nameof(IModel.BallisticSkill)} and cannot attack.");

			if (weapon == null)
				throw new ArgumentNullException(nameof(weapon));

			if (targetUnit == null)
				throw new ArgumentNullException(nameof(targetUnit));

			var result = new ShootingAttackResult();

			result.Hits = Enumerable.Range(0, weapon.GetShots(shooter, targetUnit, range)).Select(x => weapon.RollToHit(shooter, targetUnit, range)).Where(x => x).Count();

			result.Wounds = Enumerable.Range(0, result.Hits).Select(x => weapon.RollToWound(shooter, targetUnit, range)).Where(x => x).Count();

			result.Saves = Enumerable.Range(0, result.Wounds).Select(x => DiceRoller.RollSave(targetUnit.ArmourSave, weapon.GetAP(shooter,targetUnit,range), targetUnit.InvulnerableSave)).Where(x => x).Count();

			result.Damage = Enumerable.Range(0, result.Wounds - result.Saves).Select(x => weapon.GetDamage(shooter, targetUnit, range));

			return result;
		}
	}
}