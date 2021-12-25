using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using CommandLineParser; // Original source code: https://github.com/wertrain/command-line-parser-cs (Version 0.1)

//    ConsoleTraceListener myWriter = new GonsoleTraceListener();
//    Trace.Listeners.Add(myWriter);
Debug.Write("OOProgram start.");
var rotation = Rotation.Horizontal;
var clargs = Environment.GetCommandLineArgs();
var pArgs = clargs[1..];
var parseResult = Parser.Parse<Options>(pArgs);
var speed_ratio = 1;
var screen_width = 72;
var screen_height = 24;
var paddle_width = 8;
if (parseResult.Tag == ParserResultType.Parsed){
	speed_ratio = parseResult.Value.speed;
	screen_width = parseResult.Value.width;
	screen_height = parseResult.Value.height;
	paddle_width = parseResult.Value.paddle;
	rotation = parseResult.Value.rotation ? Rotation.Vertical : Rotation.Horizontal;
}
var (screen_w, screen_h) = OnScreen.init(screen_width, screen_height);
var game = new Game(speed_ratio, screen_w, screen_h, paddle_width, rotation);
game.Run();
public class Game {
	public PaddleScreen screen;
	public SelfPaddle selfPadl;
	public OpponentPaddle oppoPadl;
	public BitArray SelfOutputImage, OpponentOutputImage;
	// public int PaddleWidth {get; init;}
	public Dictionary<System.ConsoleKey, Func<int>> manipDict = new();	
	public Rotation rotation {get; init;}
	TimeSpan delay;
	public Game(int speed_ratio, int screen_w, int screen_h, int paddleWidth, Rotation rot){
		screen = new(screen_w, screen_h, rot == Rotation.Vertical ? true : false);
		selfPadl = new(range: screen.PaddleRange, width: paddleWidth, manipDict);
		oppoPadl = new(range: screen.PaddleRange, width: paddleWidth);
		if (rot == Rotation.Vertical){
			manipDict[ConsoleKey.UpArrow] = ()=>{ return selfPadl.Shift(-1); };
			manipDict[ConsoleKey.DownArrow] = ()=>{ return selfPadl.Shift(1); };
		}else{
			manipDict[ConsoleKey.LeftArrow] = ()=>{ return selfPadl.Shift(-1); };
			manipDict[ConsoleKey.RightArrow] = ()=>{ return selfPadl.Shift(1); };
		}
	delay = TimeSpan.FromMilliseconds(200);
	// pdl = new VPaddle(screen.w, paddle_width); // NestedRange(0..(width / 3), 0..width);
	Console.CancelKeyPress += delegate {
		Console.CursorVisible = true;
	};
	Console.CursorVisible = false; // hide cursor
	Console.Clear();
	Debug.WriteLine($"screen.isRotated={screen.isRotated}");
	Debug.WriteLine($"selfPadl range: 0..{selfPadl.Offset.Max + selfPadl.Width + 1}");
	Debug.Write($"screen.w={screen.w}, ");
	Debug.WriteLine($"screen.h={screen.h}");
	Debug.WriteLine($"screen.EndOfLines={screen.EndOfLines}");
		screen.drawWalls();
		screen.draw(selfPadl);
		screen.draw(oppoPadl);

	}

	public void Run(){
	while(true){
		int react;
		if (Console.KeyAvailable)
		{
			System.ConsoleKey key = Console.ReadKey(true).Key;
			if (key == ConsoleKey.Escape)
				goto exit;
			if (selfPadl.ManipDict.ContainsKey(key)) {
				react = selfPadl.ReactKey(key);
				if(react != 0){
					screen.draw(selfPadl);
				}
			}
			// else if (pdl.manipDict.ContainsKey(key)) moved = pdl.manipDict[key]() != 0;
			while(Console.KeyAvailable) // clear over input
				Console.ReadKey(true);
		}
		Thread.Sleep(delay);
	}
	exit:
	Console.CursorVisible = true;
	}
}



class Options {
	[Option('r', "rotation", Required =false, HelpText = "rotation default false(not rotated)")]
	public bool rotation { get; set;}
	[Option('s', "speed", Required =false, HelpText = "paddle speed default 4")]
	public int speed { get; set;}
	[Option('w', "width", Required =false, HelpText = "screen width default 64")]
	public int width {get; set;}
	[Option('h', "height", Required =false, HelpText = "screen height default 24")]
	public int height {get; set;}
	[Option('p', "paddle", Required =false, HelpText = "paddle width default 8")]
	public int paddle {get; set;}
}

public class GonsoleTraceListener : ConsoleTraceListener {
	public override void Write(string s){
		var (x,y) = Console.GetCursorPosition();
		Console.SetCursorPosition(0, 0);
		Trace.WriteLine(s);
		Console.ReadKey();
		Console.SetCursorPosition(x,y);
	}

}