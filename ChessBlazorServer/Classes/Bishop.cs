namespace ChessBlazorServer.Classes
{
    public class Bishop : ChessPiece
    {
        public Bishop(string name, string svgName, string color, int row, int col) : base(name, svgName, color, row, col)
        {

        }

        public override void PossibleMoves(Board board)
        {
            (int startRow, int startCol) = this.Position;
            var directions = new List<(int rowChange, int colChange)>
            {
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
    }
}
