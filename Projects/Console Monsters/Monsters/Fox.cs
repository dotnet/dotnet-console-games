using Console_Monsters.Moves;

namespace Console_Monsters.Monsters;

internal class Fox : MonsterBase
{
	public Fox()
	{
		Name = "Fox";
		Level = 5;

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
		@" /\                  " + '\n' +
		@"/~~\  <‾__>╭───╮<__‾>" + '\n' +
		@"\   \      │^_^│     " + '\n' +
		@" \___>╭────╯~~┬╯     " + '\n' +
		@"      │ ╭┬──╮ ││     " + '\n' +
		@"      ╰─╯╯  ╰─╯╯     ").Split('\n');
}
