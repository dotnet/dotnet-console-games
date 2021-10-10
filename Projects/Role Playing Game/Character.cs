namespace Role_Playing_Game
{
	public class Character
	{
		public int Level { get; set; } = 1;
		public int Experience { get; set; }
		public int ExperienceToNextLevel { get; set; } = 10;
		public int Health { get; set; } = 10;
		public int MaxHealth { get; set; } = 10;
		public int Gold { get; set; }
		public int I { get; set; }
		public int J { get; set; }
		public int TileI => I < 0 ? (I - 6) / 7 : I / 7;
		public int TileJ => J < 0 ? (J - 3) / 4 : J / 4;
		private string[] _mapAnaimation;
		public string[] MapAnimation
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
				if (_mapAnimationFrame >= MapAnimation.Length)
				{
					if (MapAnimation == Sprites.RunUp)    { Moved = true; MapAnimation = Sprites.IdleUp;    }
					if (MapAnimation == Sprites.RunDown)  { Moved = true; MapAnimation = Sprites.IdleDown;  }
					if (MapAnimation == Sprites.RunLeft)  { Moved = true; MapAnimation = Sprites.IdleLeft;  }
					if (MapAnimation == Sprites.RunRight) { Moved = true; MapAnimation = Sprites.IdleRight; }
					_mapAnimationFrame = 0;
				}
			}
		}
		public bool IsIdle
		{
			get =>
				_mapAnaimation == Sprites.IdleDown ||
				_mapAnaimation == Sprites.IdleUp ||
				_mapAnaimation == Sprites.IdleLeft ||
				_mapAnaimation == Sprites.IdleRight;
		}
		public string Render =>
			_mapAnaimation is not null && _mapAnimationFrame < _mapAnaimation.Length
			? _mapAnaimation[_mapAnimationFrame]
			: // "T" pose :D
			  @" __O__ " + '\n' +
			  @"   |   " + '\n' +
			  @"   |   " + '\n' +
			  @"  | |  ";
		public bool Moved { get; set; } = false;
	}
}
