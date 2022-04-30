using System.Collections.Generic;
using GrimdarkFuture.Mechanics.Interfaces;
using GrimdarkFuture.Mechanics.Models;
using Microsoft.Xna.Framework;

namespace GrimdarkFuture.Entities.Game
{
	public class ChaosSpaceMarineHavoc : AModel
	{
		public override int? Defense => 2;

		public override Faction Faction => Faction.BlackTemplars;

		public override GameMeasure MoveStat => GameMeasure.FromInches(6);

		public override int Quality => 3;

		public override string TextureName => "CSM";

		public override IEnumerable<IWeapon> Weapons => new IWeapon[] { new HeavyBolter() };

		public override GameMeasure BaseSize => GameMeasure.FromMillimeters(32);

		public ChaosSpaceMarineHavoc(Vector2 position) : base(position)
		{
		}
	}
}