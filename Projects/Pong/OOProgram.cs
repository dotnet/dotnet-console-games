using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

Debug.Print("OOProgram start.");
Environment.Exit(0);
var screen_wh = OnScreen.init();
Debug.Print($"screen size is w(x axis): {screen_wh.W} and h(y axis): {screen_wh.H}.");
int width = screen_wh.W; // Console.WindowWidth;
int height = screen_wh.H; // Console.WindowHeight;
// Screen screen = new();
// OnScreen.W = screen.w;
// OnScreen.H = screen.h;
float multiplier = 1.1f;
Random random = new();
TimeSpan delay = TimeSpan.FromMilliseconds(100);
TimeSpan enemyInputDelay = TimeSpan.FromMilliseconds(150);
int paddleSizeDenom = 4;
int paddleSize = screen_wh.H / paddleSizeDenom;
Stopwatch stopwatch = new();
Stopwatch enemyStopwatch = new();
int scoreA = 0;
int scoreB = 0;
Ball ball;
int startPaddlePos = screen_wh.H / 2 - paddleSize / 2;
int paddleA = startPaddlePos; // height / 3; // paddle position
int paddleB = startPaddlePos;
Player playerA = new(0, new(paddleSize..screen_wh.H, startPaddlePos)); 
Player playerB = new(0, new(paddleSize..screen_wh.H, startPaddlePos)); 
var pAp_is_set = playerA.paddle.Set(startPaddlePos);
if (pAp_is_set)
	Debug.WriteLine($"playerA.paddle.pos is set as: {playerA.paddle.pos}");
else
	Debug.Print("playerA.paddle is not set!");

Console.Clear();
stopwatch.Restart();
enemyStopwatch.Restart();
Console.CursorVisible = false;
int ball_speed = 15;
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
                             VTGB AN Ngb 6yh
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

public class Screen : OnScreen {
	public int w {get; init;}
	public int h {get; init;}
	public Screen() {
		OnScreen.init();
		OnScreen.W = w = Console.WindowWidth;
		OnScreen.H = h = Console.WindowHeight;
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
    public Paddle paddle {get;}
}
/* public class Paddle : PaddleBase {
	public Direction direction {get; init;}
    public Paddle(Range range, int start_pos = 0, Direction direc = Direction.V) : PaddleBase(range, start_pos) 
	{
		direction = direc;
	}
} */

record Dim2(int W, int H);
record Cood2(int X, int Y);
interface OnScreen {
	static int W; // width
	static int H; // height

	static Dim2 init() {
		W = Console.WindowWidth;
		H = Console.WindowHeight;
		return new Dim2(W, H);
	}
}
public enum ScreenChar {O = '\u25CB',
 C = ' ', 
 B = '\u25AE' }
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
public class Paddle : HasDispChar {
	public char DispChar() {
		return (char)ScreenChar.B;
	}
	public bool atLeft {get; init;} // Position is at left edge(if not, right edge).
	public int pos {
		get {
			return _pos.Value;
		}
		private set {
			this._pos.set(value);
		}
	}
    public Clamp _pos;
    public int size {get; init;}
	/// <summary>range Start is paddle size, range end is screen height.</sammary>
    public Paddle(Range range, int start_pos = 0, bool at_left = true) {
        if (range.Start.Value == 0)
        {
            throw new ArgumentOutOfRangeException("Paddle size must be more than 0!");
        }
        size = range.Start.Value;
        _pos = new(range.End.Value - range.Start.Value, start_pos);
		atLeft = at_left;
    }

	public void show(){
		Console.SetCursorPosition(atLeft ? 0 : OnScreen.W - 1, pos);
		Console.Write(ScreenChar.B);

	}
	public void move_to(int y) {
		var old_pos = pos;
		pos = y;
		

	}
    public bool Up() {
        return _pos.Dec();
    }
    public bool Down() {
        return _pos.Inc();
    }

	public bool Set(int n) {
		return _pos.set(n);
	}

	public bool Hit(int p) {
		if (p >= pos && p < (pos + size))
			return true;
		return false;
	}

}

public class Clamp
{
    public int Value {get; private set;}
    public int Max {get; private set;}

    public Clamp(int ma, int start = 0) {
        if (ma < 0){
            throw new ArgumentOutOfRangeException("Max must not minus!");
        }
        if (start < 0 || start >= ma)// !(0..Max).Contains(start)) 
            throw new ArgumentOutOfRangeException($"start value({start}) is not in [0..{Max}]. ");           
        Max = ma;
        Value = start;
    }

    public bool Inc(){
        if ((0..Max).Contains(Value + 1)) {
            Value += 1;
			return true;
		}
		else
			return false;
    }
    public bool Dec(){
        if (Value > 0) {
            Value -= 1;
			return true;
		}
		else
			return false;
    }
    public bool set(int nv){
        if ((0..Max).Contains(nv)) {
            Value  = nv;
			return true;
		}
		else
			return false;
    }

}

public class NestedRange {
	public Range inner {get; private set;}
	public Range outer {get; init;}
	public NestedRange(Range _inner, Range _outer) {
		if (_inner.Start.Value < _outer.Start.Value || _inner.End.Value > _outer.End.Value)
            throw new ArgumentOutOfRangeException("Inner range out of outer range!");
		inner = _inner;
		outer = _outer;
	}
	public int shift(int d) {
		if (d == 0)
			return 0;
		if (d > 0) {
			var s = outer.End.Value - inner.End.Value;
			if (s < d)
				d = s;
		}
		else {
			var s = inner.Start.Value - outer.Start.Value;
			if (s < -d)
				d = -s;
		}
		inner = (inner.Start.Value + d)..(inner.End.Value + d);
		return d;
	}

	public char[] render(char shape){
		var cc = new char[outer.End.Value - outer.Start.Value];
		var nn = cc[inner];
		for(int i = 0; i < nn.Length; ++i)
			nn[i] = shape;
		return cc;
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