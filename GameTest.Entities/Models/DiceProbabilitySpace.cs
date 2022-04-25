using System;
using System.Collections.Generic;
using System.Linq;

namespace ShootingCalculator
{
	public static class DiceProbabilitySpace
    {
        public static IEnumerable<KeyValuePair<bool, string>> RollToHit(int target) => RollDice(roll => roll >= target, success => success ? "Hit" : "Missed");

        public static IEnumerable<KeyValuePair<bool, string>> RollSave(int? armourSave, int? weaponAP, int? invulnerableSave)
        {
            var useArmourSave = !invulnerableSave.HasValue || (armourSave ?? 7) - (weaponAP ?? 0) <= invulnerableSave.Value;

            if (useArmourSave)
                return RollDice(roll => roll >= (armourSave ?? 7) - (weaponAP ?? 0), success => success ? "Armour save" : "Armour save failed");
            else
                return RollDice(roll => roll >= invulnerableSave.Value, success => success ? "Invulnerable save!" : "Invulnerable save failed");
        }

        public static IEnumerable<KeyValuePair<bool, string>> RollToWound(int strength, int toughness)
        {
            int targetRoll = 1;
            if (strength <= toughness / 2)
                targetRoll = 6;
            else if (strength >= 2 * toughness)
                targetRoll = 2;
            else if (strength < toughness)
                targetRoll = 5;
            else if (strength > toughness)
                targetRoll = 3;
            else if (strength == toughness)
                targetRoll = 4;
            else
                throw new Exception("Somenting went wrong calculating the target value.");

            return RollDice(roll => roll >= targetRoll, success => success ? "Wound" : "Failed to wound");
        }

        public static IEnumerable<KeyValuePair<bool, string>> RollDice(Func<int, bool> successFunction, Func<bool, string> messageFunction, int diceSize = 6) => Enumerable.Range(1, diceSize).Select(x => new KeyValuePair<bool, string>(successFunction(x), messageFunction(successFunction(x))));
    }
}