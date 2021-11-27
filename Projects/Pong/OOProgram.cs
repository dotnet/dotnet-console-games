using System;
using System.Diagnostics;
using System.Threading;

int width = Console.WindowWidth;
int height = Console.WindowHeight;
Screen screen = new();

float multiplier = 1.1f;
Random random = new();
TimeSpan delay = TimeSpan.FromMilliseconds(3);
TimeSpan enemyInputDelay = TimeSpan.FromMilliseconds(100);
int paddleSize = height / 4;
Stopwatch stopwatch = new();
Stopwatch enemyStopwatch = new();
int scoreA = 0;
int scoreB = 0;
Ball ball;
Player aPlayer = new(screen, screen.h / 4);
Player bPlayer = new(screen, screen.h / 4);
int paddleA = height / 3;
int paddleB = height / 3;

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
		float time = (float)stopwatch.Elapsed.TotalSeconds * 15;
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
				case ConsoleKey.Escape:
					Console.Clear();
					Console.CursorVisible = true;
					Console.Write("Pong was closed.");
					return;
			}
		}
		while (Console.KeyAvailable)
		{
			Console.ReadKey(true);
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

		#endregion

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

public class Screen {
	public int w {get; private set;}
	public int h {get; private set;}
	public Screen() {
		w = Console.WindowWidth;
		h = Console.WindowHeight;

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
    public int Score {get; set;} = 0;
    public Paddle paddle;
	public Player(Screen scr, int paddleWidth) {
		paddle = new(scr, paddleWidth);
	}
}
public class Paddle {
	public int pos {
		get {
			return _pos.Value;
		}
		private set {
			this._pos.set(value);
		}
	}
    Clamp _pos;
    int wid;
    public Paddle(Screen scr, int w) {
        if (w > scr.h) {
            throw new ArgumentOutOfRangeException("Width must not be more than _pos max!");
        }
        if (w <= 0) {
            throw new ArgumentOutOfRangeException("Width must be plus value!");
        }
        wid = w;
        _pos = new(scr.h - w);

    }

	public int Pos(){
		return _pos.Value;
	}
    public bool Up() {
        return _pos.Dec();
    }
    public bool Down() {
        return _pos.Inc();
    }

	public bool Hit(int p) {
		if (p >= Pos() && p < (Pos() + wid))
			return true;
		return false;
	}

}

public class Clamp
{
    public int Value {get; private set;}
    public int Max {get; private set;}

    public Clamp(int ma) {
        if (ma <= 0){
            throw new Exception("Max must not minus nor 0!");
        }
        Max = ma;
        Value = 0;
    }

    public bool Inc(){
        if ((0..Max).Contains(Value + 1)) { // Value < Max - 1) {
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