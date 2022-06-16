using Checkers.Types;

namespace Checkers.Helpers;

    /// <summary>
    /// Assigns AI/human players
    /// N.B. currently can only play as black in a one player game
    /// </summary>
    public static class PlayerHelper
    {
        public static void AssignPlayersToSide(int numberOfPlayers, Game currentGame)
        {
            switch (numberOfPlayers)
            {
                case 0:
                    foreach (var player in currentGame.Players)
                    {
                        player.ControlledBy = PlayerControl.Computer;
                    }

                    break;
                case 1:
                    foreach (var player in currentGame.Players.Where(x => x.Side == PieceColour.White))
                    {
                        player.ControlledBy = PlayerControl.Computer;
                    }

                    foreach (var player in currentGame.Players.Where(x => x.Side == PieceColour.Black))
                    {
                        player.ControlledBy = PlayerControl.Human;
                    }

                    break;
                case 2:
                    foreach (var player in currentGame.Players)
                    {
                        player.ControlledBy = PlayerControl.Human;
                    }

                    break;
            }
        }
    }
