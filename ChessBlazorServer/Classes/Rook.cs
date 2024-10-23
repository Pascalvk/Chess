namespace ChessBlazorServer.Classes
{
    public class Rook : ChessPiece
    {
        public Rook(string name, string svgName, string color, int row, int col) : base(name, svgName ,color, row, col)
        {

        }

        public override void PossibleMoves(Board board)
        {
            (int startX, int startY) = this.Position;
            var directions = new List<(int xChange, int yChange)>
            {
                (-1, 0), // Up
                (1, 0),  // Down
                (0, 1),  // Right
                (0, -1), // Left
            };

            // Loop through each direction and calculate possible moves
            foreach (var (xChange, yChange) in directions)
            {
                CalculateMovesInDirection(board, startX, startY, xChange, yChange);
            }
        }



    }
}
