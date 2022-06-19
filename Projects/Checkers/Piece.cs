namespace Checkers;

public class Piece
{
    public Piece()
    {
        InPlay = true;
        Promoted = false;
    }

    public int XPosition { get; set; }

    public int YPosition { get; set; }

    public string NotationPosition
    {
        get => PositionHelper.ToPositionNotationString(XPosition, YPosition);
        set => (XPosition, YPosition) = PositionHelper.ParsePositionNotation(value);
    }

    public PieceColour Side { get; set; }

    public bool InPlay { get; set; }

    public bool Promoted { get; set; }

    public bool Aggressor { get; set; }
}

