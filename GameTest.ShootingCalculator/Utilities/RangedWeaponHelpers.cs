using System;

namespace ShootingCalculator
{
	public static class RangedWeaponHelpers
    {
        public static int DefaultCalculateShots(RangedWeaponType rangedWeaponType, int range, int weaponRange, int shots)
        {
            switch (rangedWeaponType)
            {
                case RangedWeaponType.RapidFire:
                    if (range <= 1 || range > weaponRange)
                        throw new WeaponOutOfRangeException();
                    return range < ((decimal)weaponRange / 2) ? shots * 2 : shots;

                case RangedWeaponType.Pistol:
                    if (range > weaponRange)
                        throw new WeaponOutOfRangeException();
                    return shots;

                case RangedWeaponType.Heavy:
                    if (range <= 1 || range > weaponRange)
                        throw new WeaponOutOfRangeException();
                    return shots;

                case RangedWeaponType.Assault:
                    if (range <= 1 || range > weaponRange)
                        throw new WeaponOutOfRangeException();
                    return shots;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}