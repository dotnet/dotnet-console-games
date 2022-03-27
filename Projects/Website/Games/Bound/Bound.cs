using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Website.Games.Bound;

public class Bound
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
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

		await Console.WriteLine("This game is a work in progress.");
		await Console.WriteLine("It is not yet complete.");
		await Console.WriteLine("Press [enter] to continue...");
	GetEnterOrEscape:
		Console.CursorVisible = false;
		switch ((await Console.ReadKey(true)).Key)
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
		await Console.Clear();
		while (true)
		{
			level = levels[levelIndex];
			Console.CursorVisible = false;
			await Console.SetCursorPosition(0, 0);
			await Console.WriteLine();
			await Console.WriteLine("  Bound");
			await Console.WriteLine();
			await Console.WriteLine($"  Lives: {lives}   ");
			await Console.WriteLine($"  Level: {levelIndex}");
			int mapTop = Console.CursorTop;
			await Console.Write(level.Frames[frame].Map);
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
			await Console.SetCursorPosition(position.Left, position.Top + mapTop);
			await Console.Write(GetPlayerChar());
			DateTime start = DateTime.Now;
			while (DateTime.Now - start < level.Frames[frame].Delay)
			{
				while (await Console.KeyAvailable())
				{
					switch ((await Console.ReadKey(true)).Key)
					{
						case ConsoleKey.UpArrow:
							lastMovementKey = ConsoleKey.UpArrow;
							if (map[position.Top - 1][position.Left] is ' ' or '@' or '#')
							{
								lastMovementKey = ConsoleKey.UpArrow;
								await Console.SetCursorPosition(position.Left, position.Top + mapTop);
								await Console.Write(' ');
								position.Top--;
							}
							break;
						case ConsoleKey.DownArrow:
							lastMovementKey = ConsoleKey.DownArrow;
							if (map[position.Top + 1][position.Left] is ' ' or '@' or '#')
							{
								await Console.SetCursorPosition(position.Left, position.Top + mapTop);
								await Console.Write(' ');
								position.Top++;
							}
							break;
						case ConsoleKey.LeftArrow:
							lastMovementKey = ConsoleKey.LeftArrow;
							if (map[position.Top][position.Left - 1] is ' ' or '@' or '#')
							{
								await Console.SetCursorPosition(position.Left, position.Top + mapTop);
								await Console.Write(' ');
								position.Left--;
							}
							break;
						case ConsoleKey.RightArrow:
							lastMovementKey = ConsoleKey.RightArrow;
							if (map[position.Top][position.Left + 1] is ' ' or '@' or '#')
							{
								await Console.SetCursorPosition(position.Left, position.Top + mapTop);
								await Console.Write(' ');
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
							await Console.Clear();
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
						await Console.SetCursorPosition(position.Left, position.Top + mapTop);
						await Console.Write(GetPlayerChar());
					}
					else
					{
						await Console.SetCursorPosition(position.Left, position.Top + mapTop);
						await Console.Write(GetPlayerChar());
					}
				}
			}
			frame = (frame + 1) % level.Frames.Length;
		}
	YouWin:
		await Console.Clear();
		await Console.WriteLine("You Win!");
		await Console.Refresh();
		return;
	YouLose:
		await Console.Clear();
		await Console.WriteLine("You Lose!");
		await Console.Refresh();
		return;


		char GetPlayerChar() =>
			lastMovementKey switch
			{
				ConsoleKey.UpArrow => '^',
				ConsoleKey.DownArrow => 'v',
				ConsoleKey.LeftArrow => '<',
				ConsoleKey.RightArrow => '>',
				_ => throw new NotImplementedException(),
			};
	}
}
