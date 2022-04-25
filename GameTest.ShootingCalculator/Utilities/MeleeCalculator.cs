using System;
using System.Linq;
using ShootingCalculator.Interfaces;

namespace ShootingCalculator
{
	public static class MeleeCalculator
    {
        public static MeleeAttackResult DefaultMeleeAttack(this IModel attacker, IUnit target, IMeleeWeapon weapon = null)
        {
            if (attacker == null)
                throw new ArgumentNullException(nameof(attacker));
            
			if (target == null)
                throw new ArgumentNullException(nameof(target));

            var result = new MeleeAttackResult();

            result.Hits = Enumerable.Range(0, weapon.GetAttacks(attacker, target)).Select(x => DiceRoller(attacker.WeaponSkill.Value)).Where(x => x).Count();

            result.Wounds = Enumerable.Range(0, result.Hits).Select(x => weapon.RollToWound(attacker,target)).Where(x => x).Count();

            result.Saves = Enumerable.Range(0, result.Wounds).Select(x => DiceRoller.RollSave(target.ArmourSave, weapon.GetAP(attacker, target), target.InvulnerableSave)).Where(x => x).Count();

            result.Damage = Enumerable.Range(0, result.Wounds - result.Saves).Select(x => weapon?.GetDamage(attacker, target) ?? 1);

            return result;
        }
    }
}