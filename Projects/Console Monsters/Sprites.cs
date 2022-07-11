namespace Console_Monsters;

public static class Sprites
{
	public const int Width = 7;
	public const int Height = 5;

	public const int BattleSpriteWidth = 70;
	public const int BattleSpriteHeight = 20;

	#region InteriorWalls
	public const string InteriorWallNE =
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" │ ╰───" + "\n" +
		@" ╰─────" + "\n" +
		@"       ";
	public const string InteriorWallNEShort =
		@"  │ ╰── " + "\n" +
		@" ╰───── " + "\n" +
		@"        " + "\n" +
		@"        " + "\n" + 
		@"        ";
	public const string InteriorWallNW =
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"───╯ │ " + "\n" +
		@"─────╯ " + "\n" +
		@"       ";
	public const string InteriorWallNWShort =
		@"─╯ │   " + "\n" +
		@"───╯   " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"       ";
	public const string InteriorWallSW =
		@"       " + "\n" +
		@"─────╮ " + "\n" +
		@"───╮ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ ";
	public const string InteriorWallSWShort =
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"───╮   " + "\n" +
		@"─╮ │   ";
	public const string InteriorWallSE =
		@"       " + "\n" +
		@" ╭─────" + "\n" +
		@" │ ╭───" + "\n" +
		@" │ │   " + "\n" +
		@" │ │   ";
	public const string InteriorWallSEShort =
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"  ╭────" + "\n" +
		@"  │ ╭──";
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
	public const string InteriorWallNSRightRight =
		@"    │ │" + "\n" +
		@"    │ │" + "\n" +
		@"    │ │" + "\n" +
		@"    │ │" + "\n" +
		@"    │ │";
	public const string InteriorWallNSMid =
		@"  │ │  " + "\n" +
		@"  │ │  " + "\n" +
		@"  │ │  " + "\n" +
		@"  │ │  " + "\n" +
		@"  │ │  ";
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
	public const string InteriorWallHorizontalBottmn =
	@"       " + "\n" +
	@"       " + "\n" +
	@"       " + "\n" +
	@"───────" + "\n" +
	@"───────";
	public const string InteriorWallHorizontalTop =
	@"───────" + "\n" +
	@"───────" + "\n" +
	@"       " + "\n" +
	@"       " + "\n" +
	@"       ";
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
	public const string Locker =
		@"╔═════╗" + "\n" +
		@"║==│==║" + "\n" +
		@"║● │● ║" + "\n" +
		@"║  │  ║" + "\n" +
		@"╚╦═══╦╝";
	public const string Lamp =
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"  ╮  ╭ " + "\n" +
		@"  ╰╮╭╯ ";
	public const string Lamp2 =
		@"   ││  " + "\n" +
		@"   ││  " + "\n" +
		@"   ││  " + "\n" +
		@"   ││  " + "\n" +
		@"  ╭╯╰╮ ";
	public const string Carpet =
		@"░░░░░░░" + "\n" +
		@"░░░░░░░" + "\n" +
		@"░░░░░░░" + "\n" +
		@"░░░░░░░" + "\n" +
		@"░░░░░░░";
	public const string Table =
		@"       " + "\n" +
		@"══════╗" + "\n" +
		@"      ║" + "\n" +
		@"══════║" + "\n" +
		@"      ║";
	public const string Chair =
		@" │───│ " + "\n" +
		@" │   │ " + "\n" +
		@" │___│ " + "\n" +
		@" │   │ " + "\n" +
		@" ┴   ┴ ";
	public const string ChairLeft =
		@"       " + "\n" +
		@" │    ╔" + "\n" +
		@" │___ ║" + "\n" +
		@" │   │║" + "\n" +
		@" ┴   ┴║";
	public const string ChairRight =
		@"       " + "\n" +
		@"     │ " + "\n" +
		@"  ___│ " + "\n" +
		@" │   │ " + "\n" +
		@" ┴   ┴ ";
	public const string StopSign =
		@" ╭───╮ " + "\n" +
		@" │╲│╱│ " + "\n" +
		@" │╱│╲│ " + "\n" +
		@" ╰─╦─╯ " + "\n" +
		@"   ║   ";
	public const string StopSign2 =
		@"   ║   " + "\n" +
		@"   ║   " + "\n" +
		@"  ╱│╲  " + "\n" +
		@"       " + "\n" +
		@"       ";
	public const string Fridge =
		@"╔═════╗" + "\n" +
		@"║     ║" + "\n" +
		@"║     ║" + "\n" +
		@"║     ║" + "\n" +
		@"╚═════╝";
	public const string MicroWave =
		@"       " + "\n" +
		@"       " + "\n" +
		@"╔═════╗" + "\n" +
		@"║   │=║" + "\n" +
		@"╚═════╝";
	public const string LowCabnetWithHandle =
		@"       " + "\n" +
		@"╔═════╗" + "\n" +
		@"║●    ║" + "\n" +
		@"║     ║" + "\n" +
		@"╚═════╝";
	public const string HigherCabnetWithHandle =
		@"       " + "\n" +
		@"╔═════╗" + "\n" +
		@"║     ║" + "\n" +
		@"║●    ║" + "\n" +
		@"╚═════╝";
	public const string LowCabnetWithDraws =
		@"       " + "\n" +
		@"╔═════╗" + "\n" +
		@"║ === ║" + "\n" +
		@"║ === ║" + "\n" +
		@"╚═════╝";
	public const string LowerCabnetWithDraws =
		@"       " + "\n" +
		@"       " + "\n" +
		@"╔═════╗" + "\n" +
		@"║ === ║" + "\n" +
		@"╚═════╝";

