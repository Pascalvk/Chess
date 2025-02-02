namespace ChessBlazorServer.Classes
{
    public class Bishop : ChessPiece
    {
        public Bishop(string name, string svgName, string color, int row, int col) : base(name, svgName, color, row, col)
        {

        }

        public override ChessPiece Clone()
        {
            Bishop clonedPiece = new Bishop(this.Name, this.SVGName, this.Color, this.Position.Row, this.Position.Col)
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
            foreach (var (rowChange, colChange) in directions)
            {
                CalculateAttacksInDirection(board, startRow, startCol, rowChange, colChange);
            }
            
        }

        public override void PossiblePiecesToAttack(Board board)
        {
            AttackingPieceList.Clear();
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
                CalculateAttackingPiecesInDirection(board, startRow, startCol, rowChange, colChange);
            }
        }
    }
}
