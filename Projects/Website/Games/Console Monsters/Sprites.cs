using System;
using System.Linq;
using System.Text;
using System.Threading;
using static Website.Games.Console_Monsters._using;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Monsters;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.NPCs;
using System.Collections.Generic;

namespace Website.Games.Console_Monsters;

public static class Sprites
{
	public const int Width = 7;
	public const int Height = 5;

	public const int BattleSpriteWidth = 70;
	public const int BattleSpriteHeight = 20;

	#region Interior
	public const string InteriorWallNE =
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" │ ╰───" + "\n" +
		@" ╰─────" + "\n" +
		@"       ";
	public const string InteriorWallNW =
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"───╯ │ " + "\n" +
		@"─────╯ " + "\n" +
		@"       ";
	public const string InteriorWallSW =
		@"       " + "\n" +
		@"─────╮ " + "\n" +
		@"───╮ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ ";
	public const string InteriorWallSE =
		@"       " + "\n" +
		@" ╭─────" + "\n" +
		@" │ ╭───" + "\n" +
		@" │ │   " + "\n" +
		@" │ │   ";
	public const string InteriorWallEWHigh =
		@"       " + "\n" +
		@"───────" + "\n" +
		@"───────" + "\n" +
		@"       " + "\n" +
		@"       ";
	public const string InteriorWallEWLow =
		@"       " + "\n" +
		@"       " + "\n" +
		@"───────" + "\n" +
		@"───────" + "\n" +
		@"       ";
	public const string InteriorWallNSLeft =
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" │ │   ";
	public const string InteriorWallNSRight =
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ ";
	public const string InteriorWallSWEHighLeft =
		@"       " + "\n" +
		@"───────" + "\n" +
		@"─╮ ╭───" + "\n" +
		@" │ │   " + "\n" +
		@" │ │   ";
	public const string InteriorWallSWEHighRight =
		@"       " + "\n" +
		@"───────" + "\n" +
		@"───╮ ╭─" + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ ";
	public const string InteriorWallNLeft =
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" ╰─╯   ";
	public const string InteriorWallNRight =
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   ╰─╯ ";
	#endregion

	#region Buildings
	public const string BuildingSmall =
		@"       " + "\n" +
		@" /---\ " + "\n" +
		@"/-----\" + "\n" +
		@"|     |" + "\n" +
		@"|  █  |";
	public const string VetSmall = // REMOVE? (Make it bigger like the pokecenter?)
		@"       " + "\n" +
		@" /---\ " + "\n" +
		@"/-Vet-\" + "\n" +
		@"|     |" + "\n" +
		@"|  █  |";
	public const string Store = // REMOVE? (Make it bigger like the pokemart?)
		@"       " + "\n" +
		@" /---\ " + "\n" +
		@"/Store\" + "\n" +
		@"|     |" + "\n" +
		@"|  █  |";
	public const string Door =
		@"╔═════╗" + "\n" +
		@"║ ■■■ ║" + "\n" +
		@"║    o║" + "\n" +
		@"║     ║" + "\n" +
		@"╚═════╝";
	public const string TopRoofLeft =
		@"       " + "\n" +
		@"   ////" + "\n" +
		@"  /////" + "\n" +
		@" //////" + "\n" +
		@"///////";
	public const string TopRoofRight =
		@"       " + "\n" +
		@"\\\\   " + "\n" +
		@"\\\\\  " + "\n" +
		@"\\\\\\ " + "\n" +
		@"\\\\\\\";
	public const string MiddleRoof =
		@"       " + "\n" +
		@"|||||||" + "\n" +
		@"|||||||" + "\n" +
		@"|||||||" + "\n" +
		@"|||||||";
	public const string BuildingLeft =
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      ";
	public const string BuildingBaseLeft =
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"└──────";
	public const string BuildingRight =
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │";
	public const string BuildingBaseRight =
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"──────┘";
	public const string MiddleWindow =
		@"       " + "\n" +
		@"       " + "\n" +
		@" ▐█ ▐█ " + "\n" +
		@"       " + "\n" +
		@"       ";
	public const string LowWindowSideLeft =
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │";
	public const string LowWindow =
		@"       " + "\n" +
		@"       " + "\n" +
		@" ▐█ ▐█ " + "\n" +
		@"       " + "\n" +
		@"───────";
	#endregion

