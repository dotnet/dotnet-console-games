namespace Console_Monsters.Monsters;

internal class ElecticBearThing : MonsterBase
{
	public ElecticBearThing()
	{
		Name = "Electic Bear Thing";
	}

	public override string[] Sprite => (
		@"     ₒ   ₒ       " + '\n' +
		@"    __╲_╱__      " + '\n' +
		@"   ╱▬▬ⱺ♦ⱺ▬▬╲     " + '\n' +
		@"╭─wwww/┅\wwww─╮  " + '\n' +
		@"├~~╮  ╷▄╷  ╭~~┤  " + '\n' +
		@"┝▬▬┽ww┤█├ww┾▬▬┥  " + '\n' +
		@"╰╰╰│▀▀ █ ▀▀│╯╯╯ ₒ" + '\n' +
		@"   ╰┬─┬▀┬─┬╯━─━─╯" + '\n' +
		@"    ╰┅╯ ╰┅╯      ").Split('\n');
}
