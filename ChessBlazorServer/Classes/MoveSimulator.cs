namespace ChessBlazorServer.Classes
{
    public class MoveSimulator : Board
    {
        public Board SimulatedBoard { get; set; }

        public MoveSimulator(Board originalBoard) : base(true) 
        {
            CloneBoard(originalBoard);
        }

        // Clone board
        private void CloneBoard(Board originalBoard)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    ChessPiece piece = originalBoard.GetPieceAt(row, col);
                    if (piece != null)
                    {
                        SetPieceAt(row, col, piece.Clone());
                    }
                }
            }
            UnderAttackPositions = new List<(int, int)>(originalBoard.UnderAttackPositions);
        }
    }
}