	#region Objects
	public const string SignALeft =
		@" ┬──┬─┐" + "\n" +
		@"╭┴──┴╮│" + "\n" +
		@"│Sign││" + "\n" +
		@"╰────╯│" + "\n" +
		@"      │";
	public const string SignARight =
		@"┌─┬──┬─" + "\n" +
		@"│╭┴──┴╮" + "\n" +
		@"││Sign│" + "\n" +
		@"│╰────╯" + "\n" +
		@"│      ";
	public const string SignBLeft =
		@"       " + "\n" +
		@"╭────╮ " + "\n" +
		@"│Sign│ " + "\n" +
		@"╰─┬┬─╯ " + "\n" +
		@"  ││   ";
	public const string SignBRight =
		@"       " + "\n" +
		@" ╭────╮" + "\n" +
		@" │Sign│" + "\n" +
		@" ╰─┬┬─╯" + "\n" +
		@"   ││  ";
	public const string PotPlant1 =
		@"  ╬╬╬  " + "\n" +
		@" ╬╬╬╬╬ " + "\n" +
		@"  ╬╬╬  " + "\n" +
		@" _|_|_ " + "\n" +
		@" \___/ ";
	public const string PotPlant2 =
		@"  ###  " + "\n" +
		@" ##### " + "\n" +
		@"  ###  " + "\n" +
		@" _|_|_ " + "\n" +
		@" \___/ ";
	public const string Fence =
		@"       " + "\n" +
		@"╔═════╗" + "\n" +
		@"╠═════╣" + "\n" +
		@"║     ║" + "\n" +
		@"╩     ╩";
	public const string FenceLow =
		@"       " + "\n" +
		@"       " + "\n" +
		@"╦═════╦" + "\n" +
		@"╬═════╬" + "\n" +
		@"╩     ╩";
	public const string DeskLeft =
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"║      " + "\n" +
		@"╚══════" + "\n" +
		@"       ";
	public const string DeskRight =
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"      ║" + "\n" +
		@"══════╝" + "\n" +
		@"       ";
	public const string DeskMiddle =
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"       " + "\n" +
		@"═══════" + "\n" +
		@"       ";
	public const string DeskBottom =
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"███████";
	#endregion

	#region Wall
	public const string Wall_0000 =
		@"╔═════╗" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"╚═════╝";
	public const string Wall_0001 =
		@"══════╗" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"══════╝";
	public const string Wall_0010 =
		@"╔═════╗" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║";
	public const string Wall_0011 =
		@"══════╗" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"╗█████║";
	public const string Wall_0100 =
		@"╔══════" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"╚══════";
	public const string Wall_0101 =
		@"═══════" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"═══════";
	public const string Wall_0110 =
		@"╔══════" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║█████╔";
	public const string Wall_0111 =
		@"═══════" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"╗█████╔";
	public const string Wall_1000 =
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"╚═════╝";
	public const string Wall_1001 =
		@"╝█████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"══════╝";
	public const string Wall_1010 =
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║";
	public const string Wall_1011 =
		@"╝█████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"╗█████║";
	public const string Wall_1100 =
		@"║█████╚" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"╚══════";
	public const string Wall_1101 =
		@"╝█████╚" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"═══════";
	public const string Wall_1110 =
		@"║█████╚" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║█████╔";
	public const string Wall_1111 =
		@"╝█████╚" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"╗█████╔";
	#endregion

	#region Nature
	public const string Water =
		@"~~~~~~~" + "\n" +
		@"~~~~~~~" + "\n" +
		@"~~~~~~~" + "\n" +
		@"~~~~~~~" + "\n" +
		@"~~~~~~~";
	public const string Gate =
		@"▀▄▀▄▀▄▀" + "\n" +
		@"▀▄▀▄▀▄▀" + "\n" +
		@"▀▄▀▄▀▄▀" + "\n" +
		@"▀▄▀▄▀▄▀" + "\n" +
		@"▀▄▀▄▀▄▀";
	public const string Tree =
		@" /‾‾‾\ " + "\n" +
		@"/‾\ /‾\" + "\n" +
		@"\_____/" + "\n" +
		@"  | |  " + "\n" +
		@"  / \  ";
	public const string Tree2 =
		@"  (@@) " + "\n" +
		@" (@@@@)" + "\n" +
		@"   ||  " + "\n" +
		@"   ||  " + "\n" +
		@"   ||  ";
	public const string GrassDec =
		@" .   . " + "\n" +
		@".  . . " + "\n" +
		@" .   . " + "\n" +
		@".  .  ." + "\n" +
		@" .   . ";
	public const string Grass =
		@" v   v " + "\n" +
		@"v  v  v" + "\n" +
		@"  v  v " + "\n" +
		@"v  v  v" + "\n" +
		@" v  v  ";
	public const string Grass2 =
		@" ╷ ╷ v " + "\n" +
		@"╷ v ╷ ╷" + "\n" +
		@" ╷ ╷ v " + "\n" +
		@"v ╷ ╷ ╷" + "\n" +
		@" ╷ v ╷ ";
	public const string Grass3 =
		@"╵╷╵╷╵╷╵" + "\n" +
		@"╵╷╵╷╵╷╵" + "\n" +
		@"╵╷╵╷╵╷╵" + "\n" +
		@"╵╷╵╷╵╷╵" + "\n" +
		@"╵╷╵╷╵╷╵";
	public const string HalfRock =
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"#######" + "\n" +
		@"#######";
	public const string HalfRockGrass =
		@" .  .  " + "\n" +
		@".  . . " + "\n" +
		@" .   . " + "\n" +
		@"#######" + "\n" +
		@"#######";
	public const string HalfRockStairs =
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"▬▬▬▬▬▬▬" + "\n" +
		@"▬▬▬▬▬▬▬";
	public const string HalfRockStairsGrass =
		@" .  .  " + "\n" +
		@".  . . " + "\n" +
		@" .   . " + "\n" +
		@"▬▬▬▬▬▬▬" + "\n" +
		@"▬▬▬▬▬▬▬";
	public const string ArrowLightDown =
		@"  |‾|  " + "\n" +
		@"  | |  " + "\n" +
		@" _| |_ " + "\n" +
		@" \   / " + "\n" +
		@"  \ /  ";
	public const string ArrowLightUp =
		@"  / \  " + "\n" +
		@" /_ _\ " + "\n" +
		@"  | |  " + "\n" +
		@"  | |  " + "\n" +
		@"  |_|  ";
	public const string ArrowHeavyDown =
		@"  ███  " + "\n" +
		@"  ███  " + "\n" +
		@"  ███  " + "\n" +
		@"▀█████▀" + "\n" +
		@"  ▀█▀  ";
	public const string ArrowHeavyUp =
		@"  ▄█▄  " + "\n" +
		@"▄█████▄" + "\n" +
		@"  ███  " + "\n" +
		@"  ███  " + "\n" +
		@"  ███  ";
	public const string Mountains =
		@" /_\   " + "\n" +
		@"/   /_\" + "\n" +
		@"/_\/   " + "\n" +
		@"   \   " + "\n" +
		@"    \   ";
	public const string Mountain =
		@"       " + "\n" +
		@"   /\  " + "\n" +
		@"  /--\ " + "\n" +
		@" /    \" + "\n" +
		@"/      ";
	public const string Mountain2 =
		@"       " + "\n" +
		@"   /\  " + "\n" +
		@"  /\/\ " + "\n" +
		@" /    \" + "\n" +
		@"/      ";
	public const string Mountain3 =
		@"       " + "\n" +
		@"   /\  " + "\n" +
		@"  /**\ " + "\n" +
		@" /    \" + "\n" +
		@"/      ";
	#endregion

