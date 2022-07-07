namespace Console_Monsters.Monsters;

public class FireLizard : MonsterBase
{
	public FireLizard()
	{
		Name = "Fire Lizard Small";
		Level = 5;
		MaximumHP = 20;
		MaximumEnergy = 50;
		CurrentHP = MaximumHP;
		CurrentEnergy = MaximumEnergy;
		Evolution = 1;
		AttackStat = 10;
		SpeedStat = 10;
		DefenseStat = 10;
	}

	public override string[] Sprite => (
		"╰╮             " + '\n' +
		"╰╮╰╮      ╭───╮" + '\n' +
		"╰╮╰╮╰╮    │^_^│" + '\n' +
		"   ╰╮╰────╯  ╭╯" + '\n' +
		"    ╰┬╮ ╭─┬╮ │ " + '\n' +
		"     ╰╰─╯ ╰╰─╯ ").Split('\n');
}