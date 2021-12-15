using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using CommandLineParser; // Original source code: https://github.com/wertrain/command-line-parser-cs (Version 0.1)

var clargs = Environment.GetCommandLineArgs();
var pArgs = clargs[1..];
var parseResult = Parser.Parse<Options>(pArgs);
var speed_ratio = 1;
var screen_width = 72;
var screen_height = 24;
var paddle_width = 8;
Paddle pdl = new Paddle(width=paddle_width, screen_height);
int n = pdl.Shift(8);
n = pdl.Shift(-9);
if (parseResult.Tag == ParserResultType.Parsed){
	speed_ratio = parseResult.Value.speed;
	screen_width = parseResult.Value.width;
}
var (screen_w, screen_h) = OnScreen.init(screen_width, screen_height);
// int width = screen_w; // Console.WindowWidth;
// int height = screen_h; // Console.WindowHeight;
Debug.Print("OOProgram start.");
Debug.Print($"speed ratio: {speed_ratio}");
Debug.Print($"screen size is w(x axis): {screen_w} and h(y axis): {screen_h}.");
Debug.Print($"option width is w(x axis): {screen_width}");
// if (ar.Length > 2) speed_ratio = Convert.ToInt32(ar[2]);
var game = new Game(speed_ratio, screen_w, screen_h);
// game.run();
public class Game {
	Screen scrn;
	PaddleBase pdl;
	Paddle[] Paddles = new();
	enum Side {Home = 0, Away = 1}

	public Game(int speed_ratio, int screen_w, int screen_h){
		for (int i = 0; i < 2; ++i)
			Paddles[i] = new Paddle(8, 24);
	TimeSpan delay = TimeSpan.FromMilliseconds(200);
	scrn = new Screen(screen_w, screen_h);
	pdl = new VPaddle(scrn.w, paddle_width); // NestedRange(0..(width / 3), 0..width);
	var pdl_barr = pdl.ToBitArray();
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
			Array.Copy(pdlArry[0], pdl.AtTop ? scrn.buffer[0] : scrn.buffer[scrn.h - 1], pdlArry[0].Length);
		
		var pdlStr = new string(pdlArry[0]);
		pdlStr = pdlStr.Replace('\0', ' ');
		 Console.SetCursorPosition(0, pdl.AtTop ? 0 : scrn.h - 1);
		 Console.Write(pdlStr);
		}
		Thread.Sleep(delay);
	}
	exit:
	Console.CursorVisible = true;
	}

}
	public class Paddle {
		public BitArray buffer {get; init;}
		public int Width {get; init;}
		public Paddle(int width, int range) {
			buffer = new BitArray(range);
			for (int i = 0; i < width; ++i)
				buffer[i] = true;
			Width = width;
		}
		public int Shift(int n) {
			return buffer.ClampShift(n);
		}
	}

class Options {
	[Option('s', "speed", Required =false, HelpText = "--speed 4")]
	public int speed { get; set;}
	[Option('w', "width", Required =false, HelpText = "--width 80")]
	public int width {get; set;}
}

