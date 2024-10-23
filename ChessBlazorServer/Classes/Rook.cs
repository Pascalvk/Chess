namespace ChessBlazorServer.Classes
{
    public class Rook : ChessPiece
    {
        public Rook(string name, string svgName, string color, int row, int col) : base(name, svgName ,color, row, col)
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
            };

            // Loop through each direction and calculate possible moves
            foreach (var (rowChange, colChange) in directions)
            {
                CalculateMovesInDirection(board, startRow, startCol, rowChange, colChange);
            }
        }



    }
}
