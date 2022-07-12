namespace Console_Monsters.Monsters;

internal class FireHorse : MonsterBase
{
	public FireHorse()
	{
		Name = "Fire Horse";
	}

	public override string[] Sprite => (
		@"         ╰╮╰╮   " + "\n" +
		@"╰╮╰╮╰╮     /^╰─╮" + "\n" +
		@"╰╮╰╮╰╮╰╮╰╮/ /‾‾ " + "\n" +
		@" ╰/‾‾‾‾‾‾  /    " + "\n" +
		@"  │\ \──┬\ \    " + "\n" +
		@"  ││//  ││//    " + "\n" +
		@"  ┕┙˅   ┕┙˅     ").Split('\n');

	// Alternate Sprite
	//
	//            ╰╮╰╮
	// ╰╮╰╮╰╮╰╮     /^╰─╮
	//   ╰╮╰╮╰╮╰╮╰╮/ /‾‾
	//    ╰/‾‾‾‾‾‾  /
	//    / \ \─┬┬\ \
	//   // // // //
	//   ˅  ˅  ˅  ˅
}
