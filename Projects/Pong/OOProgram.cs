using System;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
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
// game(width, height);
void game(int width, int height, int ball_speed = 15)
{
	Console.CancelKeyPress += delegate {
		Console.CursorVisible = true;
	};
float multiplier = 1.1f;
Random random = new();
TimeSpan delay = TimeSpan.FromMilliseconds(100);
TimeSpan enemyInputDelay = TimeSpan.FromMilliseconds(150);
int paddleSizeDenom = 4;
int paddleSize = screen_w / paddleSizeDenom;
Stopwatch stopwatch = new();
Stopwatch enemyStopwatch = new();
int scoreA = 0;
int scoreB = 0;
Ball ball;
int startPaddlePos = screen_h / 2 - paddleSize / 2;
int paddleA = startPaddlePos; // height / 3; // paddle position
int paddleB = startPaddlePos;
// Player playerA = new(0, new(paddleSize..screen_wh.H, startPaddlePos)); 
// var pAp_is_set = playerA.paddle.Set(startPaddlePos);
//if (pAp_is_set)
//	Debug.WriteLine($"playerA.paddle.pos is set as: {playerA.paddle.pos}");
//else
	// Debug.Print("playerA.paddle is not set!");

Console.Clear();
stopwatch.Restart();
enemyStopwatch.Restart();
Console.CursorVisible = false;
while (scoreA < 3 && scoreB < 3)
{
	ball = CreateNewBall();
	while (true)
	{
		#region Update Ball

		// Compute Time And New Ball Position
		float time = (float)stopwatch.Elapsed.TotalSeconds * ball_speed;
		var (X2, Y2) = (ball.X + (time * ball.dX), ball.Y + (time * ball.dY));

		// Collisions With Up/Down Walls
		if (Y2 < 0 || Y2 > height)
		{
			ball.dY = -ball.dY;
			Y2 = ball.Y + ball.dY;
		}

		// Collision With Paddle A
		if (Math.Min(ball.X, X2) <= 2 && 2 <= Math.Max(ball.X, X2))
		{
			int ballPathAtPaddleA = height - (int)GetLineValue(((ball.X, height - ball.Y), (X2, height - Y2)), 2);
			ballPathAtPaddleA = Math.Max(0, ballPathAtPaddleA);
			ballPathAtPaddleA = Math.Min(height - 1, ballPathAtPaddleA);
			if (paddleA <= ballPathAtPaddleA && ballPathAtPaddleA <= paddleA + paddleSize)
			{
				ball.dX = -ball.dX;
				ball.dX *= multiplier;
				ball.dY *= multiplier;
				X2 = ball.X + (time * ball.dX);
			}
		}

		// Collision With Paddle B
		if (Math.Min(ball.X, X2) <= width - 2 && width - 2 <= Math.Max(ball.X, X2))
		{
			int ballPathAtPaddleB = height - (int)GetLineValue(((ball.X, height - ball.Y), (X2, height - Y2)), width - 2);
			ballPathAtPaddleB = Math.Max(0, ballPathAtPaddleB);
			ballPathAtPaddleB = Math.Min(height - 1, ballPathAtPaddleB);
			if (paddleB <= ballPathAtPaddleB && ballPathAtPaddleB <= paddleB + paddleSize)
			{
				ball.dX = -ball.dX;
				ball.dX *= multiplier;
				ball.dY *= multiplier;
				X2 = ball.X + (time * ball.dX);
			}
		}

		// Collisions With Left/Right Walls
		if (X2 < 0)
		{
			scoreB++;
			break;
		}
		if (X2 > width)
		{
			scoreA++;
			break;
		}

		// Updating Ball Position
		Console.SetCursorPosition((int)ball.X, (int)ball.Y);
		Console.Write(' ');
		ball.X += time * ball.dX;
		ball.Y += time * ball.dY;
		Console.SetCursorPosition((int)ball.X, (int)ball.Y);
		Console.Write('O');

		#endregion

		#region Update Player Paddle

		if (Console.KeyAvailable)
		{
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.UpArrow: paddleA = Math.Max(paddleA - 1, 0); break;
				case ConsoleKey.DownArrow: paddleA = Math.Min(paddleA + 1, height - paddleSize - 1); break;
				case ConsoleKey.Escape: Console.Clear();
					Console.Write("Pong was closed.");
					Console.CursorVisible = true;
					return;
			}
		}
		while (Console.KeyAvailable)
		{
			Console.ReadKey(true); // Drop excessive key hits.
		}

		#endregion

		#region Update Computer Paddle

		if (enemyStopwatch.Elapsed > enemyInputDelay)
		{
			if (ball.Y < paddleB + (paddleSize / 2) && ball.dY < 0)
			{
				paddleB = Math.Max(paddleB - 1, 0);
			}
			else if (ball.Y > paddleB + (paddleSize / 2) && ball.dY > 0)
			{
				paddleB = Math.Min(paddleB + 1, height - paddleSize - 1);
			}
			enemyStopwatch.Restart();
		}

		#endregion

		#region Render Paddles

		for (int i = 0; i < height; i++)
		{
			Console.SetCursorPosition(2, i);
			Console.Write(paddleA <= i && i <= paddleA + paddleSize ? '█' : ' ');
			Console.SetCursorPosition(width - 2, i);
			Console.Write(paddleB <= i && i <= paddleB + paddleSize ? '█' : ' ');
		}

		#endregion+
		stopwatch.Restart();
		Thread.Sleep(delay);
	}
	Console.SetCursorPosition((int)ball.X, (int)ball.Y);
	Console.Write(' ');
}
Console.Clear();
if (scoreA > scoreB)
{
	Console.Write("You win.");
}
if (scoreA < scoreB)
{
	Console.Write("You lose.");
}
Console.CursorVisible = true;

