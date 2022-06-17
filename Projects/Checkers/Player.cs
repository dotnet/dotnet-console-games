using Checkers.Types;

namespace Checkers;

public class Player
{
    public PlayerControl ControlledBy { get; set; }
    public PieceColour Side { get; set; }
}

