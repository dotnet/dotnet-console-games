namespace Console_Monsters.Monsters;

internal class Flower1 : MonsterBase
{
	public Flower1()
	{
		Name = "Flower Bud";
	}

	public override string[] Sprite => (
		@"   ╮╭    " + '\n' +
		@"  ╭/╰╮   " + '\n' +
		@"  │ⱺⱺ├<> " + '\n' +
		@"  ╰╮/╯   " + '\n' +
		@" <>┤│    " + '\n' +
		@"─^─┴┴^─^─").Split('\n');
}
