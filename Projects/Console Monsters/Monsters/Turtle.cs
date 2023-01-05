using Console_Monsters.Moves;

namespace Console_Monsters.Monsters;

public class Turtle : MonsterBase
{
	public Turtle()
	{
		Name = "Turtle";
		Level = 5; //TEMP
#warning Fix level not being set here

		HPStat = 44;
		MaximumHP = SetMaxHPFromBase(HPStat, Level, Statexp);
		CurrentHP = MaximumHP;

		BaseEnergy = 50;
		MaximumEnergy = 50;
		CurrentEnergy = MaximumEnergy;

		AttackStat = 48;
		SpecialAttackStat = 50;
		DefenseStat = 65;
		SpecialDefenseStat = 64;
		SpeedStat = 43;

		Statexp = 0;

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
		"   ╭───╮   " + '\n' +
		"   │^_^│   " + '\n' +
		"╭──╔═══╗──╮" + '\n' +
		"╰─╔╝   ╚╗─╯" + '\n' +
		" ╭╚╗   ╔╝╮ " + '\n' +
		" ╰─╚═══╝─╯ ").Split('\n');
}