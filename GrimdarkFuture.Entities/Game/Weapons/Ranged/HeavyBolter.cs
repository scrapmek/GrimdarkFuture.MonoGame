using GrimdarkFuture.Entities.Interfaces;
using GrimdarkFuture.Mechanics.Models;

namespace GrimdarkFuture.Entities.Game
{
	public class HeavyBolter : IRangedWeapon
	{
		public GameMeasure MaxRange => GameMeasure.FromInches(36);

		public int GetAP(IModel shooter, IUnit target, GameMeasure range) => 1;

		public int GetDamage(IModel shooter, IUnit target, GameMeasure range) => 2;

		public int GetShots(IModel shooter, IUnit target, GameMeasure range) => 3;
	}
}