using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class PaddleScreen : Screen {
	public int PaddleRange {get{return AwayLineNum + 1;}} // 0 <= Paddle < PaddleRange
	public int AwayLineNum {get;init;}
	public const int HomeLineNum = 0;
	// public Paddle[] Paddles = new Paddle[2]; // 0: self, 1: opponent
	public PaddleScreen(int x, int y, bool rotate) : base(x,y,rotate) {
		AwayLineNum = this.EndOfLines; // Lines.Length - 1;
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
	public void DrawPaddle(Paddle paddle){
		BitArray image = paddle.GetImage();
		drawImage(paddle.Side == PaddleSide.Home ? HomeLineNum : AwayLineNum, image, paddle.DispChar);
	}
	public void RedrawPaddle(Paddle paddle){
		BitArray image = paddle.GetImage();
		redrawImage(paddle.Side == PaddleSide.Home ? HomeLineNum : AwayLineNum, image, paddle.DispChar);
	}

}
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
		Lines = new BitArray[isRotated ? h : w];
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