Ball CreateNewBall()
{
	float randomFloat = (float)random.NextDouble() * 2f;
	float dx = Math.Max(randomFloat, 1f - randomFloat);
	float dy = 1f - dx;
	float x = width / 2;
	float y = height / 2;
	if (random.Next(2) == 0)
	{
		dx = -dx;
	}
	if (random.Next(2) == 0)
	{
		dy = -dy;
	}
	return new Ball
	{
		X = x,
		Y = y,
		dX = dx,
		dY = dy,
	};
}

float GetLineValue(((float X, float Y) A, (float X, float Y) B) line, float x)
{
	// order points from least to greatest X
	if (line.B.X < line.A.X)
	{
		var temp = line.B;
		line.B = line.A;
		line.A = temp;
	}
	// find the slope
	float slope = (line.B.Y - line.A.Y) / (line.B.X - line.A.X);
	// find the y-intercept
	float yIntercept = line.A.Y - line.A.X * slope;
	// find the function's value at parameter "x"
	return x * slope + yIntercept;
}

} // end game

class Options {
	[Option('s', "speed", Required =false, HelpText = "speed: 1")]
	public int speed { get; set;}
	[Option('w', "width", Required =false, HelpText = "width: 80")]
	public int width {get; set;}
}
public class Screen : OnScreen {
	public int w {get; private set;}
	public int h {get; private set;}
	public char [][] buffer {get; private set;} // [h][w]
	public Screen(int x = 80, int y = 25) {
		(w, h) = OnScreen.init(x, y);
		buffer = new char[h][];
		for(int i = 0; i < h; ++i) 
			buffer[i] = new char[w];
	}

	public Screen() {
		(w, h) = OnScreen.init();
	}

	public char [][] new_buffer() {
		var old_buffer = buffer;
		buffer = new char[h][];
		for(int i = 0; i < h; ++i) 
			buffer[i] = new char[w];
		return old_buffer;
	}
	void show(){

	}

}

public class Ball
{
	public float X;
	public float Y;
	public float dX;
	public float dY;
}
public class Player {
    public int score {get; set;}
    public PaddleBase paddle {get;}
}

record struct Dim2(int W, int H);
record struct Cood2(int X, int Y);
interface OnScreen {
	public static Cood2 dim;

	static (int, int) init(int x = 0, int y = 0) {
		var W = Console.WindowWidth;
		var H = Console.WindowHeight;
		dim = new(x > 0 ? x : W, y > 0 ? y : H);
		return (dim.X, dim.Y);
	}
}
public enum ScreenChar {O = '\u25CB',
 C = ' ', 
 B = '\u25A0', // Black square
}
interface Cood2Listable {
	List<Cood2> Cood2List();
}
interface HasDispChar {
	char DispChar();
}

interface Movable {
	void move_to(int x, int y); // move to (x, y) and redraw
	void move_by(int x, int y);
}

