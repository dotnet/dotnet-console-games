using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Monsters.Moves;

public class Scratch : MoveBase
{
	public Scratch()
	{
		Name = "Scratch";
		BaseDamge = 40;
		FinalDamage = (((2 * PlayerMonster.Level / 5 + 2) * BaseDamge * PlayerMonster.AttackStat / OpponentMonster.DefenseStat) / 50 + 2) * BattleRandom.Next(85, 101) / 100;
		EnergyTaken = 10; // Temp
	}
}
