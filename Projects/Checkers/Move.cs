using System.Drawing;

namespace Checkers;

public class Move
{
    public Piece? PieceToMove { get; set; }

    public Point To { get; set; }

    public Point? Capturing { get; set; }

    public MoveType TypeOfMove { get; set; } = MoveType.Unknown;
}

