using System;

namespace Website;

public class BlazorOperatingSystem
{
	/// <summary>
	/// Returns true. Some members of <see cref="Console"/> only work
	/// on Windows such as <see cref="Console.WindowWidth"/>, but even though this
	/// is blazor and not necessarily on Windows, this wrapper contains implementations
	/// for those Windows-only members.
	/// </summary>
	/// <returns>true</returns>
	public static bool IsWindows() => true;
}
