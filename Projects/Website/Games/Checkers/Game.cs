using System;
using System.Collections.Generic;
using System.Linq;
using static Website.Games.Checkers._using;

namespace Website.Games.Checkers;

public class Game
{
	private const int PiecesPerColor = 12;

	public PieceColor Turn { get; private set; }
	public Board Board { get; private set; }
	public PieceColor? Winner { get; private set; }
	public List<Player> Players { get; private set; }

	public Game(int humanPlayerCount)
	{
		if (humanPlayerCount < 0 || 2 < humanPlayerCount) throw new ArgumentOutOfRangeException(nameof(humanPlayerCount));
		Board = new Board();
		Players = new()
		{
			new Player(humanPlayerCount >= 1, Black),
			new Player(humanPlayerCount >= 2, White),
		};
		Turn = Black;
		Winner = null;
	}

	public void PerformMove(Move move)
	{
		(move.PieceToMove.X, move.PieceToMove.Y) = move.To;
		if ((move.PieceToMove.Color is Black && move.To.Y is 7) ||
			(move.PieceToMove.Color is White && move.To.Y is 0))
		{
			move.PieceToMove.Promoted = true;
		}
		if (move.PieceToCapture is not null)
		{
			Board.Pieces.Remove(move.PieceToCapture);
		}
		if (move.PieceToCapture is not null &&
			Board.GetPossibleMoves(move.PieceToMove).Any(move => move.PieceToCapture is not null))
		{
			Board.Aggressor = move.PieceToMove;
		}
		else
		{
			Board.Aggressor = null;
			Turn = Turn is Black ? White : Black;
		}
		CheckForWinner();
	}

	public void CheckForWinner()
	{
		if (!Board.Pieces.Any(piece => piece.Color is Black))
		{
			Winner = White;
		}
		if (!Board.Pieces.Any(piece => piece.Color is White))
		{
			Winner = Black;
		}
		if (Winner is null && Board.GetPossibleMoves(Turn).Count is 0)
		{
			Winner = Turn is Black ? White : Black;
		}
	}

	public int TakenCount(PieceColor colour) =>
		PiecesPerColor - Board.Pieces.Count(piece => piece.Color == colour);
}
