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

    public string InitialPosition
    {
        set
        {
            var position = PositionHelper.GetPositionByNotation(value);

            if (position == null)
            {
                return;
            }

            XPosition = position.Value.X;
            YPosition = position.Value.Y;
        }
    }

    public PieceColour Side { get; set; }

    public bool InPlay { get; set; }

    public bool Promoted { get; set; }

    public bool Aggressor { get; set; }
}