public enum Direction {V, H}
public enum HPos {Start, End}

interface IRender {
	char[][] render();
}

interface KeyManipulate { // key manipulate-able
	bool manipulate(System.ConsoleKey key);
}

interface IDrawOnScreen : OnScreen {
	void draw(char [][] buffer);
}

public class PaddleBase : NestedRange, HasDispChar, KeyManipulate {
	protected Screen scr {get; set;}
	public int speed_ratio {get; protected set;}
	public Dictionary<System.ConsoleKey, Func<int>> manipDict = new();
	virtual public char DispChar() {
		return '+';
	}
	public PaddleBase(Screen scr_, int quot, int speed_ratio_) {
		scr = scr_;
		inner = 0..(scr.w / quot);
		outer = 0..(scr.w);
		speed_ratio = speed_ratio_;
	}


	virtual public char [][] render() {
		char[][] buffer = new char[scr.h][];
		for (int i = 0; i < scr.h; ++i)
			buffer[i] = new char[scr.w];
		buffer[0][0] = DispChar(); // only at base point
		return buffer;
	}

	virtual public bool manipulate(System.ConsoleKey key) {
		return false;
	}

}

public class HPaddle : PaddleBase {

	public bool atTop {get; private set;}
	public HPaddle(Screen scr, int quot = 3, int speed_ratio_ = 1, bool atTop_ = true) : base(scr, quot, speed_ratio_) {
		atTop = atTop_;
		manipDict.Add(ConsoleKey.LeftArrow, () => shift(-speed_ratio));
		manipDict.Add(ConsoleKey.RightArrow, () => shift(speed_ratio));
	}

	override public char DispChar() {
		return (char)ScreenChar.B;
	}

	override public char [][] render() {
		char [][] buffer = new char[1][];
		buffer[0] = new char[scr.w];
		for (int x = inner.Start.Value; x < inner.End.Value; ++x) 
			buffer[0][x] = DispChar();
		return buffer;
	}

	override public bool manipulate(System.ConsoleKey key) {
		switch(key) {
			case ConsoleKey.LeftArrow: shift(-speed_ratio); return true;
			case ConsoleKey.RightArrow: shift(speed_ratio); return true;
		}
		return false;
	}

}
public class EscKeyPressedException : Exception
{
    public EscKeyPressedException()
    {
    }

    public EscKeyPressedException(string message)
        : base(message)
    {
    }

    public EscKeyPressedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
public class NestedRange {
	public Range inner {get; protected set;}
	public Range outer {get; protected set;}
	public NestedRange(Range _inner, Range _outer) {
		if (_inner.Start.Value < _outer.Start.Value || _inner.End.Value > _outer.End.Value)
            throw new ArgumentOutOfRangeException("Inner range out of outer range!");
		inner = _inner;
		outer = _outer;
	}

	public NestedRange() {
		inner = (0..1);
		outer = (0..1);
	}
	public int shift(int d) {
		if (d == 0)
			return 0;
		if (d > 0) {
			var s = outer.End.Value - inner.End.Value;
			if (s == 0)
				return 0;
			if (s < d)
				d = s;
		}
		else {
			var s = inner.Start.Value - outer.Start.Value;
			if (s == 0)
				return 0;
			if (s < -d)
				d = -s;
		}
		inner = (inner.Start.Value + d)..(inner.End.Value + d);
		return d;
	}

	public char[] render(char element){
		var cc = new char[outer.End.Value - outer.Start.Value];
		var nn = cc.AsSpan()[inner];
		for(int i = 0; i < nn.Length; ++i)
			nn[i] = element;
		return cc;
	}
	public BitArray ToBitArray(){
		var all = new BitArray(outer.End.Value - outer.Start.Value, false);
		var part = new BitArray(inner.End.Value - inner.Start.Value, true);
		return all.Or(part.RightShift(inner.Start.Value));
	}
	
}
static class RangeExtention
{
    public static bool Contains(this Range range, int value)
    {
        var start = range.Start.IsFromEnd ? (int.MaxValue - range.Start.Value) : range.Start.Value;
        var end = range.End.IsFromEnd ? (int.MaxValue - range.End.Value) : range.End.Value;
        if (start > end)
            throw new ArgumentOutOfRangeException(nameof(range));
        return start <= value && value < end;
    }
}

