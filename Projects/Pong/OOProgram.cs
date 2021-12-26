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
var refresh_delay = 200;
if (parseResult.Tag == ParserResultType.Parsed){
	speed_ratio = parseResult.Value.speed;
	screen_width = parseResult.Value.width;
	screen_height = parseResult.Value.height;
	paddle_width = parseResult.Value.paddle;
	rotation = parseResult.Value.rotation ? Rotation.Vertical : Rotation.Horizontal;
	if(parseResult.Value.delay > 0)
		refresh_delay = parseResult.Value.delay;

}
var (screen_w, screen_h) = OnScreen.init(screen_width, screen_height);
var game = new Game(speed_ratio, screen_w, screen_h, paddle_width, rotation, refresh_delay);
game.Run();
public class Game {
	// public Ball ball;
	public PaddleScreen screen;
	public SelfPaddle selfPadl;
	public OpponentPaddle oppoPadl;
	public BitArray SelfOutputImage, OpponentOutputImage;
	// public int PaddleWidth {get; init;}
	public Dictionary<System.ConsoleKey, Func<int>> manipDict = new();	
	public Rotation rotation {get; init;}
	TimeSpan delay;
	Stopwatch opponentStopwatch = new();
	TimeSpan opponentInputDelay = TimeSpan.FromMilliseconds(100);
	public Game(int speed_ratio, int screen_w, int screen_h, int paddleWidth, Rotation rot, int refresh_delay){
		screen = new(screen_w, screen_h, rot == Rotation.Vertical ? true : false);
		selfPadl = new(range: screen.PaddleRange, width: paddleWidth, manipDict);
		oppoPadl = new(range: screen.PaddleRange, width: paddleWidth);
		screen.Paddles[0] = selfPadl;
		screen.Paddles[1] = oppoPadl;
		if (rot == Rotation.Vertical){
			manipDict[ConsoleKey.UpArrow] = ()=>{ return selfPadl.Shift(-1); };
			manipDict[ConsoleKey.DownArrow] = ()=>{ return selfPadl.Shift(1); };
		}else{
			manipDict[ConsoleKey.LeftArrow] = ()=>{ return selfPadl.Shift(-1); };
			manipDict[ConsoleKey.RightArrow] = ()=>{ return selfPadl.Shift(1); };
		}
	delay = TimeSpan.FromMilliseconds(refresh_delay);
	// pdl = new VPaddle(screen.w, paddle_width); // NestedRange(0..(width / 3), 0..width);
	Console.CancelKeyPress += delegate {
		Console.CursorVisible = true;
	};
	Console.CursorVisible = false; // hide cursor
	Console.Clear();
	Debug.WriteLine($"screen.isRotated={screen.isRotated}");
	Debug.WriteLine($"selfPadl range: 0..{selfPadl.Offset.Max + selfPadl.Width + 1}");
	Debug.Write($"screen.SideToSide={screen.SideToSide}, ");
	Debug.WriteLine($"screen.HomeToAway={screen.HomeToAway}");
	Debug.WriteLine($"screen.EndOfLines={screen.EndOfLines}");
		screen.drawWalls();
		screen.draw(selfPadl);
		screen.draw(oppoPadl);

	}

	public void Run(){
		opponentStopwatch.Restart();
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
		var offsets = screen.drawBall(); // screen.Ball.Move();
		if(offsets.y == 1){
			var selfPadlStart = selfPadl.Offset.Value; 
			var selfPadlEnd = selfPadlStart + selfPadl.Width;
			if(!(selfPadlStart..selfPadlEnd).Contains(offsets.x)) {
				screen.SetCursorPosition(0, 0);
				Console.Write("Your paddle failed to hit the ball!: Hit any key..");
				Console.ReadKey();
				goto exit;
			}
		}
		else if (offsets.y == screen.HomeToAway - 2){
			var PadlStart = oppoPadl.Offset.Value; 
			var PadlEnd = PadlStart + oppoPadl.Width;
			if(!(PadlStart..PadlEnd).Contains(offsets.x)) {
				screen.SetCursorPosition(0, 0);
				Console.Write("Opponent's paddle failed to hit the ball!: Hit any key..");
				Console.ReadKey();
				goto exit;
			}

		}
		if(opponentStopwatch.Elapsed > opponentInputDelay){
			var diff = screen.Ball.offsets.x - oppoPadl.Offset.Value;
			if (Math.Abs(diff) > 2){
				oppoPadl.Shift(diff);
				screen.draw(oppoPadl);
			}
			opponentStopwatch.Restart();
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
	[Option('d', "delay", Required =false, HelpText = "ball refresh rate. default 200")]
	public int delay {get; set;}
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