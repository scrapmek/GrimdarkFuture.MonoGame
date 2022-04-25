using System.Linq;
using GameTest.Mechanics.Interfaces;

namespace GameTest.Mechanics.Utilities
{
	public static class MoraleCalculator
	{
		public static void PerformMoraleTest(IUnit unit)
		{
			var moraleSuccess = DiceRoller.RollQualityCheck(unit.Quality);

			if (moraleSuccess)
				return;
			else if (unit.Models.Count() < unit.LostModels.Count())
				unit.RoutUnit();
			else
				unit.PinUnit();
		}
	}
}