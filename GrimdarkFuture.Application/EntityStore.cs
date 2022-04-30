using System.Collections.Generic;
using System.Linq;
using GrimdarkFuture.Entities.Interfaces.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GrimdarkFuture.Application
{
	public class EntityStore
	{
		public List<IGameEntity> GameEntities { get; private set; }

		public void Create(IEnumerable<IGameEntity> gameEntities) => this.GameEntities = gameEntities.ToList();

		public IList<T> GetEntitiesByType<T>() where T : class => GameEntities.Where(x => x is T).Select(x => x as T).ToList();

		public void LoadAssets(ContentManager content) => GameEntities.ForEach(x => x.LoadAssets(content));

		public void Update(GameTime time) => GameEntities.ForEach(x => x.Update(time));
	}
}