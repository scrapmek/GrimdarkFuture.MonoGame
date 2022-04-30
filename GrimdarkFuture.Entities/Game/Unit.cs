using System;
using System.Collections.Generic;
using System.Linq;
using GrimdarkFuture.Entities.Interfaces;
using GrimdarkFuture.Entities.Interfaces.Game;
using GrimdarkFuture.Mechanics.Models;
using GrimdarkFuture.Mechanics.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GrimdarkFuture.Entities.Game
{
	public class Unit<T> : IUnit, ISelectable, IGameEntity, IGlobalLeftClickInteraction, IGlobalRightClickInteraction
		where T : AModel
	{
		private readonly List<T> models;

		public Unit(IEnumerable<T> models) =>
			this.models = models.ToList();

		public Unit(T model) : this(new T[] { model }) { }

		public int CoherencyDistance => 4;

		public IEnumerable<AModel> Models => this.models.Where(x => x.Health > 0);
		public IEnumerable<AModel> DestroyedModels => this.models.Where(x => x.Health <= 0);

		public int? Defense => Models.Min(x => x.Defense);
		public GameMeasure MoveStat => Models.Min(x => x.MoveStat);
		public int Quality => Models.Min(x => x.Quality);

		public bool Pinned { get; set; }
		public bool Selected { get; private set; }
		public GameTime LastUpdated { get; }
		public Vector2 Position { get; }

		public void ApplyDamage(int damage) => Models.FirstOrDefault(x => x.Health > 0)?.ApplyDamage(damage);

		public bool InCoherency()
		{
			foreach (var model in models)
			{
				var otherModels = models.Where(x => x != model).Where(x => x.Health > 0);

				// Can't be out of coherency if there aren't any other models.
				if (otherModels.Any())
				{
					var inCoherency = otherModels.Select(x => x.GetRange(model).Inches <= this.CoherencyDistance).Where(x => x).Count();

					if (inCoherency < Math.Min(2, otherModels.Count()))
						return false;
				}
			}

			return true;
		}
		public void Draw(SpriteBatch spriteBatch) =>
			models.ForEach(x => x.Draw(spriteBatch));

		public void PreformMoraleTest() => MoraleCalculator.PerformDefaultMoraleTest(this);

		public void LoadAssets(ContentManager content) => models.ForEach(x => x.LoadAssets(content));

		public void UnloadAssets() => models.ForEach(x => x.UnloadAssets());

		public void Update(GameTime time) =>
			models.ForEach(x => x.Update(time));

		public void OnGlobalLeftMouseClick(Point point, IEnumerable<IMouseInteraction> mouseOver)
		{
			if (mouseOver.Contains(this))
			{
				Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: Left clicked on {this}");
				this.Selected = true;
			}
			else
				this.Selected = false;

			// Propagate event to models.
			models.ForEach(x => x.OnGlobalLeftMouseClick(point, mouseOver));
		}

		public bool IsMouseOver(Point clickPosition) =>
			models.Any(x => x.IsMouseOver(clickPosition));

		public void OnGlobalRightMouseClick(Point point, IEnumerable<IMouseInteraction> mouseOver)
		{
			if (this.Selected)
			{
				if (mouseOver.Any(x => x is IUnit unit && unit != this))
				{
					// Attaaaaack!
					var target = mouseOver.First(x => x is IUnit) as IUnit;

					foreach (var item in Models)
						item.Target = target;
				}
				else
				{
					// Move the unit;
					var maxBaseSize = Models.Select(x => x.BaseSize.GameDistance).Max();
					var sqrt = Math.Ceiling(Math.Sqrt(Models.Count()));

					for (int i = 0; i < models.Count; i++)
					{
						var item = models[i];

						var column = (i % sqrt) ;
						var row = Math.Floor(i / sqrt);
						if (item.Health > 0)
							item.OnGlobalRightMouseClick(point + new Point(Convert.ToInt32(column * maxBaseSize), Convert.ToInt32(row * maxBaseSize)), mouseOver);
						else
							item.OnGlobalRightMouseClick(point, mouseOver);
					}
				}

			}
			else
				// Propagate event to models.
				models.ForEach(x => x.OnGlobalRightMouseClick(point, mouseOver));
		}
	}
}