namespace ChessBlazorServer.Classes
{
    public class MoveSimulator
    {
        public Board SimulatedBoard { get; set; }

        public MoveSimulator(Board originalBoard)
        {
            SimulatedBoard = CloneBoard(originalBoard);
        }

        // Clone board
        public Board CloneBoard(Board originalBoard)
        {
            // Make a deep copy
            Board clone = originalBoard;
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    var piece = originalBoard.GetPieceAt(row, col);
                    if (piece != null)
                    {
                        // Copies the piece to the board
                        clone.SetPieceAt(row, col, piece.Clone());
                    }
                }
            }
            return clone;
        }

        // Use to print a board to console
        public void DebugPrintBoard()
        {
            Console.WriteLine("Board state:");

            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    ChessPiece piece = SimulatedBoard.GetPieceAt(row, col);

                    if (piece == null)
                    {
                        Console.Write(". ");  
                    }
                    else
                    {
                        Console.Write(piece.Name[0] + " ");  
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
