namespace Console_Monsters.Moves;

public class Punch : MoveBase
{
	public Punch()
	{
		Name = "Punch";
		BaseDamage = 40;
		EnergyTaken = 10;
		DamageType = Enums.DamageType.Physical;
		Element = Enums.CMType.Normal;
	}
}
