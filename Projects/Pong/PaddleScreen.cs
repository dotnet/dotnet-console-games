using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class PaddleScreen : Screen {
	public int HomeToAway {get{
		return isRotated ? w : h;
	}}
	public int SideToSide {get{
		return isRotated ? h : w;
	}}
	public Range PaddleRange {get{
		return new Range(0, SideToSide);}} // 0 <= Paddle < PaddleRange
	public int AwayLineNum {get;init;}
	public const int HomeLineNum = 0;
	// public Wall[] Walls = new Wall[2];
	// public int[] WallLocations = new int[2];
	enum WallSide {Left, Right};
	SideWall[] SideWalls = new SideWall[2];
	record SideWall(WallSide Side, Wall wall);
	// public Paddle[] Paddles = new Paddle[2]; // 0: self, 1: opponent
	public Paddle[] Paddles = new Paddle[2];
	List<ScreenDrawItem> DrawItems = new();
	public PaddleScreen(int x, int y, bool rotate) : base(x,y,rotate) {
		AwayLineNum = this.EndOfLines; // Lines.Length - 1;
		// for (int i = 0; i < Walls.Length; ++i) Walls[i] = new Wall(1..EndOfLines);
		// WallLocations = {0, EndOfLines - 1};
		SideWalls[0] = new SideWall(WallSide.Left, new Wall(1..EndOfLines));
		SideWalls[1] = new SideWall(WallSide.Right, new Wall(1..EndOfLines));
	}
	public void draw(Paddle padl, bool replace_buffer = true) {
		var side = padl.Side;
		int n = (side == PaddleSide.Home) ? 0 : AwayLineNum;
		var image = padl.GetImage();
		if(Lines[n] == null)
			drawImage(n, image, padl.DispChar);
		else
			redrawImage(n, image, padl.DispChar);
		if(replace_buffer)
			Lines[n] = image;
	}
	/*
	public void DrawPaddle(Paddle paddle){
		BitArray image = paddle.GetImage();
		drawImage(paddle.Side == PaddleSide.Home ? HomeLineNum : AwayLineNum, image, paddle.DispChar);
	}
	public void RedrawPaddle(Paddle paddle){
		BitArray image = paddle.GetImage();
		redrawImage(paddle.Side == PaddleSide.Home ? HomeLineNum : AwayLineNum, image, paddle.DispChar);
	}
	*/
	public void drawWalls() {
		char c = isRotated ? '-' : '|';
		void drawVLine(int fromLeft) {
			for (int y = 0; y < HomeToAway; ++y){
				SetCursorPosition(fromLeft, y);
				Console.Write(c);
			}
		}
		drawVLine(0);
		drawVLine(SideToSide - 1);
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
			var buff = new BitArray(range.End.Value + 2);
			for (int i = 1; i < range.End.Value; ++i)
				buff[i] = true;
			return buff;
		}
	}
}