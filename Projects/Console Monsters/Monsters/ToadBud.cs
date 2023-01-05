using Console_Monsters.Moves;

namespace Console_Monsters.Monsters;

public class ToadBud : MonsterBase
{
	public ToadBud()
	{
		Name = "Toad Bud";

		MaximumHP = SetMaxHPFromBase(HPStat, Level, Statexp);
		CurrentHP = MaximumHP;
		MaximumEnergy = 50;
		CurrentEnergy = MaximumEnergy;

		HPStat = 45;
		AttackStat = 10;
		SpeedStat = 10;
		DefenseStat = 10;

		Statexp = AttackStat + SpecialAttackStat + DefenseStat + SpecialDefenseStat + SpeedStat;

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
		"  ╭─────╮  " + '\n' +
		" ╭│ ^_^ │╮ " + '\n' +
		"╭─╰─────╯─╮" + '\n' +
		"│ ╭─────╮ │" + '\n' +
		"╰─╯─╯ ╰─╰─╯").Split('\n');
}