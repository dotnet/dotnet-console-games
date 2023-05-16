using Console_Monsters.Moves;

namespace Console_Monsters.Monsters;

public class Fox : MonsterBase
{
	public Fox()
	{
		Name = "Fox";
		Level = 5;
		
		MaximumHP = SetMaxHPFromBase(HPStat, Level, Statexp);
		CurrentHP = MaximumHP;
		MaximumEnergy = 50;
		CurrentEnergy = MaximumEnergy;

		HPStat = 55;
		AttackStat = 55;
		SpecialAttackStat = 45;
		DefenseStat = 50;
		SpecialDefenseStat = 65;
		SpeedStat = 55;

		Statexp = (int)HPStat + AttackStat + SpecialAttackStat + DefenseStat + SpecialDefenseStat + SpeedStat;

		MoveSet = new List<MoveBase>
		{
			new Punch(),
			new Tackle(),
			new Scratch(),
			new Growl()
		};

		Evolution = 1;
	}

	public override string[] Sprite => (
		@" /\                  " + '\n' +
		@"/~~\  <‾__>╭───╮<__‾>" + '\n' +
		@"\   \      │^_^│     " + '\n' +
		@" \___>╭────╯~~┬╯     " + '\n' +
		@"      │ ╭┬──╮ ││     " + '\n' +
		@"      ╰─╯╯  ╰─╯╯     ").Split('\n');
}
