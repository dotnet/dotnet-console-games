namespace Console_Monsters.Moves;

public class QuickAttack : MoveBase
{
	public QuickAttack()
	{
		Name = "Quick Attack";
		BaseDamage = 40;
		EnergyTaken = 10;
		DamageType = Enums.DamageType.Physical;
		Element = Enums.CMType.Normal;
		Priority = 1;
	}
}
