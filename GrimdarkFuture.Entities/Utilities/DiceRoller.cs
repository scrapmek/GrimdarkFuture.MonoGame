using System;
using System.Collections.Generic;
using System.Linq;
using GrimdarkFuture.Mechanics.Models;

namespace GrimdarkFuture.Mechanics.Utilities
{
	public static class DiceRoller
	{
		private static readonly Random random = new Random();

		public static int Roll1d6() =>
			random.Next(0, 6) + 1;

		public static IEnumerable<int> Roll2d6() => RollDice(2);

		public static bool RollDefense(int? defense, int ap)
		{
			int roll = Roll1d6();
			return defense != null && roll - ap >= defense;
		}

		public static IEnumerable<int> RollDice(int number) =>
					Enumerable.Range(0, number).Select(_ => Roll1d6()).ToArray();

		public static bool RollQualityCheck(int quality)
		{
			int roll = Roll1d6();
			return roll >= quality;
		}
	}
}