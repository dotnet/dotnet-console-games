using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public enum Rotation {
	Horizontal, Vertical
}

public class Screen : OnScreen {
	public Action<int, BitArray, char> redrawPaddle; // (Line, this_array, new_array, c='+')
	public bool isRotated {get; init;} // 90 degree
	public int w {get; init;}
	public int h {get; init;}
	public BitArray[] Lines {get; private set;} // [h][w]
	public Screen(int x = 80, int y = 24, bool rotate = false) {
		(w, h) = OnScreen.init(x, y);
		isRotated = rotate;
		Lines = new BitArray[isRotated ? w : h];
		// for(int i = 0; i < (rotate ? w :h); ++i) buffer[i] = new BitArray(rotate ? h :w);
			redrawPaddle = isRotated ? (line, new_buff, c)=>{
				var (added, deleted) = Lines[line].ToAddedDeleted(new_buff);
				VPutCasBitArray(line, c, added);
				VPutCasBitArray(line, ' ', deleted);
			} : (line, new_buff, c)=>{
				var (added, deleted) = Lines[line].ToAddedDeleted(new_buff);
				HPutCasBitArray(line, c, added);
				HPutCasBitArray(line, ' ', deleted);
			};

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

	public static void PutCasBitArray(Boolean rot, int line, char c, BitArray bb) {
		if(rot)
			HPutCasBitArray(line, c, bb);
		else
			VPutCasBitArray(line, c, bb);

	}
	public static void VPutCasBitArray(int x, char c, BitArray bb) {
		for(int i = 0; i < bb.Length; ++i)
		if(bb[i]){ 
			Console.SetCursorPosition(x, i);
			Console.Write(c);
		}
	}
	public static void HPutCasBitArray(int y, char c, BitArray bb) {
				for(int i = 0; i < bb.Length; ++i)
		if(bb[i]){ 
			Console.SetCursorPosition(i, y);
			Console.Write(c);
		}
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
		(int _x, int _y) = (x > 0 ? x : W, y > 0 ? y : H);
		(int mx, int my) = (_x % 8, _y % 8);
		dim = new(_x - mx, _y - my);
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
	void draw(in char [][] buffer);
}

public class PaddleBase : NestedRange, HasDispChar, KeyManipulate {
	public int Speed_ratio {get; protected set;}
	public Dictionary<System.ConsoleKey, Func<int>> manipDict = new();
	virtual public char DispChar() {
		return '+';
	}
	public PaddleBase(int segment_length, int width, int speed_ratio) : base(0..segment_length, 0..width) {
		// inner = 0..(scr.w / quot); outer = 0..(scr.w);
		Speed_ratio = speed_ratio;
	}

	virtual public char [][] render() { 
	  char[][] buffer = new char[1][];
		buffer[0] = new char[1];
		buffer[0][0] = DispChar(); // only at base point
		return buffer;
	}

	virtual public bool manipulate(System.ConsoleKey key) {
		return false;
	}

}

public class HPaddle : PaddleBase {

	public Screen screen {get; init;}
	public bool atHome {get; private set;}
	public char dispChar {get; private set;}
	public HPaddle(Screen screen_, int width, int speed_ratio = 1, bool at_home = true, char disp_char = (char)ScreenChar.B) : 
	base(screen_.isRotated ? screen_.h : screen_.w, width, speed_ratio) {
		atHome = at_home;
		dispChar = disp_char;
		if (screen_.isRotated) {
			manipDict.Add(ConsoleKey.DownArrow, () => shift(-speed_ratio));
			manipDict.Add(ConsoleKey.UpArrow, () => shift(speed_ratio));
		} else {
			manipDict.Add(ConsoleKey.LeftArrow, () => shift(-speed_ratio));
			manipDict.Add(ConsoleKey.RightArrow, () => shift(speed_ratio));
		}
	}

	override public char DispChar() {
		return dispChar;
	}

	override public char [][] render() {
		char [][] buffer = new char[1][];
		buffer[0] = new char[screen.isRotated ? screen.h : screen.w];
		for (int x = inner.Start.Value; x < inner.End.Value; ++x) 
			buffer[0][x] = DispChar();
		return buffer;
	}

	override public bool manipulate(System.ConsoleKey key) {
		if (screen.isRotated)
		switch(key) {
			case ConsoleKey.UpArrow: shift(-Speed_ratio); return true;
			case ConsoleKey.RightArrow: shift(Speed_ratio); return true;
		}
		switch(key) {
			case ConsoleKey.LeftArrow: shift(-Speed_ratio); return true;
			case ConsoleKey.RightArrow: shift(Speed_ratio); return true;
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
	public Range inner {
        get{ return (offset.Value..(offset.Value + width)); }
     }
    public Clamp offset{get; init;}
    public int width {get; init;}
	public Range outer {get; init;}
	public NestedRange(Range r1, Range r2) {
            var (_inner, _outer) = 
        (r1.End.Value - r1.Start.Value < r2.End.Value - r2.Start.Value) ?
            (r1, r2) : (r2, r1); // automatically makes smaller range as inner.

		if (_inner.Start.Value < _outer.Start.Value || _inner.End.Value > _outer.End.Value)
            throw new ArgumentOutOfRangeException("Inner range out of outer range!");
		// inner = _inner;
        offset = new Clamp(_outer.End.Value - _inner.End.Value);
        width = _inner.End.Value - _inner.Start.Value;
		outer = _outer;
	}

	public NestedRange() {
		outer = (0..1);
        offset = new Clamp(0);
		width = 0;
	}
	public int shift(int d) {
		return offset.Move(d);
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
		for (int i = 0; i < width; ++i)
			all[i + offset.Value] = true;
		return all;
	}
	
}

public class Clamp
{
    public int Value {get; private set;}
    public int Max {get; init;}

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
        if (Value == Max - 1)
			return false;
        Value += 1;
        return true;
    }
	public int Add(int n) {
		int i = 0;
		while(n-- > 0) {
			if (!Inc())
				break;
			++i;
		}
		return i;
	}
    public bool Dec(){
        if (Value == 0) 
			return false;
        Value -= 1;
        return true;
    }

	public int Sub(int n) {
		int i = 0;
		while(n-- > 0) {
			if (!Dec())
				break;
			++i;
		}
		return i;
	}

	public int Move(int n) {
		if (n > 0) 
			return Add(n);
		else if (n < 0)
			return Sub(n);
		return 0;
	}
    public bool set(int nv) {
        if (nv < 0 || nv >= Max)
            throw new ArgumentOutOfRangeException("Out of clamp range!");
        if (nv == Value)
            return false;
        Value = nv;
        return true;
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

static class BitArrayExtention {
	public static Tuple<BitArray, BitArray> ToAddedDeleted (this BitArray this_one, BitArray new_one) {
		var xor = this_one.Xor(new_one);
		return Tuple.Create(xor.And(new_one), xor.And(this_one));
	}
	public static void Shift(this BitArray this_one, int d) {
		if (d < 0)
			this_one.RightShift(d);
		else if (d > 0)
			this_one.LeftShift(d);
	}

	public static int ClampShift(this BitArray this_one, int n) {
		int i = 0;
		if (n < 0) {
			n = -n;
			for(; i < n; ++i)
				if (!this_one.ClampRightShift1())
					break;
			return -i;
		}
		else if (n > 0) {
			for(; i < n; ++i)
				if (!this_one.ClampRightShift1())
					break;
			return i;
		}
		return 0;
	}
	public static bool ClampRightShift1(this BitArray this_one) {
		if (this_one[0])
			return false;
		this_one.RightShift(1);
		return true;
	}
	public static bool ClampLeftShift1(this BitArray this_one) {
		if (this_one[^1])
			return false;
		this_one.LeftShift(1);
		return true;
	}
}