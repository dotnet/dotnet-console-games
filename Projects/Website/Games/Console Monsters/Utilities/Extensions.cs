//using Website.Games.Console_Monsters.Screens;

namespace Website.Games.Console_Monsters.Utilities;

public static class Extensions
{
	[System.Diagnostics.DebuggerHidden]
	public static T Get<T>(this T[,] array2D, (int Index1, int Index2) indeces) => array2D[indeces.Index1, indeces.Index2];

	[System.Diagnostics.DebuggerHidden]
	public static (T, T) Reverse<T>(this (T, T) tuple) => (tuple.Item2, tuple.Item1);
}
