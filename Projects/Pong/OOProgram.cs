#define TRACE
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using CommandLineParser; // Original source code: https://github.com/wertrain/command-line-parser-cs (Version 0.1)

    ConsoleTraceListener myWriter = new GonsoleTraceListener();
    Trace.Listeners.Add(myWriter);
Trace.Write("OOProgram start.");
var rotation = Rotation.Vertical;
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
}
var (screen_w, screen_h) = OnScreen.init(screen_width, screen_height);
// int width = screen_w; // Console.WindowWidth;
// int height = screen_h; // Console.WindowHeight;
// Debug.Print($"speed ratio: {speed_ratio}");
// Debug.Print($"screen size is w(x axis): {screen_w} and h(y axis): {screen_h}.");
// Debug.Print($"option width is w(x axis): {screen_width}");
// Console.ReadKey();
// if (ar.Length > 2) speed_ratio = Convert.ToInt32(ar[2]);
var game = new Game(speed_ratio, screen_w, screen_h, paddle_width, rotation);
var imageCc = game.selfPadl.GetImage().renderImage();
Trace.Write($"selfPadl:{imageCc}");
game.selfPadl.Shift(-1);
Trace.Write($"selfPadl:{game.selfPadl.GetImage().renderImage()}");
game.Run();
// game.run();
public class Game {
	public PaddleScreen screen;
	// PaddleBase pdl; Paddle[] Paddles = new Paddle[2];
	public SelfPaddle selfPadl;
	public OpponentPaddle oppoPadl;
	// BitArray[] PaddleImages = new BitArray[2];
	public BitArray SelfOutputImage, OpponentOutputImage;
	public int PaddleWidth {get; init;}
	public Dictionary<System.ConsoleKey, Func<int>> manipDict = new();	
	public Rotation rotation {get; init;}
	public Game(int speed_ratio, int screen_w, int screen_h, int paddleWidth, Rotation rot){
				screen = new(screen_w, screen_h, rot == Rotation.Vertical ? true : false);
		selfPadl = new(width: paddleWidth, range: screen.PaddleRange, manipDict);
		oppoPadl = new(width: paddleWidth, range: screen.PaddleRange);
		if (rot == Rotation.Vertical){
			manipDict[ConsoleKey.UpArrow] = ()=>{ return selfPadl.Shift(-1); };
			manipDict[ConsoleKey.DownArrow] = ()=>{ return selfPadl.Shift(1); };
		}else{
			manipDict[ConsoleKey.LeftArrow] = ()=>{ return selfPadl.Shift(-1); };
			manipDict[ConsoleKey.RightArrow] = ()=>{ return selfPadl.Shift(1); };
		}
	}

	public void Run(){

		screen.DrawPaddle(selfPadl);
		screen.DrawPaddle(oppoPadl);
		
	TimeSpan delay = TimeSpan.FromMilliseconds(200);
	// pdl = new VPaddle(screen.w, paddle_width); // NestedRange(0..(width / 3), 0..width);
	Console.CancelKeyPress += delegate {
		Console.CursorVisible = true;
	};
	Console.CursorVisible = false; // hide cursor
	Console.Clear();
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
					screen.RedrawPaddle(selfPadl);
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

public class OpponentPaddle : Paddle {

	override public PaddleSide Side {get{return PaddleSide.Away;}}
    public OpponentPaddle(int width, int range) : base(width, range){

	}
}

public class SelfPaddle : Paddle {

	public Dictionary<System.ConsoleKey, Func<int>> ManipDict;
	override public PaddleSide Side {get{return PaddleSide.Home;}}
    public SelfPaddle(int width, int range, Dictionary<System.ConsoleKey, Func<int>> manipDict): base(width, range){
		ManipDict = manipDict;
	}
	public int ReactKey(System.ConsoleKey key) {
		return ManipDict[key]();
	}
}
public class Paddle : ScreenDrawItem
{
	virtual public PaddleSide Side {get;}
	public virtual char DispChar{get{return '+';}}
    public BitArray buffer { get; init; }
    public int Width
    {
        get
        {
            var trues = (from bool m in buffer
                         where m
                         select m).Count();
            return trues;
        }
    }
    public Paddle(int width, int range)
    {
		Debug.Assert(width > 0 && range > 0 && range > width);
        buffer = new BitArray(range);
        for (int i = 0; i < width; ++i)
            buffer[i] = true;
    }
	
/// <summary>Manipulate</summary>
/// <returns>0 if no reaction</returns> 
    public int Shift(int n)
    {
        return buffer.ClampShift(n);
    }
    public BitArray GetImage()
    {
        return buffer.Clone() as BitArray;
    }
}

class Options {
	[Option('s', "speed", Required =false, HelpText = "--speed 4")]
	public int speed { get; set;}
	[Option('w', "width", Required =false, HelpText = "--width 80")]
	public int width {get; set;}
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