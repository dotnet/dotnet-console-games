using Console_Monsters.Moves;

namespace Console_Monsters.Monsters;

public class Turtle : MonsterBase
{
	public Turtle()
	{
		Name = "Turtle";
		Level = 5;
		MaximumHP = 20;
		MaximumEnergy = 50;
		CurrentHP = MaximumHP;
		CurrentEnergy = MaximumEnergy;
		Evolution = 1;
		AttackStat = 10;
		SpeedStat = 10;
		DefenseStat = 10;
		MoveSet = new List<MoveBase>
		{
			new Punch(),
			new Tackle(),
			new Scratch()
		};
	}

	public override string[] Sprite => (
		"   ╭───╮   " + '\n' +
		"   │^_^│   " + '\n' +
		"╭──╔═══╗──╮" + '\n' +
		"╰─╔╝   ╚╗─╯" + '\n' +
		" ╭╚╗   ╔╝╮ " + '\n' +
		" ╰─╚═══╝─╯ ").Split('\n');
}