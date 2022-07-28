namespace Console_Monsters.Monsters;

public class Egg : MonsterBase
{
	public Egg()
	{
		Name = "Egg";
	}

	public override string[] Sprite => (
		@" ╭───╮ " + "\n" +
		@" │^_^│ " + "\n" +
		@"ᴄ├˄^˄┤ᴐ" + "\n" +
		@" ╰U─U╯ ").Split('\n');
}