	#region NPCs
	public static readonly string NPC1 =
		@"/_____\" + '\n' +
		@" │'_'│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"╰├───┤╯" + '\n' +
		@" │_|_│ ";
	public static readonly string NPC3 =
		@"╭╭───╮╮" + '\n' +
		@" │^_^│ " + '\n' +
		@"╭┴───┴╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";
	public static readonly string NPC4 =
		@"////\\\" + '\n' +
		@"//^_^\\" + '\n' +
		@"╭┴───┴╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";
	public static readonly string NPC5 =
		@" (((‾))" + '\n' +
		@"((^_^))" + '\n' +
		@"╭┴───┴╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_ _│ ";
	public static readonly string NPC7 =
		@" ╭───╮ " + '\n' +
		@" /^_^\ " + '\n' +
		@"╰─────╯" + '\n' +
		@"╰├───┤╯" + '\n' +
		@" │_|_│ ";
	public static readonly string NPC9 =
		@" ╭▲─▲╮ " + '\n' +
		@" │‾◊‾│ " + '\n' +
		@"╭┴───┴╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";
	public static readonly string NPC10 =
		@" §§§§§ " + '\n' +
		@"§§^_^§§" + '\n' +
		@"╭┴───┴╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";
	public static readonly string NPC11 =
		@" ▄███▄ " + '\n' +
		@"▀█^_^█▀" + '\n' +
		@"╭┴───┴╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";
	public static readonly string NPC12 =
		@" ╯╯╯╯╮╮" + '\n' +
		@"╯╯^_^╰╰" + '\n' +
		@"╭┴───┴╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_ _│ ";
	#endregion

	#region Items
	public const string HealthPotionLarge =
		@" [╤═╤] " + "\n" +
		@" ╭╯ ╰╮ " + "\n" +
		@" │▄█▄│ " + "\n" +
		@" │ ▀ │ " + "\n" +
		@" ╰───╯ ";
	public const string HealthPotionMedium =
		@" [╤═╤] " + "\n" +
		@" ╭╯ ╰╮ " + "\n" +
		@" │╺╋╸│ " + "\n" +
		@" ╰───╯ " + "\n" +
		@"       ";
	public const string HealthPotionSmall =
		@" [╤╤]  " + "\n" +
		@" ╭╯╰╮  " + "\n" +
		@" │+ │  " + "\n" +
		@" ╰──╯  " + "\n" +
		@"       ";
	public const string MonsterBox =
		@"       " + "\n" +
		@" ╭───╮ " + "\n" +
		@" ╞═●═╡ " + "\n" +
		@" ╰───╯ " + "\n" +
		@"       ";
	public const string XPBerries =
		@"   \   " + "\n" +
		@" ()(() " + "\n" +
		@"()()())" + "\n" +
		@" (()() " + "\n" +
		@"  ())  ";
	#endregion

	public const string FullBlock =
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"███████";
	public const string Open =
		@"       " + "\n" +
		@"       " + "\n" +	
		@"       " + "\n" +
		@"       " + "\n" +
		@"       ";
	public const string Error =
		@"╔═════╗" + "\n" +
		@"║error║" + "\n" +
		@"║error║" + "\n" +
		@"║error║" + "\n" +
		@"╚═════╝";
}
