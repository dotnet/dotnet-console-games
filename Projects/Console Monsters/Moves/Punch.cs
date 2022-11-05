namespace Console_Monsters.Moves;

public class Punch : MoveBase
{
	const int level = 5;
	const int defenseStat = 10;
	const int attackStat = 10;

	public Punch()
	{
		Name = "Punch";
		BaseDamage = 40;
		EnergyTaken = 10;
	}
}
