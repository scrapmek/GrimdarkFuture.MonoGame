using System.Collections.Generic;
using GrimdarkFuture.Mechanics.Interfaces;
using GrimdarkFuture.Mechanics.Models;
using Microsoft.Xna.Framework;

namespace GrimdarkFuture.Entities.Interfaces
{
	public interface IModel : IStatBlock
	{
		Faction Faction { get; }
		IUnit Target { get; set; }
		int Health { get; }
		Vector2 Position { get; }

		GameMeasure BaseSize { get; }

		IEnumerable<IWeapon> Weapons { get; }

		void ApplyDamage(int damage);

		void FireWeapon(IRangedWeapon rangedWeapon, IUnit target, GameMeasure range);

		void Flee();

		void MeleeAttack(IMeleeWeapon meleeWeapon, IUnit target);
	}
}