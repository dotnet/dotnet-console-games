namespace Console_Monsters.Monsters;

public class SpikyMouse : MonsterBase
{
	public SpikyMouse()
	{
		Name = "Spiky Mouse";
	}

	public override string[] Sprite => (
		@"       ᴧ^ᴧ^ᴧ " + "\n" +
		@"     <˂╰⁰ ⁰╯╮" + "\n" +
		@"    <˂  ╰─╯ ╮" + "\n" +
		@"   ˂<  ╰─≥ ≤╯" + "\n" +
		@"ᴧ   <˂╮ _ ╭╯ " + "\n" +
		@"╰───╯ ╰╯ ╰╯  ").Split('\n');
}
