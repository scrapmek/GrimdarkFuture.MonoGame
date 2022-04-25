namespace GameTest.Mechanics.Interfaces
{
	public interface IModel : IStatBlock
	{
		public IUnit Target { get; set; }

		void Flee();

		void ApplyDamage(int damage);

		void MeleeAttack(IMeleeWeapon meleeWeapon, IUnit target);

		void FireWeapon(IRangedWeapon rangedWeapon, IUnit target, int range);
	}


}