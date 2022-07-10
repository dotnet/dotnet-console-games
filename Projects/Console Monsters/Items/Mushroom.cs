using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Monsters.Items;

internal class Mushroom : ItemBase
{
	public override string? Name => "Mushroom";

	public override string? Description => "It is a mushroom";

	public override string Sprite =>
		@"   __  " + "\n" +
		@"  / `\ " + "\n" +
		@" (___:)" + "\n" +
		@"  """""""" " + "\n" +
		@"   ||  ";

	public static readonly Mushroom Instance = new();
}
