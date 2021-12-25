using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class PaddleScreen : Screen {
	public Range PaddleRange {get{
		return new Range(0, isRotated ? h : w);}} // 0 <= Paddle < PaddleRange
	public int AwayLineNum {get;init;}
	public const int HomeLineNum = 0;
	// public Wall[] Walls = new Wall[2];
	// public int[] WallLocations = new int[2];
	enum WallSide {Left, Right};
	SideWall[] SideWalls = new SideWall[2];
	record SideWall(WallSide Side, Wall wall);
	// public Paddle[] Paddles = new Paddle[2]; // 0: self, 1: opponent
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
		// enum Pos {NEAR, FAR}
		void drawV(int x){
			for (int y = 1; y < h; ++y){
				Console.SetCursorPosition(x, y);
				Console.Write('|');
			}
		}
		void drawH(int y){
			for (int x = 1; x < w; ++x){
				Console.SetCursorPosition(x, y);
				Console.Write('-');
			}
		}
		// if (isRotated) // walls are horizontal
		Action<int> draw = isRotated ? (x)=> drawH(x) : (x)=> drawV(x);
		draw(0);
		draw((isRotated ? h : w) - 1);
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
enum DrawDirection {Normal, Rotating}
public record struct Dimention ( int x, int y);
public class Screen : OnScreen {
	public enum CharCode {ESC = '\x1b', SPC = '\x20', VBAR = '|', HBAR = '-', DOT = '.'}
	public Dictionary<System.ConsoleKey, Func<int>> KeyManipDict;
	public const char BlankChar = (char)CharCode.SPC;
	public Action<int, BitArray, char> DrawImage;
	public Action<int, BitArray, char> RedrawImage; // (Line, this_array, new_array, c='+')
	public virtual bool isRotated {get; init;} // 90 degree
	public int w {get; init;}
	public int h {get; init;}
	public Dimention dim {get; init;}
	public int EndOfLines {get{return Lines.Length - 1;}}
	public BitArray[] Lines {get; private set;} // [h][w]
	public Screen(int x = 80, int y = 24, bool rotate = false) {
		(w, h) = OnScreen.init(x, y);
		dim = new(w, h);
		isRotated = rotate;
		Lines = new BitArray[isRotated ? w : h];
        // for(int i = 0; i < (rotate ? w :h); ++i) buffer[i] = new BitArray(rotate ? h :w);
        RedrawImage = isRotated ? (line, new_buff, c) =>
        {
            var ad = Lines[line].ToAddedDeleted(new_buff);
            VPutCasBitArray(line, c, ad.Added);
            VPutCasBitArray(line, BlankChar, ad.Deleted);
        }
        : (line, new_buff, c) =>
        {
            var ad = Lines[line].ToAddedDeleted(new_buff);
            HPutCasBitArray(line, c, ad.Added);
            HPutCasBitArray(line, BlankChar, ad.Deleted);
        };
        DrawImage = isRotated ? (line, buff, c) =>
            VPutCasBitArray(line, c, buff)
        : (line, buff, c) =>
            HPutCasBitArray(line, c, buff);

	}

	public Screen() {
		(w, h) = OnScreen.init();
	}

	public BitArray [] new_buffer() {
		var old_buffer = Lines;
		Lines = new BitArray[isRotated ? w : h];
		// for(int i = 0; i < h; ++i) buffer[i] = new char[w];
		return old_buffer;
	}
	public void drawImage(int n, BitArray image, char c){
		PutCasBitArray(this.isRotated, n, c, image);
		// Lines[n] = image;
	}
	public void redrawImage(int n, BitArray image, char c, char b = BlankChar){
		Debug.Assert(Lines[n] != null);
        var ad = Lines[n].ToAddedDeleted(image);
        if(this.isRotated ) {
            VPutCasBitArray(n, c, ad.Added);
            VPutCasBitArray(n, b, ad.Deleted);
        }
		else {
            HPutCasBitArray(n, c, ad.Added);
            HPutCasBitArray(n, b, ad.Deleted);
        }
		// Lines[n] = image;
	}

	public static void PutCasBitArray(Boolean rot, int line, char c, BitArray bb) {
		if(rot)
			VPutCasBitArray(line, c, bb);
		else
			HPutCasBitArray(line, c, bb);
	}
	public static void VPutCasBitArray(int x, char c, BitArray bb) {
		Debug.Write(bb.renderImage());
		for(int i = 0; i < bb.Length; ++i)
            if (bb[i])
            {
                Console.SetCursorPosition(x, i);
                Console.Write(c);
            }
    }

	// public void HDrawImage(Side side, BitArray image){ HPutCasBitArray(SideToLine(side), SideToChar(side), image); }
	public static void HPutCasBitArray(int y, char c, BitArray bb) {
        for (int i = 0; i < bb.Length; ++i)
            if (bb[i])
            {
                Console.SetCursorPosition(i, y);
                Console.Write(c);
            }
    }
}
