using System;
using System.Collections.Generic;
using System.Linq;
using GameTest.Mechanics.Models;

namespace GameTest.Mechanics.Utilities
{
	public static class DiceRoller
	{
		private static Random random = new Random();

		public static int Roll1d6() =>
			random.Next(0, 6) + 1;

		public static IEnumerable<int> Roll2d6() => RollDice(2);

		public static IEnumerable<int> RollDice(int number) =>
			Enumerable.Range(0, number).Select(x => Roll1d6()).ToArray();


		public static bool RollDefense(int? defense, int ap) => defense != null && Roll1d6() - ap >= defense;

		public static bool RollQualityCheck(int quality) => Roll1d6() >= quality;
	}


}
