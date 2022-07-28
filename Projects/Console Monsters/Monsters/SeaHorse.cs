namespace Console_Monsters.Monsters;

internal class Seahorse : MonsterBase
{
	public Seahorse()
	{
		Name = "Seahorse";
	}

	public override string[] Sprite => (
		@" ╭───╮   " + '\n' +
		@"o╡^ ≤│   " + '\n' +
		@" ╰┬─ │<_)" + '\n' +
		@"╭─╮≡ │   " + '\n' +
		@"│┬╯┘ │   " + '\n' +
		@"╰────╯   ").Split('\n');
}
