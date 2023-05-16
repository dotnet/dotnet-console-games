namespace Console_Monsters.Moves;

public class Scratch : MoveBase
{
	public Scratch()
	{
		Name = "Scratch";
		BaseDamage = 40;
		EnergyTaken = 10;
		DamageType = Enums.DamageType.Physical;
		Element = Enums.CMType.Normal;
		Priority = 0;
	}
}
