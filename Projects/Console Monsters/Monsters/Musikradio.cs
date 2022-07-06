using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Monsters.Monsters;

public class Musikradio : MonsterBase
{
	public Musikradio()
	{
		Name = "MusikRadio";
	}
	public override string[] Sprite => (
		"╭──♫♫♫──╮" + '\n' +
		"│♪ ^_^ ♪│" + '\n' +
		"╰───◌───╯").Split('\n');
}
