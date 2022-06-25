﻿namespace Animal_Trainer;

public class Character
{
	// I & J represent the character's position in pixel coordinates
	// relative to the current map
	public int I { get; set; }
	public int J { get; set; }
	// TileI and TileJ represent the character's position in tile coordinates
	// relative to the current map
	public int TileI => I < 0 ? (I - 6) / 7 : I / 7;
	public int TileJ => J < 0 ? (J - 4) / 5 : J / 5;
	private string[]? _mapAnaimation;
	public string[]? MapAnimation
	{
		get => _mapAnaimation;
		set
		{
			_mapAnaimation = value;
			_mapAnimationFrame = 0;
		}
	}
	private int _mapAnimationFrame;
	public int MapAnimationFrame
	{
		get => _mapAnimationFrame;
		set
		{
			_mapAnimationFrame = value;
			Moved = false;
			if (_mapAnimationFrame >= MapAnimation!.Length)
			{
				if (MapAnimation == Sprites.RunUp) { Moved = true; MapAnimation = Sprites.Idle; }
				if (MapAnimation == Sprites.RunDown) { Moved = true; MapAnimation = Sprites.Idle; }
				if (MapAnimation == Sprites.RunLeft) { Moved = true; MapAnimation = Sprites.Idle; }
				if (MapAnimation == Sprites.RunRight) { Moved = true; MapAnimation = Sprites.Idle; }
				_mapAnimationFrame = 0;
			}
		}
	}
	public bool IsIdle
	{
		get =>
			_mapAnaimation == Sprites.Idle;
	}
	public string Render =>
		_mapAnaimation is not null && _mapAnimationFrame < _mapAnaimation.Length
		? _mapAnaimation[_mapAnimationFrame]
		: // "T" pose :D
		  @" __O__ " + '\n' +
		  @"   |   " + '\n' +
		  @"   O   " + '\n' +
		  @"   |   " + '\n' +
		  @"  | |  ";
	public bool Moved { get; set; } = false;
}
