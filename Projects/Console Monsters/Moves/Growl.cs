namespace Console_Monsters.Moves;

public class Growl : MoveBase
{
	public Growl()
	{
		Name = "Growl";
		BaseDamage = 0;
		EnergyTaken = 10;
		DamageType = Enums.DamageType.Special;
		Element = Enums.CMType.Normal;
		Priority = 0;
	}
}
