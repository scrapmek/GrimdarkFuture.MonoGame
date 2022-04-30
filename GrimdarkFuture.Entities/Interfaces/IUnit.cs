using System.Collections.Generic;
using GrimdarkFuture.Entities.Game;

namespace GrimdarkFuture.Entities.Interfaces
{
	public interface IUnit : IStatBlock /*, IKeywords,*/
	{
		bool Pinned { get; set; }
		int CoherencyDistance { get; }
		//IEnumerable<string> FactionKeywords { get; }
		IEnumerable<AModel> Models { get; }
		IEnumerable<AModel> DestroyedModels { get; }

		void ApplyDamage(int damage);

		bool InCoherency();

		void PreformMoraleTest();
	}
}