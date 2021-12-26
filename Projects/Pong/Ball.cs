using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public record Offsets(int x, int y);
public class Ball // : ScreenDrawItem
{
	public char DispChar {get{return 'O';}}
	float X;
	float Y;
	float dX;
	float dY;
	public Offsets offsets {get {
		return new Offsets(XOffset.Value, YOffset.Value);
	}}
	public Slider XOffset{get; init;}
	public Slider YOffset{get; init;}
	Random random = new();
	public Ball(Range x_range, Range y_range, bool rotate){
	float randomFloat = (float)random.NextDouble() * 2f;
	float dx = Math.Max(randomFloat, 1f - randomFloat);
	float dy = 1f - dx;
	float x = x_range.End.Value / 2f;
	float y = y_range.End.Value / 2f;
	if (random.Next(2) == 0)
		dx = -dx;
	if (random.Next(2) == 0)
		dy = -dy;
	/* if (rotate) {
		(Y, X) = (x, y);
		(dY, dX) = (dx, dy);
		YOffset = new Slider(x_range, (int)x);
		XOffset = new Slider(y_range, (int)y);
	}
	else { */
		(X, Y) = (x, y);
		(dX, dY) = (dx, dy);
		XOffset = new Slider(x_range, (int)x);
		YOffset = new Slider(y_range, (int)y);
	}

	public Offsets Move() {
		if (XOffset.Value == 0 && dX < 0f ||
			XOffset.Value == XOffset.Max && dX > 0f)
			dX = -dX;
		if (YOffset.Value == 0 && dY < 0f ||
			YOffset.Value == YOffset.Max && dY > 0f)
			dY = -dY;
		X += dX;
		Y += dY;
		XOffset.set((int)X);
		YOffset.set((int)Y);
		return offsets;
	}
	/* public BitArray GetImage(){
		BitArray buff = new BitArray(XOffset.Max + 1);
		return
	}*/
}
