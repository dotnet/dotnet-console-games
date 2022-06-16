using System.Diagnostics;
using Checkers.Types;

namespace Checkers.Helpers;

    /// <summary>
    /// Used for logging games for analysis - see flag in Program.cs
    /// </summary>
    public static class LoggingHelper
    {
        public static void LogMove(string from, string to, PlayerAction action, PieceColour sidePlaying, int blacksInPlay, int whitesInPlay)
        {
            var colour = sidePlaying == PieceColour.Black ? "B" : "W";
            var suffix = DecodePlayerAction(action);

            var outputLine = $"Move   : {colour} {from}-{to} {suffix,2}";

            if (action == PlayerAction.Capture || action == PlayerAction.CapturePromotion)
            {
                var piecesCount = $" B:{blacksInPlay,2} W:{whitesInPlay,2}";
                outputLine += piecesCount;
            }

            Trace.WriteLine(outputLine);
        }

        public static void LogMoves(int numberOfMoves)
        {
            Trace.WriteLine($"Moves  : {numberOfMoves}");
        }

        public static void LogStart()
        {
            Trace.WriteLine($"Started: {DateTime.Now}");
        }

        public static void LogFinish()
        {
            Trace.WriteLine($"Stopped: {DateTime.Now}");
        }

        public static void LogOutcome(PieceColour winner)
        {
            Trace.WriteLine($"Winner : {winner}");
        }

        private static string DecodePlayerAction(PlayerAction action)
        {
            switch (action)
            {
                case PlayerAction.Move:
                    return string.Empty;
                case PlayerAction.Promotion:
                    return "K";
                case PlayerAction.Capture:
                    return "X";
                case PlayerAction.CapturePromotion:
                    return "KX";
                default:
                    return String.Empty;
            }
        }
    }
