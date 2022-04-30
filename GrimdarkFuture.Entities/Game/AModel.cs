using System;
using System.Collections.Generic;
using System.Linq;
using GrimdarkFuture.Entities.Interfaces;
using GrimdarkFuture.Entities.Interfaces.Game;
using GrimdarkFuture.Mechanics.Interfaces;
using GrimdarkFuture.Mechanics.Models;
using GrimdarkFuture.Mechanics.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GrimdarkFuture.Entities.Game
{
	public abstract class AModel : IGameEntity, IMoveableEntity, IGlobalLeftClickInteraction, IGlobalRightClickInteraction, ISelectable, IModel
	{
		public int AnimatedMoveSpeed { get; } = 2;

		public abstract int? Defense { get; }

		public Vector2 Destination { get; set; }

		public abstract Faction Faction { get; }

		public int Health { get; private set; } = 1;

		public GameTime LastUpdated { get; private set; }

		public abstract GameMeasure MoveStat { get; }

		public Vector2 Position { get; private set; }

		public abstract int Quality { get; }

		public bool Selected { get; private set; }

		public IUnit Target { get; set; }

		public Texture2D Texture { get; protected set; }

		public abstract string TextureName { get; }

		public Vector2 TexturePositionOffset => new Vector2(this.BaseSize.GameDistance / 2, this.BaseSize.GameDistance / 2);

		public abstract IEnumerable<IWeapon> Weapons { get; }

		private Rectangle MousePointerMesh => new Rectangle(this.Position.ToPoint() - this.TexturePositionOffset.ToPoint(), new Vector2(this.Texture.Width, this.Texture.Height).ToPoint());

		public abstract GameMeasure BaseSize { get; }

		protected AModel(Vector2 position)
		{
			this.Position = position;
			this.Destination = position;
		}

		public void ApplyDamage(int damage) => this.Health -= damage;

		public void Draw(SpriteBatch spriteBatch) {

			var hScale = this.BaseSize.GameDistance / this.Texture.Height;
			var wScale = this.BaseSize.GameDistance/ this.Texture.Width ;

			var minScale = Math.Min(hScale, wScale);

			spriteBatch.Draw(this.Texture, this.Position - this.TexturePositionOffset, null, Color.White,0, Vector2.Zero,minScale,SpriteEffects.None,0); 
		}

		public void FireWeapon(IRangedWeapon rangedWeapon, IUnit target, GameMeasure range) => ShootingAttackCalculator.DefaultShootingAttack(this, target, rangedWeapon, range);

		public void Flee() => this.Health = 0;

		public bool IsMouseOver(Point clickPosition) => this.MousePointerMesh.Contains(clickPosition);

		public void LoadAssets(ContentManager content)
		{
			try
			{

				this.Texture = content.Load<Texture2D>(this.TextureName);

			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to load texture for: {GetType()} using {this.TextureName}", ex);
			}
		}

		public void MeleeAttack(IMeleeWeapon meleeWeapon, IUnit target) => MeleeCalculator.DefaultRangedAttack(this, target, meleeWeapon);

		public void OnGlobalLeftMouseClick(Point point, IEnumerable<IMouseInteraction> entities)
		{
			if (entities.Contains(this) || entities.Any(c => c is IUnit unit && unit.Models.Contains(this)))
			{
				Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: Left clicked on {this}");
				this.Selected = true;
			}
			
			else
			{
				this.Selected = false;
			}
		}

		public void OnGlobalRightMouseClick(Point point, IEnumerable<IMouseInteraction> entities)
		{
			if (this.Selected)
				this.Destination = point.ToVector2();
		}

		public void UnloadAssets() => throw new NotImplementedException();

		public void Update(GameTime time)
		{
			if (Target != null && Target.Models.Any())
				UpdateDestination();
			if (this.Destination != this.Position)
				Move(time);
			if (Target != null && Target.Models.Any())
				ResolveAttacks();

			this.LastUpdated = time;
		}

		private void ResolveAttacks() 
		{

			Console.WriteLine($"{this}: Resolving attacks against {Target}.");
			var targetModel = Target.Models.OrderBy(x => this.GetRange(x).GameDistance).First();

			if (GetRange(targetModel).Inches <= 1)
			{
				Console.WriteLine($"{this}: Within melee range, performing melee attack!");
				var meleeWeapons = this.Weapons.Where(x => x is IMeleeWeapon).Select(x => x as IMeleeWeapon);
				foreach (var item in meleeWeapons)
					this.MeleeAttack(item,Target);

				this.Target = null;
			}
			else if (this.Weapons.Any(x => x is IRangedWeapon))
			{
				var range = Vector2.Distance(this.Position, targetModel.Position);

				var rangedWeapons = this.Weapons.Where(x => x is IRangedWeapon).Select(x => x as IRangedWeapon);

				var weaponsInRange = rangedWeapons.Where(rangedWeapon => range <= rangedWeapon.MaxRange.GameDistance);

				if (weaponsInRange.Any())
				{
					Console.WriteLine($"{this}: Withing range, shooting!");
					foreach (var item in rangedWeapons.Where(rangedWeapon => range <= rangedWeapon.MaxRange.GameDistance))
						this.FireWeapon(item, Target, GameMeasure.FromGameDisance(range));

					this.Target = null;

				}

			}
		}

		private void UpdateDestination()
		{
			if (Target != null && Target.Models.Any())
			{
				var targetModel = Target.Models.OrderBy(x => this.GetRange(x).GameDistance).First();
				Console.WriteLine($"{this} is setting targeted model to {targetModel}.");
				this.Destination = targetModel.Position;
				if(GetRange(targetModel).Inches <= 0)
					this.Destination = this.Position;
				if (this.Weapons.Any(x => x is IRangedWeapon ranged && Vector2.Distance(this.Position,targetModel.Position) <= ranged.MaxRange.GameDistance))
				{
					// One of this model's ranges weapons is in range, stay where you are.
					Console.WriteLine($"{this} is within range of {targetModel} staying still.");
					this.Destination = this.Position;
				}
			}
		}

		protected void Move(GameTime time)
		{
			var direction = this.Destination - this.Position;
			var distance = Vector2.Distance(this.Destination, Position);

			if (distance < this.AnimatedMoveSpeed)
			{
				this.Position = this.Destination;
			}
			else
			{
				var multiplier = this.AnimatedMoveSpeed / distance;

				this.Position += Vector2.Multiply(direction, multiplier);
			}
		}

		public GameMeasure GetRange(AModel model) =>
			GameMeasure.FromGameDisance(Vector2.Distance(this.Position, model.Position)) -
			(this.BaseSize / 2) -
			(model.BaseSize / 2);
	}
}