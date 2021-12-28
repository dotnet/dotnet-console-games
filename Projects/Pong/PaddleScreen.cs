using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class PaddleScreen : Screen {
	public int HomeToAway {get{
		return isRotated ? w - 1 : h - 1;
	}}
	public int SideToSide {get{
		return isRotated ? h - 1: w - 1;
	}}
	public Range PaddleRange {get{
		return new Range(0, SideToSide + 1);}} // 0 <= Paddle < PaddleRange
	public int AwayLineNum {get;init;}
	public const int HomeLineNum = 0;
	// public Wall[] Walls = new Wall[2];
	// public int[] WallLocations = new int[2];
	enum WallSide {Left, Right};
	SideWall[] SideWalls = new SideWall[2];
	record SideWall(WallSide Side, Wall wall);
	// public Paddle[] Paddles = new Paddle[2]; // 0: self, 1: opponent
	public Ball Ball;
	public Paddle[] Paddles = new Paddle[2];
	List<ScreenDrawItem> DrawItems = new();
	public PaddleScreen(int x, int y, bool rotate) : base(x,y,rotate) {
		AwayLineNum = this.EndOfLines; // Lines.Length - 1;
		// for (int i = 0; i < Walls.Length; ++i) Walls[i] = new Wall(1..EndOfLines);
		// WallLocations = {0, EndOfLines - 1};
		SideWalls[0] = new SideWall(WallSide.Left, new Wall(1..EndOfLines));
		SideWalls[1] = new SideWall(WallSide.Right, new Wall(1..EndOfLines));
		Ball = new(0..SideToSide, 0..HomeToAway, rotate);
	}
	public void draw(Paddle padl, bool replace_buffer = true) {
		var side = padl.Side;
		int n = (side == PaddleSide.Home) ? 0 : AwayLineNum;
		var image = padl.GetImage();
		Debug.WriteLine($"Pad bitimage Length:{image.Length}, Width: {padl.Width}, Offset Max: {padl.Offset.Max}.");
		if(Lines[n] == null)
			drawImage(n, image, padl.DispChar);
		else
			redrawImage(n, image, padl.DispChar);
		if(replace_buffer)
			Lines[n] = image;
	}
	public Offsets drawBall() {
		var offsets = Ball.offsets;
		var new_offsets = Ball.Move();
		if (new_offsets != offsets) {
			SetCursorPosition(offsets.x, offsets.y);
			Console.Write((char)CharCode.SPC);
			SetCursorPosition(new_offsets.x, new_offsets.y);
			Console.Write(Ball.DispChar);
		}
		return new_offsets;
	}
	public void drawWalls() {
		char c = isRotated ? '-' : '|';
		void drawVLine(int fromLeft) {
			Debug.WriteLine($"Walls y: from 1 to {HomeToAway}.");
			for (int y = 1; y <= HomeToAway; ++y){
				SetCursorPosition(fromLeft, y);
				Console.Write(c);
			}
		}
		drawVLine(0);
		Debug.WriteLine($"Walls x: 0 and {SideToSide}.");
		drawVLine(SideToSide);
	}

	public class Wall : ScreenDrawItem {
		DrawDirection drawDirection {get{return DrawDirection.Rotating;}}
		public char DispChar {get { return '.';}}
		public Range range{get; init;}
		bool isRotating{get; init;}
		public Wall(Range _range) {
			range = _range;
		}
		public BitArray GetImage() {
			var buff = new BitArray(range.End.Value);
			for (int i = 1; i < range.End.Value; ++i)
				buff[i] = true;
			return buff;
		}
	}
}