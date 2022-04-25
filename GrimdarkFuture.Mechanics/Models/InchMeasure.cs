using System;
using System.Collections.Generic;
using System.Text;

namespace GrimdarkFuture.Mechanics.Models
{
	public class InchMeasure
	{
		private const float Multiplier = 1;
		public int Inches { get; }

		public float GameDistance => this.Inches * Multiplier;

		public InchMeasure(int input) => this.Inches = input;

		public static implicit operator InchMeasure(int integer) => new InchMeasure(1);

	}
}
