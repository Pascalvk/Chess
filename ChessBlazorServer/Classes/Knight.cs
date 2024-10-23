namespace ChessBlazorServer.Classes
{
    public class Knight : ChessPiece
    {
        public Knight(string name, string svgName, string color, int row, int col) : base(name, svgName, color, row, col)
        {

        }

        public override void PossibleMoves(Board board)
        {
            (int startX, int startY) = this.Position;

            var directions = new List<(int xChange, int yChange)>
        {
            (2, 1), (2, -1), (-2, 1), (-2, -1),
            (1, 2), (1, -2), (-1, 2), (-1, -2)
        };

            foreach (var (xChange, yChange) in directions)
            {
                int newX = startX + xChange;
                int newY = startY + yChange;

                if (board.IsWithinBounds(newX, newY))
                {
                    var pieceAtPosition = board.GetPieceAt(newX, newY);
                    if (pieceAtPosition == null || pieceAtPosition.Color != this.Color)
                    {
                        this.AddToPossibleMoveList(newX, newY);
                    }
                }
            }
        }



    }
}
