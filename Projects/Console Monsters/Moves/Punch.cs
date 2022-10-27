namespace Console_Monsters.Moves;

public class Punch : MoveBase
{
	public Punch()
	{
		Name = "Punch";
		Power = 40;
		FinalDamage = (((2 * AttackingMonster!.Level / 5 + 2) 
			* Power * AttackingMonster.AttackStat / DefendingMonster!.DefenseStat) / 50 + 2) 
			* BattleRandom.Next(85, 101) / 100;
		EnergyTaken = 10;
	}
}
