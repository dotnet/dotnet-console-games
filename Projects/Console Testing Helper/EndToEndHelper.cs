using System.Diagnostics;
using System.Reflection;

namespace ConsoleTestingHelper;
public static class EndToEndHelper
{
	//TODO https://www.codeproject.com/Articles/170017/Solving-Problems-of-Monitoring-Standard-Output-and
	public static Process Run(Type executableClass)
	{
		var bin = Assembly.GetAssembly(executableClass)!.Location
			.Replace(".Test", "")
			.Replace(".dll", ".exe");

		var processInfo = new ProcessStartInfo
		{
			FileName = bin,
			WindowStyle = ProcessWindowStyle.Hidden,
			CreateNoWindow = true,
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			RedirectStandardInput = true,

		};
		var app = Process.Start(processInfo)
			?? throw new NullReferenceException();
		
		return app;
	}
}
