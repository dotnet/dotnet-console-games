using Console_Monsters.Moves;

namespace Console_Monsters.Monsters;

public class FireLizard : MonsterBase
{
	public FireLizard()
	{
		Name = "Fire Lizard";

		BaseHP = 44;
		MaximumHP = SetMaxHPFromBase(BaseHP, Level);
		CurrentHP = MaximumHP;
		MaximumEnergy = 50;

		CurrentEnergy = MaximumEnergy;

		AttackStat = 10;
		SpeedStat = 10;
		DefenseStat = 10;

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
		"╰╮             " + '\n' +
		"╰╮╰╮      ╭───╮" + '\n' +
		"╰╮╰╮╰╮    │^_^│" + '\n' +
		"   ╰╮╰────╯  ╭╯" + '\n' +
		"    ╰┬╮ ╭─┬╮ │ " + '\n' +
		"     ╰╰─╯ ╰╰─╯ ").Split('\n');
}