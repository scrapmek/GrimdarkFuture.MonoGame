using System;
using System.Collections.Generic;
using System.Linq;
using GrimdarkFuture.Entities.Interfaces;
using GrimdarkFuture.Mechanics.Interfaces;
using GrimdarkFuture.Mechanics.Models;
using GrimdarkFuture.Mechanics.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GrimdarkFuture.Entities
{
	public abstract class AModel : IGameEntity, IMoveableEntity, IGlobalLeftClickInteraction, IGlobalRightClickInteraction, ISelectable, IModel
	{
		protected AModel(Vector2 position)
		{
			this.Position = position;
			this.Destination = position;
		}

		public Vector2 Destination { get; set; }
		public int Health { get; private set; } = 1;
		public GameTime LastUpdated { get; private set; }
		private Rectangle MousePointerMesh => new Rectangle(this.Position.ToPoint() - this.TexturePositionOffset.ToPoint(), new Vector2(this.Texture.Width, this.Texture.Height).ToPoint());
		public Vector2 Position { get; private set; }
		
		public abstract InchMeasure MoveStat { get; }
		public abstract int? Defense { get; }
		public abstract int Quality { get; }
		public abstract Faction Faction { get; }
		public abstract IEnumerable<IWeapon> Weapons { get; }
		
		public bool Selected { get; private set; }
		
		public IUnit Target { get; set; }
		
		public Texture2D Texture { get; protected set; }
		public abstract string TextureName { get; }
		public Vector2 TexturePositionOffset => new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
		public int AnimatedMoveSpeed { get; } = 2;

		public void FireWeapon(IRangedWeapon rangedWeapon, IUnit target, int range) => GrimdarkFuture.Mechanics.Utilities.ShootingAttackCalculator.DefaultShootingAttack(this, target, rangedWeapon, range);
		public void ApplyDamage(int damage) => this.Health -= damage;

		public void Flee() => this.Health = 0;

		public bool IsMouseOver(Point clickPosition) => this.MousePointerMesh.Contains(clickPosition);

		public void MeleeAttack(IMeleeWeapon meleeWeapon, IUnit target) => MeleeCalculator.DefaultRangedAttack(this, target, meleeWeapon);

		public void OnLeftMouseClick(Point point, IEnumerable<IMouseInteraction> entities)
		{
			if (entities.Contains(this))
			{
				Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: Left clicked on {this}");
				this.Selected = true;
			}
			else
				this.Selected = false;
		}


		public void OnRightMouseClick(Point point, IEnumerable<IMouseInteraction> entities )
		{
			if (this.Selected)
				this.Destination = point.ToVector2();
		}


		public void Update(GameTime time)
		{
			if (this.Destination != this.Position)
				Move(time);

			this.LastUpdated = time;
		}

		protected void Move(GameTime time)
		{
			var direction = this.Destination - this.Position;
			var distance = Vector2.Distance(this.Destination, Position);

			if (distance < this.AnimatedMoveSpeed)
				this.Position = this.Destination;
			else
			{
				var multiplier = this.AnimatedMoveSpeed / distance;

				this.Position += Vector2.Multiply(direction, multiplier);
			}
		}

		public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(this.Texture, this.Position - this.TexturePositionOffset, Color.White);
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

		public void UnloadAssets() => throw new NotImplementedException();
	}

	public class SwordBrethren : AModel
	{
		public SwordBrethren(Vector2 position) : base(position)
		{
		}


		public override string TextureName => "SpaceMarine";

		public override InchMeasure MoveStat => 6;
		public override int? Defense => 2;
		public override int Quality => 3;

		public override Faction Faction => Faction.BlackTemplars;

		public override IEnumerable<IWeapon> Weapons => new IWeapon[] { new Chainsword(), new BoltPistol() };
	}

	public class ChaosSpaceMarineHavoc : AModel
	{
		public ChaosSpaceMarineHavoc(Vector2 position) : base(position)
		{
		}

		public override string TextureName => "CSM";

		public override InchMeasure MoveStat => 6;
		public override int? Defense => 2;
		public override int Quality => 3;

		public override Faction Faction => Faction.BlackTemplars;

		public override IEnumerable<IWeapon> Weapons => new IWeapon[] { new HeavyBolter() };
	}

	public class HeavyBolter : IRangedWeapon
	{
		public InchMeasure MaxRange => 36;

		public int GetAP(IModel shooter, IUnit target, int range) => 1;
		public int GetDamage(IModel shooter, IUnit target, int range) => 2;
		public int GetShots(IModel shooter, IUnit target, int range) => 3;
	}

	public class Chainsword : IMeleeWeapon
	{
		public int GetAP(IModel shooter, IUnit target) => 1;
		public int GetAttacks(IModel attacker, IUnit target) => 2;
		public int GetDamage(IModel shooter, IUnit target) => 1;
	}

	public class BoltPistol : IRangedWeapon
	{
		public InchMeasure MaxRange => 18;

		public int GetAP(IModel shooter, IUnit target, int range) => 1;
		public int GetDamage(IModel shooter, IUnit target, int range) => 1;
		public int GetShots(IModel shooter, IUnit target, int range) => 1;
	}
}