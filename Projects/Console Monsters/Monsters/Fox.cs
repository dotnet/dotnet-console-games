using Console_Monsters.Moves;

namespace Console_Monsters.Monsters;

internal class Fox : MonsterBase
{
	public Fox()
	{
		Name = "Fox";
		Level = 5;
		MaximumHP = 20;
		MaximumEnergy = 50;
		CurrentHP = MaximumHP;
		CurrentEnergy = MaximumEnergy;
		Evolution = 1;
		AttackStat = 10;
		SpeedStat = 9;
		DefenseStat = 10;
		MoveSet = new List<MoveBase>
		{
			new Punch(),
			new Tackle(),
			new Scratch()
		};
	}

	public override string[] Sprite => (
		@" /\                  " + '\n' +
		@"/~~\  <‾__>╭───╮<__‾>" + '\n' +
		@"\   \      │^_^│     " + '\n' +
		@" \___>╭────╯~~┬╯     " + '\n' +
		@"      │ ╭┬──╮ ││     " + '\n' +
		@"      ╰─╯╯  ╰─╯╯     ").Split('\n');
}
