namespace ChessBlazorServer.Classes
{
    public class Board
    {
        public const int BoardSize = 8;
        private ChessPiece[,] grid;
        public List<(int row, int col)> UnderAttackPositions { get; set; }

        public Board()
        {
            grid = new ChessPiece[BoardSize, BoardSize];
            UnderAttackPositions = new List<(int, int)>();
            SetupBoard();
        }

        // Empty constructor (used for cloning)
        public Board(bool empty)
        {
            grid = new ChessPiece[BoardSize, BoardSize];
        }

        public void SetupBoard()
        {     
            // White pieces
            grid[6, 0] = new Pawn("P", "wp", "white", 6, 0);
            grid[6, 1] = new Pawn("P", "wp", "white", 6, 1);
            grid[6, 2] = new Pawn("P", "wp", "white", 6, 2);
            grid[6, 3] = new Pawn("P", "wp", "white", 6, 3);
            grid[6, 4] = new Pawn("P", "wp", "white", 6, 4);
            grid[6, 5] = new Pawn("P", "wp", "white", 6, 5);
            grid[6, 6] = new Pawn("P", "wp", "white", 6, 6);
            grid[6, 7] = new Pawn("P", "wp", "white", 6, 7);

            grid[7, 0] = new Rook("R", "wr", "white", 7, 0);
            grid[7, 7] = new Rook("R", "wr", "white", 7, 7);

            grid[7, 1] = new Knight("N", "wn", "white", 7, 1);
            grid[7, 6] = new Knight("N", "wn", "white", 7, 6);
                     
            grid[7, 2] = new Bishop("B", "wb", "white", 7, 2);
            grid[7, 5] = new Bishop("B", "wb", "white", 7, 5);
       
            grid[7, 3] = new Queen("Q", "wq", "white", 7, 3);

            grid[7, 4] = new King("K", "wk", "white", 7, 4);

            // Black pieces
            grid[1, 0] = new Pawn("P", "bp", "black", 1, 0);
            grid[1, 1] = new Pawn("P", "bp", "black", 1, 1);
            grid[1, 2] = new Pawn("P", "bp", "black", 1, 2);
            grid[1, 3] = new Pawn("P", "bp", "black", 1, 3);
            grid[1, 4] = new Pawn("P", "bp", "black", 1, 4);
            grid[1, 5] = new Pawn("P", "bp", "black", 1, 5);
            grid[1, 6] = new Pawn("P", "bp", "black", 1, 6);
            grid[1, 7] = new Pawn("P", "bp", "black", 1, 7);

            grid[0, 0] = new Rook("R", "br", "black", 0, 0);
            grid[0, 7] = new Rook("R", "br", "black", 0, 7);

            grid[0, 1] = new Knight("N", "bn", "black", 0, 1);
            grid[0, 6] = new Knight("N", "bn", "black", 0, 6);

            grid[0, 2] = new Bishop("B", "bb", "black", 0, 2);
            grid[0, 5] = new Bishop("B", "bb", "black", 0, 5);

            grid[0, 3] = new Queen("Q", "bq", "black", 0, 3);

            grid[0, 4] = new King("K", "bk", "black", 0, 4);
        }

        // Moves a piece to a new location; Note: No logic checks inside this method
        public void MovePieceToNewPositionOnBoard(int currentRow, int currentCol, int newRow, int newCol)
        {
            grid[newRow, newCol] = grid[currentRow, currentCol];
            grid[currentRow, currentCol] = null;
            grid[newRow, newCol].NewPosition(newRow, newCol);
        }

        // Just place a piece at the location
        public void SetPieceAt(int row, int col, ChessPiece Piece)
        {
            grid[row, col] = Piece;
        }

        // Is the move withing bounds of the board
        public bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < BoardSize && col >= 0 && col < BoardSize;
        }

        // Gets a piece from a cord
        public ChessPiece GetPieceAt(int row, int col)
        {
            return grid[row, col];
        }

        // Method to fill the list of attacked positions to current board; No uses yet; maybe use as helper
        public void UpdateUnderAttackPositions()
        {
            UnderAttackPositions.Clear();
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var piece = GetPieceAt(row, col);
                    if (piece != null)
                    {
                        // Skips King; maybe delete this; still testing needed
                        if (piece is King)
                        {
                            continue; 
                        }
                        piece.PossibleMoves(this);
                        UnderAttackPositions.AddRange(piece.AttackList);
                    }
                }
            }
            // Remove duplicates
            UnderAttackPositions = UnderAttackPositions.Distinct().ToList();
        }

        public bool IsUnderAttack(int row, int col)
        {
            
            return UnderAttackPositions.Contains((row, col));
        }

        // Use to print a board to console
        public void DebugPrintBoard()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    ChessPiece piece = grid[row, col];
                    if (piece == null)
                    {
                        Console.Write(". "); 
                    }
                    else
                    {
                        Console.Write(piece.Name + " "); 
                    }
                }
                Console.WriteLine(); 
            }
        }

        public void DebugPrintUnderAttackList()
        {
            foreach (var attackedPos in UnderAttackPositions)
            {
                Console.WriteLine(attackedPos);
            }
        }
    }
}
