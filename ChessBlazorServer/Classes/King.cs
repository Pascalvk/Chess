namespace ChessBlazorServer.Classes
{
    public class King : ChessPiece
    {
        public King(string name, string svgName, string color, int row, int col) : base(name, svgName, color, row, col)
        {

        }

        public override void PossibleMoves(Board board)
        {
            (int startRow, int startCol) = this.Position;
            var directions = new List<(int rowChange, int colChange)>
            {
                (-1, 0), // Up
                (1, 0),  // Down
                (0, 1),  // Right
                (0, -1), // Left
                (-1, -1), // Diagonal left up
                (-1, 1),  // Diagonal right up
                (1, -1),  // Diagonal left down
                (1, 1)    // Diagonal right down
            };

            // Loop through each direction and calculate possible moves
            foreach (var (rowChange, colChange) in directions)
            {
                CalculateMovesInDirection(board, startRow, startCol, rowChange, colChange);
            }
        }


        // not done yet; Make it so it can do a +1 gamestate move
        /*
        public override void CalculateMovesInDirection(Board board, int startRow, int startCol, int rowChange, int colChange)
        {
            // New position
            int newRow = startRow + rowChange;
            int newCol = startCol + colChange;
            if (board.IsWithinBounds(newRow, newCol))
            {
                var pieceAtPosition = board.GetPieceAt(newRow, newCol);
                // if empty or enemy
                if (pieceAtPosition == null || pieceAtPosition.Color != this.Color)
                {
                    if (board.IsUnderAttack(newRow, newCol) == false)
                    {
                        this.AddToPossibleMoveList(newRow, newCol);
                    }
                }
            }
        }
        */
    }
}
