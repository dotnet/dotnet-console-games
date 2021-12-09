using System;
using System.Diagnostics;
using System.Threading;
using CommandLineParser; // Original source code: https://github.com/wertrain/command-line-parser-cs (Version 0.1)

var clargs = Environment.GetCommandLineArgs();
var pArgs = clargs[1..];
var parseResult = Parser.Parse<Options>(pArgs);
var speed_ratio = 1;
var screen_width = 30;
if (parseResult.Tag == ParserResultType.Parsed){
	speed_ratio = parseResult.Value.speed;
	screen_width = parseResult.Value.width;
}
var (screen_w, screen_h) = OnScreen.init();
int width = screen_w; // Console.WindowWidth;
int height = screen_h; // Console.WindowHeight;
Debug.Print("OOProgram start.");
Debug.Print($"speed ratio: {speed_ratio}");
Debug.Print($"screen size is w(x axis): {screen_w} and h(y axis): {screen_h}.");
Debug.Print($"option width is w(x axis): {screen_width}");
// if (ar.Length > 2) speed_ratio = Convert.ToInt32(ar[2]);
mock(speed_ratio, screen_width);
void mock(int speed_ratio, int screen_width){
	TimeSpan delay = TimeSpan.FromMilliseconds(200);
	var scrn = new Screen();
	scrn = new Screen(screen_width, scrn.w);
	var pdl = new HPaddle(scrn); // NestedRange(0..(width / 3), 0..width);
	Console.CancelKeyPress += delegate {
		Console.CursorVisible = true;
	};
	Console.CursorVisible = false; // hide cursor
	Console.Clear();
	while(true){
		bool moved = false;
		if (Console.KeyAvailable)
		{
			var key = Console.ReadKey(true).Key;
			if (key == ConsoleKey.Escape)
				goto exit;
			if (pdl.manipDict.ContainsKey(key)) {
				var old_bits = pdl.ToBitArray();
				pdl.manipDict[key](); // execute key proc.
				var new_bits = pdl.ToBitArray();
				var bits_diff = old_bits.Xor(new_bits);
				var disappeared_bits = bits_diff.And(old_bits);
				var appeared_bits = bits_diff.And(new_bits);
			}
			moved = pdl.manipulate(key);
			// else if (pdl.manipDict.ContainsKey(key)) moved = pdl.manipDict[key]() != 0;
			while(Console.KeyAvailable) // clear over input
				Console.ReadKey(true);
		}
		if (moved) {
			var pdlArry = pdl.render();
			var old_buffer = scrn.new_buffer();
			Array.Copy(pdlArry[0], pdl.atTop ? scrn.buffer[0] : scrn.buffer[scrn.h - 1], pdlArry[0].Length);
		
		var pdlStr = new string(pdlArry[0]);
		pdlStr = pdlStr.Replace('\0', ' ');
		 Console.SetCursorPosition(0, pdl.atTop ? 0 : scrn.h - 1);
		 Console.Write(pdlStr);
		}
		Thread.Sleep(delay);
	}
	exit:
	Console.CursorVisible = true;
}

class Options {
	[Option('s', "speed", Required =false, HelpText = "speed: 1")]
	public int speed { get; set;}
	[Option('w', "width", Required =false, HelpText = "width: 80")]
	public int width {get; set;}
}