	public const string TVLeft =
		@"╔══════" + "\n" +
		@"║      " + "\n" +
		@"║      " + "\n" +
		@"║      " + "\n" +
		@"╚═╦╦═══";
	public const string TVRight =
		@"══════╗" + "\n" +
		@"      ║" + "\n" +
		@"      ║" + "\n" +
		@"      ║" + "\n" +
		@"═══╦╦═╝";
	public const string TVDeskLeft =
		@"╔═╩╩═══" + "\n" +
		@"╚══════" + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"       ";
	public const string TVDeskRight =
		@"═══╩╩═╗" + "\n" +
		@"══════╝" + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"       ";
	public static readonly string PersonInBed1 =
		@"╭────│─" + '\n' +
		@"│ ╭──│ " + '\n' +
		@"│ ╰──│ " + '\n' +
		@"├────│─" + '\n' +
		@"└──────";
	public static readonly string PersonInBed2 =
		@"───────" + '\n' +
		@"       " + '\n' +
		@"       " + '\n' +
		@"───────" + '\n' +
		@"───────";
	public static readonly string PersonInBed3 =
		@"──────│" + '\n' +
		@"      │" + '\n' +
		@"      │" + '\n' +
		@"──────│" + '\n' +
		@"──────│";
	public const string StairsLeft =
		@"│──────" + "\n" +
		@"│──────" + "\n" +
		@"│──────" + "\n" +
		@"│──────" + "\n" +
		@"│──────";
	public const string StairsRight =
		@"──────│" + "\n" +
		@"──────│" + "\n" +
		@"──────│" + "\n" +
		@"──────│" + "\n" +
		@"──────│";
	public const string StairsDown =
		@"────── " + "\n" +
		@"││││││ " + "\n" +
		@"││││││ " + "\n" +
		@"││││││ " + "\n" +
		@"────── ";
	public const string StairsDown2 =
		@"────── " + "\n" +
		@"────── " + "\n" +
		@"────── " + "\n" +
		@"────── " + "\n" +
		@"────── ";
	public const string StairsDown4 =
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      ";
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
	public static readonly string NPC13 =
		@" /███\ " + '\n' +
		@"/│'_'│\" + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├─\─┤│" + '\n' +
		@" │_|_│ ";
	public static readonly string NPC14 =
		@" ╭√─√╮ " + '\n' +
		@" │^∞^│ " + '\n' +
		@"╚├───│╝" + '\n' +
		@" ├───┤ " + '\n' +
		@" │_|_│ ";
	public static readonly string NPC15 =
		@"╭∏─∏╮ Ʌ" + '\n' +
		@"│-_-│ │" + '\n' +
		@"│──│├─│" + '\n' +
		@"├───┤  " + '\n' +
		@"│_|_│  ";
	public static readonly string TrainConductorLeft =
		@"  ____ " + '\n' +
		@" ═│══│ " + '\n' +
		@"  │^ │ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ ";
	public static readonly string TrainConductorRight =
		@" ____  " + '\n' +
		@" │══│═ " + '\n' +
		@" │ ^│  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  ";

	#endregion

	#region Monsters
	public static readonly string Penguin =
		@"╭─────╮" + '\n' +
		@"│▪^_^▪│" + '\n' +
		@"│╷╭─╮╷│" + '\n' +
		@"├╯╰─╯╰┤" + '\n' +
		@"╰─╯‾╰─╯";
	public static readonly string WeirdMonster =
		@" │@_@│ " + '\n' +
		@"╭─╔══╗╮" + '\n' +
		@"╰╝│││╚╯" + '\n' +
		@"╭╗│││╔╮" + '\n' +
		@"╰─╚══╝╯";

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
	public const string MonsterBoxOpen =
		@"       " + "\n" +
		@" ╭─●─╮ " + "\n" +
		@" ├---┤ " + "\n" +
		@" ╰───╯ " + "\n" +
		@"       ";
	public const string XPBerries =
		@"   \   " + "\n" +
		@" ()(() " + "\n" +
		@"()()())" + "\n" +
		@" (()() " + "\n" +
		@"  ())  ";
	#endregion

	#region Letters + Symbols
	public static string BoxTop =    "╔═══════╗";
	public static string BoxSide =   "║";   //║ 5 tall
	public static string BoxBottom = "╚═══════╝";

	//WASD
	public static string[] W =
	{
		"▄   ▄",
		"█ ▄ █",
		"█▀ ▀█"
	};
	public static string[] A = 
	{
		" ▄▄▄ ",
		"█▄▄▄█",
		"█   █"
	};
	public static string[] S =
	{
		"▄▄▄▄▄",
		"█▄▄▄▄",
		"▄▄▄▄█"
	};
	public static string[] D =
	{
		"▄▄▄▄ ",
		"█   █",
		"█▄▄▄▀"
	};
	//ARROWS
	public static string[] UpArrow =
	{
		" ▄█▄ ",
		"▀ █ ▀",
		"  █  "
	};
	public static string[] LeftArrow =
	{
		"  ▄  ",
		"■█■■■",
		"  ▀  "
	};
	public static string[] DownArrow =
	{
		"  █  ",
		"▄ █ ▄",
		" ▀█▀ "
	};
	public static string[] RightArrow =
	{
		"  ▄  ",
		"■■■█■",
		"  ▀  "
	};
	//INTERACT
	public static string[] E =
	{
		"▄▄▄▄▄",
		"█▄▄▄ ",
		"█▄▄▄▄"
	};
	public static string[] Enter =
	{
		"  ▄ ▄",
		"▄█▄▄█",
		" ▀▄  "
		//"    ",
		//"◄──┘",
		//"    "
	};
	//Status
	public static string[] B =
	{
		"▄▄▄▄ ",
		"█▄▄▄▀",
		"█▄▄▄▀"
	};
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
