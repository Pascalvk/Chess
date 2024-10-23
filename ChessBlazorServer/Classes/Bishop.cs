namespace ChessBlazorServer.Classes
{
    public class Bishop : ChessPiece
    {
        public Bishop(string name, string svgName, string color, int xCord, int yCord) : base(name, svgName, color, xCord, yCord)
        {

        }

        public override void PossibleMoves(Board board)
        {
            (int startX, int startY) = this.Position;
            var directions = new List<(int xChange, int yChange)>
            {
                (-1, -1), // Diagonal left up
                (-1, 1),  // Diagonal right up
                (1, -1),  // Diagonal left down
                (1, 1)    // Diagonal right down
            };

            // Loop through each direction and calculate possible moves
            foreach (var (xChange, yChange) in directions)
            {
                CalculateMovesInDirection(board, startX, startY, xChange, yChange);
            }
        }
    }
}
