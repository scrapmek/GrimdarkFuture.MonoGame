using System;
using System.Collections.Generic;

namespace GrimdarkFuture.Mechanics.Interfaces
{

	public interface IUnit : IKeywords, IStatBlock
	{
		IEnumerable<IModel> LostModels { get; }
		IEnumerable<IModel> Models { get; }
		int ModelsLostThisTurn { get; }
		IEnumerable<string> FactionKeywords { get; }

		void RoutUnit();

		void ApplyDamage(int damage);

		bool CheckCoherency();
		int CoherencyDistance { get; }

		void PinUnit();
	}
}