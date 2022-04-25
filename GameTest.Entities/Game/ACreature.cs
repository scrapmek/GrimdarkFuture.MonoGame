using System;
using System.Collections.Generic;
using System.Linq;
using GameTest.Entities.Interfaces;
using GameTest.Mechanics.Interfaces;
using GameTest.Mechanics.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameTest.Entities
{
	public abstract class AModel : IRenderableEntity, IGameEntity, IMoveableEntity, ILeftClickInteraction, IRightClickInteraction, ISelectable, IModel
	{
		public int AnimatedMoveSpeed { get; } = 2;
		public int? Defense => 2;
		public Vector2 Destination { get; set; }
		public int Health { get; private set; } = 1;
		public GameTime LastUpdated { get; private set; }
		public Rectangle MousePointerMesh => new Rectangle(this.Position.ToPoint() - this.TexturePositionOffset.ToPoint(), new Vector2(this.Texture.Width, this.Texture.Height).ToPoint());
		public InchMeasure MoveStat => 6;
		public Vector2 Position { get; private set; }
		public int Quality => 3;
		public bool Selected { get; private set; }
		public IUnit Target { get; set; }
		public Texture2D Texture { get; protected set; }
		public abstract string TextureName { get; }
		public Vector2 TexturePositionOffset => new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);

		public void ApplyDamage(int damage) => this.Health -= damage;

		public void FireWeapon(IRangedWeapon rangedWeapon, IUnit target, int range) => GameTest.Mechanics.Utilities.ShootingAttackCalculator.DefaultShootingAttack(this, target, rangedWeapon, range);

		public void Flee() => this.Health = 0;

		public bool IsMouseOver(Point clickPosition) => this.MousePointerMesh.Contains(clickPosition);

		public virtual void LoadAssets(Microsoft.Xna.Framework.Content.ContentManager content)
		{
			try
			{
				this.Texture = content.Load<Texture2D>(this.TextureName);
			}
			catch (System.Exception)
			{
				throw new System.Exception($"Failed to load texture for: {this.GetType()} using {this.TextureName}");
			}
		}

		public void MeleeAttack(IMeleeWeapon meleeWeapon, IUnit target) => throw new System.NotImplementedException();

		public void OnLeftMouseClick(Point point, IEnumerable<IMouseInteraction> entities)
		{
			if (entities.Contains(this))
			{
				Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: Left clicked on {this}");
				this.Selected = true;
			}
		}


		public void OnRightMouseClick(Point point, IEnumerable<IMouseInteraction> entities )
		{
			if (this.Selected)
				this.Destination = point.ToVector2();
		}


		public void Update(GameTime time)
		{
			if (this.Destination != this.Position)
				this.Move(time);

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
	}

	public class SpaceMarine : AModel
	{
		public override string TextureName => "Circle";
	}
}