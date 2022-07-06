using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Monsters.Monsters;

public class Chopper:MonsterBase
{
	public Chopper()
	{
		Name = "Chopper";
	}
	public override string[] Sprite => (
		   "   ╔═════╗   " + '\n' +
		   "╷╷ ║     ║ ╷╷" + '\n' +
		   "└┴─╢  ╳  ╟─┴┘" + '\n' +
		   " ══╩╤═══╤╩══ " + '\n' +
		   "   │ ∏ ∏ │   " + '\n' +
		   "   │  ♥  │   " + '\n' +
	       "  ╭ ───── ╮  " + '\n' +
	       "  │       │  " + '\n' +
		   " ╭╭╮     ╭╭╮ " + '\n' +
		   "    │───│    " + '\n' +
	   	   "  │││   │││  " + '\n' +
		   "  ╭╭╮   ╭╭╮  ").Split('\n');

}
