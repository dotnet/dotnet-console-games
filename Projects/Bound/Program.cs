using System;
using System.Text.RegularExpressions;

((int Left, int Top) StartPosition, (string Map, TimeSpan Delay)[])[] levels =
{
	#region level 0
	((15, 16),
	new (string Map, TimeSpan Delay)[]
	{
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.1)),
	}),
	#endregion
	#region level 1
	((15, 16),
	new (string Map, TimeSpan Delay)[]
	{
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
	}),
	#endregion
	#region level 2
	((15, 16),
	new (string Map, TimeSpan Delay)[]
	{
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.4)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.4)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.4)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.4)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.4)),
	}),
	#endregion
	#region level 3
	((15, 16),
	new (string Map, TimeSpan Delay)[]
	{
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(1)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(1)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#######║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║       ║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║#######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.2)),
	}),
	#endregion
	#region level 4
	((4, 8),
	new (string Map, TimeSpan Delay)[]
	{
		(@"
      ╔═════════╗
      ║   ###   ║
      ║   ###   ║
      ║   ╔═╗###║
      ╚═══╝ ║###║
      ╔═══╗ ║###║
      ║ @ ╚═╝###║
      ║   ###   ║
      ║   ###   ║
      ╚═════════╝", TimeSpan.FromSeconds(1)),
		(@"
      ╔═════════╗
      ║         ║
      ║         ║
      ║   ╔═╗###║
      ╚═══╝ ║###║
      ╔═══╗ ║###║
      ║ @ ╚═╝###║
      ║   ###   ║
      ║   ###   ║
      ╚═════════╝", TimeSpan.FromSeconds(1.33)),
		(@"
      ╔═════════╗
      ║   ###   ║
      ║   ###   ║
      ║   ╔═╗   ║
      ╚═══╝ ║   ║
      ╔═══╗ ║   ║
      ║ @ ╚═╝   ║
      ║   ###   ║
      ║   ###   ║
      ╚═════════╝", TimeSpan.FromSeconds(1.77)),
		(@"
      ╔═════════╗
      ║   ###   ║
      ║   ###   ║
      ║   ╔═╗###║
      ╚═══╝ ║###║
      ╔═══╗ ║###║
      ║ @ ╚═╝###║
      ║         ║
      ║         ║
      ╚═════════╝", TimeSpan.FromSeconds(1.33)),
	}),
	#endregion
	#region level 5
	((12, 16),
	new (string Map, TimeSpan Delay)[]
	{
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║###### ║
            ║##### #║
            ║#### ##║
            ║### ###║
            ║## ####║
            ║# #####║
            ║ ######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(1)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║###### ║
            ║#####  ║
            ║####  #║
            ║###  ##║
            ║##  ###║
            ║#  ####║
            ║  #####║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.75)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║###### ║
            ║##### #║
            ║#### ##║
            ║### ###║
            ║## ####║
            ║# #####║
            ║ ######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(1)),
		(@"
            ╔═══════╗
            ║   @   ║
            ║       ║
            ║#####  ║
            ║####  #║
            ║###  ##║
            ║##  ###║
            ║#  ####║
            ║  #####║
            ║ ######║
            ║       ║
            ║       ║
            ╚═══════╝", TimeSpan.FromSeconds(.75)),
	}),
	#endregion
	#region level 6
	((14, 19),
	new (string Map, TimeSpan Delay)[]
	{
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║#################║
         ║#################║
         ║#################║
         ║#######   #######║
         ║#######   #######║
         ║#######   #######║
         ║#################║
         ║#################║
         ║##   ############║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║#######   #######║
         ║#######   #######║
         ║#######   #######║
         ║##   ############║
         ║##   ############║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║########   ######║
         ║########   ######║
         ║##   ###   ######║
         ║##   ############║
         ║##   ############║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║#########   #####║
         ║##   ####   #####║
         ║##   ####   #####║
         ║##   ############║
         ║#################║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║##   #####   ####║
         ║##   #####   ####║
         ║##   #####   ####║
         ║#################║
         ║#################║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║#################║
         ║#################║
         ║#################║
         ║##   ############║
         ║##   ######   ###║
         ║##   ######   ###║
         ║###########   ###║
         ║#################║
         ║#################║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║#################║
         ║#################║
         ║##   ############║
         ║##   ############║
         ║##   #######   ##║
         ║############   ##║
         ║############   ##║
         ║#################║
         ║#################║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║#################║
         ║##   ############║
         ║##   ############║
         ║##   #######   ##║
         ║############   ##║
         ║############   ##║
         ║#################║
         ║#################║
         ║#################║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║#################║
         ║###   ###########║
         ║###   ######   ##║
         ║###   ######   ##║
         ║############   ##║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║#################║
         ║####   #####   ##║
         ║####   #####   ##║
         ║####   #####   ##║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║############   ##║
         ║#####   ####   ##║
         ║#####   ####   ##║
         ║#####   #########║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║############   ##║
         ║######   ###   ##║
         ║######   ########║
         ║######   ########║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║############   ##║
         ║#######   #######║
         ║#######   #######║
         ║#######   #######║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
		(@"
         ╔═════════════════╗
         ║         @       ║
         ║                 ║
         ║#################║
         ║#################║
         ║#######   #######║
         ║#######   #######║
         ║#######   #######║
         ║#################║
         ║#################║
         ║#################║
         ║#################║
         ║                 ║
         ║                 ║
         ╚═════════════════╝", TimeSpan.FromSeconds(.3)),
	}),
	#endregion
};

Console.WriteLine("This game is a work in progress.");
Console.WriteLine("It is not yet complete.");
Console.WriteLine("Press [enter] to continue...");
GetEnterOrEscape:
Console.CursorVisible = false;
switch (Console.ReadKey(true).Key)
{
	case ConsoleKey.Enter: break;
	case ConsoleKey.Escape: return;
	default: goto GetEnterOrEscape;
}
int lives = 100;
int frame = 0;
int levelIndex = 0;
((int Left, int Top) StartPosition, (string Map, TimeSpan Delay)[] Frames) level = levels[levelIndex];
(int Top, int Left) position = level.StartPosition;
ConsoleKey lastMovementKey = ConsoleKey.UpArrow;
Console.CursorVisible = false;
Console.Clear();
while (true)
{
	level = levels[levelIndex];
	Console.CursorVisible = false;
	Console.SetCursorPosition(0, 0);
	Console.WriteLine();
	Console.WriteLine("  Bound");
	Console.WriteLine();
	Console.WriteLine($"  Lives: {lives}   ");
	Console.WriteLine($"  Level: {levelIndex}");
	int mapTop = Console.CursorTop;
	Console.Write(level.Frames[frame].Map);
	string[] map = Regex.Split(level.Frames[frame].Map, @"\n|\r\n");
	if (map[position.Top][position.Left] is '#')
	{
		lastMovementKey = ConsoleKey.UpArrow;
		position = levels[levelIndex].StartPosition;
		lives--;
		if (lives <= 0)
		{
			goto YouLose;
		}
	}
	Console.SetCursorPosition(position.Left, position.Top + mapTop);
	Console.Write(GetPlayerChar());
	DateTime start = DateTime.Now;
	while (DateTime.Now - start < level.Frames[frame].Delay)
	{
		while (Console.KeyAvailable)
		{
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.UpArrow:
					lastMovementKey = ConsoleKey.UpArrow;
					if (map[position.Top - 1][position.Left] is ' ' or '@' or '#')
					{
						lastMovementKey = ConsoleKey.UpArrow;
						Console.SetCursorPosition(position.Left, position.Top + mapTop);
						Console.Write(' ');
						position.Top--;
					}
					break;
				case ConsoleKey.DownArrow:
					lastMovementKey = ConsoleKey.DownArrow;
					if (map[position.Top + 1][position.Left] is ' ' or '@' or '#')
					{
						Console.SetCursorPosition(position.Left, position.Top + mapTop);
						Console.Write(' ');
						position.Top++;
					}
					break;
				case ConsoleKey.LeftArrow:
					lastMovementKey = ConsoleKey.LeftArrow;
					if (map[position.Top][position.Left - 1] is ' ' or '@' or '#')
					{
						Console.SetCursorPosition(position.Left, position.Top + mapTop);
						Console.Write(' ');
						position.Left--;
					}
					break;
				case ConsoleKey.RightArrow:
					lastMovementKey = ConsoleKey.RightArrow;
					if (map[position.Top][position.Left + 1] is ' ' or '@' or '#')
					{
						Console.SetCursorPosition(position.Left, position.Top + mapTop);
						Console.Write(' ');
						position.Left++;
					}
					break;
			}
			if (map[position.Top][position.Left] is '@')
			{
				frame = 0;
				levelIndex++;
				if (levelIndex >= levels.Length)
				{
					goto YouWin;
				}
				else
				{
					position = levels[levelIndex].StartPosition;
					Console.Clear();
				}
			}
			else if (map[position.Top][position.Left] is '#')
			{
				lastMovementKey = ConsoleKey.UpArrow;
				position = levels[levelIndex].StartPosition;
				lives--;
				if (lives <= 0)
				{
					goto YouLose;
				}
				Console.SetCursorPosition(position.Left, position.Top + mapTop);
				Console.Write(GetPlayerChar());
			}
			else
			{
				Console.SetCursorPosition(position.Left, position.Top + mapTop);
				Console.Write(GetPlayerChar());
			}
		}
	}
	frame = (frame + 1) % level.Frames.Length;
}
YouWin:
Console.Clear();
Console.WriteLine("You Win!");
return;
YouLose:
Console.Clear();
Console.WriteLine("You Lose!");
return;


char GetPlayerChar() =>
	lastMovementKey switch
	{
		ConsoleKey.UpArrow    => '^',
		ConsoleKey.DownArrow  => 'v',
		ConsoleKey.LeftArrow  => '<',
		ConsoleKey.RightArrow => '>',
		_ => throw new NotImplementedException(),
	};
