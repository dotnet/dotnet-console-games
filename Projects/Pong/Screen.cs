using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

enum DrawDirection {Normal, Rotating}
public record struct Dimention ( int x, int y);
public class Screen : OnScreen {
	public enum CharCode {ESC = '\x1b', SPC = '\x20', VBAR = '|', HBAR = '-', DOT = '.'}
	public Dictionary<System.ConsoleKey, Func<int>> KeyManipDict;
	public const char BlankChar = (char)CharCode.SPC;
	public Action<int, BitArray, char> DrawImage;
	public Action<int, BitArray, char> RedrawImage; // (Line, this_array, new_array, c='+')
	public virtual bool isRotated {get; init;} // 90 degree
	protected int w {get; init;}
	protected int h {get; init;}
	public Dimention dim {get; init;}
	public int EndOfLines {get{return Lines.Length - 1;}}
	public BitArray[] Lines {get; private set;} // [h][w]
	// Gonsole console;
	Action<int, int> setCursorPosition;
	public Screen(int x = 80, int y = 24, bool rotate = false) {
		(w, h) = OnScreen.init(x, y);
		dim = new(w, h);
		isRotated = rotate;
		// console = isRotated ? new VGonsole() : HGonsole();
		setCursorPosition = isRotated ? (x, y) => Console.SetCursorPosition(y, x) : (x, y) => Console.SetCursorPosition(x, y);
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
	/* public void drawImage(int n, BitArray image, char c){
		PutCasBitArray(this.isRotated, n, c, image);
		// Lines[n] = image;
	} */

	/// <summary>Breaks image</summary>
	public void redrawImage(int n, BitArray image, char c, char b = BlankChar){
		Debug.Assert(Lines[n] != null);
        var ad = Lines[n].ToAddedDeleted(image);
		drawImage(n, ad.Added, c);
		drawImage(n, ad.Deleted, b);
		/*
        if(this.isRotated ) {
            VPutCasBitArray(n, c, ad.Added);
            VPutCasBitArray(n, b, ad.Deleted);
        }
		else {
            HPutCasBitArray(n, c, ad.Added);
            HPutCasBitArray(n, b, ad.Deleted);
        } */
		// Lines[n] = image;
	}

	public static void PutCasBitArray(Boolean rot, int line, char c, BitArray bb) {
		if(rot)
			VPutCasBitArray(line, c, bb);
		else
			HPutCasBitArray(line, c, bb);
	}
	public void drawImage(int line, BitArray bb, char c) {
		// Debug.Write(bb.renderImage());
		for(int i = 0; i < bb.Length; ++i)
            if (bb[i]) {
				// if(isRotated) SetCursorPosition(i, line); else
                SetCursorPosition(i, line);
                Console.Write(c);
            }
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

    public void SetCursorPosition(int x, int y){
		if(isRotated)
			(y, x) = (x, y);
		else {
			y = h - 1 -y;
		}
        Console.SetCursorPosition(x, y);
    }
}
