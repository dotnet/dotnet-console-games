using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
public class OpponentPaddle : Paddle {

	override public PaddleSide Side {get{return PaddleSide.Away;}}
    public OpponentPaddle(Range range,int width): base(range, width) {
	}
}

public class SelfPaddle : Paddle {

	public Dictionary<System.ConsoleKey, Func<int>> ManipDict;
	override public PaddleSide Side {get{return PaddleSide.Home;}}
    public SelfPaddle(Range range, int width, Dictionary<System.ConsoleKey, Func<int>> manipDict): base(range, width) {
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
	public virtual char BlankChar{get{return '|';}}
    // BitArray buffer { get; init; }
    public int Width { get;
        // { return (from bool m in buffer where m select m).Count(); }
        init; }
    
    public Slider Offset{get; init;}
    public Paddle(Range range, int width)
    {
		Debug.Assert(width > 0);
        Debug.Assert(range.End.Value > width);
        Width = width;
        Offset = new(0..(range.End.Value - width + 1));
        // buffer = new BitArray(range);
        // for (int i = 0; i < width; ++i) buffer[i] = true;
    }
	
/// <summary>Manipulate</summary>
/// <returns>0 if no reaction</returns> 
    public int Shift(int n)
    {
        return Offset.Move(n);
        // return buffer.ClampShift(n);
    }
    public BitArray GetImage() {
        BitArray buff = new BitArray(Offset.Max + Width + 1);
        for (int i = Offset.Value; i < Offset.Value + Width; ++i)
            buff[i] = true;
        return buff; // er.Clone() as BitArray;
    }
}