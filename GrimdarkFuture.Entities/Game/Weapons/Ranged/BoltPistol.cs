using GrimdarkFuture.Entities.Interfaces;
using GrimdarkFuture.Mechanics.Models;

namespace GrimdarkFuture.Entities.Game
{
	public class BoltPistol : IRangedWeapon
	{
		public GameMeasure MaxRange => GameMeasure.FromInches(18);

		public int GetAP(IModel shooter, IUnit target, GameMeasure range) => 1;

		public int GetDamage(IModel shooter, IUnit target, GameMeasure range) => 1;

		public int GetShots(IModel shooter, IUnit target, GameMeasure range) => 1;
	}
}