using System.Collections.Generic;

namespace ShootingCalculator
{
	public abstract class AttackResult
	{
		public string ResultDescription { get; set; }
		public int Hits { get; set; }
		public int Wounds { get; set; }
		public int Saves { get; set; }
		public IEnumerable<int> Damage { get; set; }
	}
}