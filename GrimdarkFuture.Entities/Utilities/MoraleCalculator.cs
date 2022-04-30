using System.Linq;
using GrimdarkFuture.Entities.Interfaces;
using GrimdarkFuture.Mechanics.Interfaces;

namespace GrimdarkFuture.Mechanics.Utilities
{
	public static class MoraleCalculator
	{
		public static void PerformDefaultMoraleTest(IUnit unit)
		{
			var moraleSuccess = DiceRoller.RollQualityCheck(unit.Quality);

			if (moraleSuccess)
				unit.Pinned = false;
			else if (unit.Models.Count(x => x.Health <= 0) > (unit.Models.Count() / 2))
				foreach (var model in unit.Models)
					model.Flee();
			else
				unit.Pinned = true;
		}
	}
}