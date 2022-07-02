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

namespace Website.Games.Console_Monsters.Bases;

public abstract class NPCBase : InteractableBase
{
	public abstract string? Name { get; }

	public string[]? Dialogue { get; set; }

	public string Sprite { get; set; } = Sprites.Error;

	public string[]? SpriteAnimation { get; set; }
}
