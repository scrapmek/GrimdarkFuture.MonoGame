using System.Collections.Generic;

namespace GrimdarkFuture.Entities.Models
{
	public abstract class AttackResult
	{
		public IEnumerable<int> Damage { get; set; }
		public int Hits { get; set; }
		public string ResultDescription { get; set; }
		public int Saves { get; set; }
		public int Wounds { get; set; }
	}
}