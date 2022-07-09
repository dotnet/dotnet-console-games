﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Monsters.Moves;

public class Tackle : MoveBase
{
	public Tackle()
	{
		Name = "Tackle";
		BaseDamge = 40;
		FinalDamage = (((2 * AttackingMonster.Level / 5 + 2) * BaseDamge * AttackingMonster.AttackStat / DefendingMonster.DefenseStat) / 50 + 2) * BattleRandom.Next(85, 101) / 100;
		EnergyTaken = 10; // Temp
	}
}
