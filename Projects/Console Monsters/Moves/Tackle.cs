namespace Console_Monsters.Moves;

public class Tackle : MoveBase
{
	public Tackle()
	{
		Name = "Tackle";
		BaseDamage = 40;
		EnergyTaken = 10;
		DamageType = Enums.DamageType.Physical;
		Element = Enums.CMType.Normal;
	}
}
