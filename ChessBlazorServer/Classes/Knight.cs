namespace ChessBlazorServer.Classes
{
    public class Knight : ChessPiece
    {
        public Knight(string name, string svgName, string color, int row, int col) : base(name, svgName, color, row, col)
        {

        }

        public override ChessPiece Clone()
        {
            Knight clonedPiece = new Knight(this.Name, this.SVGName, this.Color, this.Position.Row, this.Position.Col)
            {
                IsCaptured = this.IsCaptured,
                HasMoved = this.HasMoved,
                MoveList = new List<(int, int)>(this.MoveList),
                AttackList = new List<(int, int)>(this.AttackList),
                AttackingPieceList = new List<(int, int)>(this.AttackingPieceList)
            };

            return clonedPiece;
        }

        public override void PossibleMoves(Board board)
        {
            MoveList.Clear();
            AttackList.Clear();
            (int startRow, int startCol) = this.Position;

            var directions = new List<(int rowChange, int colChange)>
            {
                (2, 1), (2, -1), (-2, 1), (-2, -1),
                (1, 2), (1, -2), (-1, 2), (-1, -2)
            };

            foreach (var (rowChange, colChange) in directions)
            {
                int newRow = startRow + rowChange;
                int newCol = startCol + colChange;

                if (board.IsWithinBounds(newRow, newCol))
                {
                    var pieceAtPosition = board.GetPieceAt(newRow, newCol);
                    if (pieceAtPosition.Name == "Empty" || pieceAtPosition.Color != this.Color)
                    {
                        this.AddToPossibleMoveList(newRow, newCol);
                        this.AddToAttackList(newRow, newRow);
                    }
                }
            }
        }



    }
}
