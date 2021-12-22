using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
public class OpponentPaddle : Paddle {

	override public PaddleSide Side {get{return PaddleSide.Away;}}
    public OpponentPaddle(int width, int range) : base(width, range){

	}
}

public class SelfPaddle : Paddle {

	public Dictionary<System.ConsoleKey, Func<int>> ManipDict;
	override public PaddleSide Side {get{return PaddleSide.Home;}}
    public SelfPaddle(int width, int range, Dictionary<System.ConsoleKey, Func<int>> manipDict): base(width, range){
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
    // BitArray buffer { get; init; }
    public int Width { get;
        // { return (from bool m in buffer where m select m).Count(); }
        init; }
    
    public Slider Offset{get; init;}
    public Paddle(int width, int range)
    {
		Debug.Assert(width > 0 && range > 0 && range > width);
        Width = width;
        Offset = new(range - width);
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
        BitArray buff = new BitArray(Offset.Max + Width);
        for (int i = Offset.Value; i <= Offset.Value + Width; ++i)
            buff[i] = true;
        return buff; // er.Clone() as BitArray;
    }
}