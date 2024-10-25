namespace ChessBlazorServer.Classes
{
    public class Pawn : ChessPiece
    {
        public bool CanBeTakenEnPassant { get; set; }
        public Pawn(string name, string svgName, string color, int row, int col) : base(name, svgName, color, row, col)
        {
            CanBeTakenEnPassant = false;
        }

        public override void ChangeEnPassantStatus(bool status)
        {
            this.CanBeTakenEnPassant = status;
        }

        public override ChessPiece Clone()
        {
            // Clone the Pawn and copy the EnPassant status
            Pawn clonedPawn = new Pawn(this.Name, this.SVGName, this.Color, this.Position.Row, this.Position.Col)
            {
                IsCaptured = this.IsCaptured,
                HasMoved = this.HasMoved,
                CanBeTakenEnPassant = this.CanBeTakenEnPassant,
                MoveList = new List<(int, int)>(this.MoveList)
            };

            return clonedPawn;
        }

        public override void PossibleMoves(Board board)
        {
            (int startRow, int startCol) = this.Position;

            // Direction depending on color; white is up -> -1; black is down -> 1
            int direction = (this.Color == "white") ? -1 : 1;

            // One move up
            int newRow = startRow + direction;
            if (board.IsWithinBounds(newRow, startCol) && board.GetPieceAt(newRow, startCol) == null)
            {
                this.AddToPossibleMoveList(newRow, startCol);
            }

            // If on start position a Pawn can move 2 squares
            int doubleMoveRow = startRow + 2 * direction;
            if (this.HasMoved == false && board.IsWithinBounds(doubleMoveRow, startCol)  
                && board.GetPieceAt(newRow, startCol) == null
                && board.GetPieceAt(doubleMoveRow, startCol) == null)
            {
                this.AddToPossibleMoveList(doubleMoveRow, startCol);
            }

            // Attack piece diagonal left
            int attackLeftRow = startRow + direction;
            int attackLeftCol = startCol - 1;
            if (board.IsWithinBounds(attackLeftRow, attackLeftCol))
            {
                var leftPiece = board.GetPieceAt(attackLeftRow, attackLeftCol);
                if (leftPiece != null && leftPiece.Color != this.Color)
                {
                    this.AddToPossibleMoveList(attackLeftRow, attackLeftCol);
                }
                this.AddToAttackList(attackLeftRow, attackLeftCol);
            }

            // Attack piece diagonal right
            int attackRightRow = startRow + direction;
            int attackRightCol = startCol + 1;
            if (board.IsWithinBounds(attackRightRow, attackRightCol))
            {
                var rightPiece = board.GetPieceAt(attackRightRow, attackRightCol);
                if (rightPiece != null && rightPiece.Color != this.Color)
                {
                    this.AddToPossibleMoveList(attackRightRow, attackRightCol);

                }
                this.AddToAttackList(newRow, attackRightCol);
            }

            // En passant check
            int enPassantRow = startRow; 
            int enPassantColLeft = startCol - 1;
            int enPassantColRight = startCol + 1;

            // Check left for en passant
            if (board.IsWithinBounds(enPassantRow, enPassantColLeft))
            {
                var leftPiece = board.GetPieceAt(enPassantRow, enPassantColLeft);
                if (leftPiece is Pawn pawn && pawn.CanBeTakenEnPassant == true)
                {
                    this.AddToPossibleMoveList(enPassantRow + direction, enPassantColLeft);
                }
            }

            // Check right for en passant
            if (board.IsWithinBounds(enPassantRow, enPassantColRight))
            {
                var rightPiece = board.GetPieceAt(enPassantRow, enPassantColRight);
                if (rightPiece is Pawn pawn && pawn.CanBeTakenEnPassant == true)
                {
                    this.AddToPossibleMoveList(enPassantRow + direction, enPassantColRight);
                }
            }

        }



    }
}
