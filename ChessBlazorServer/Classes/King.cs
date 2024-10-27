namespace ChessBlazorServer.Classes
{
    public class King : ChessPiece
    {
        public King(string name, string svgName, string color, int row, int col) : base(name, svgName, color, row, col)
        {

        }

        public override ChessPiece Clone()
        {
            King clonedPiece = new King(this.Name, this.SVGName, this.Color, this.Position.Row, this.Position.Col)
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

        // king does a +1 gamestate to control wether it can move to the sqaure; it cant put itself into check
        public override void CalculateMovesInDirection(Board board, int startRow, int startCol, int rowChange, int colChange)
        {
            // Gets the start position
            var piece = board.GetPieceAt(startRow, startCol);

            // New position
            int newRow = startRow + rowChange;
            int newCol = startCol + colChange;
            if (board.IsWithinBounds(newRow, newCol))
            {
                var pieceAtPosition = board.GetPieceAt(newRow, newCol);
                // if empty or enemy
                if (pieceAtPosition.Name == "Empty" || pieceAtPosition.Color != this.Color)
                {                
                    string oppenentColor = "";
                    if (this.Color == "white")
                    {
                        oppenentColor = "black";
                    }
                    if (this.Color == "black")
                    {
                        oppenentColor = "white";
                    }

                    MoveSimulator copiedBoard = new(board);
                    copiedBoard.MovePieceToNewPositionOnBoard(startRow, startCol, newRow, newCol);
                    copiedBoard.UpdateUnderAttackPositionsCurrentPlayerIs(oppenentColor, true);
                    if (copiedBoard.IsUnderAttack(newRow, newCol) == false)
                    {
                        this.AddToPossibleMoveList(newRow, newCol);
                        
                    }
                }
            }
        }


        // Note this is almost the same as the moves function but this is only for attacking squars
        // This is added because if you fill the move list with the attack list method you get an infinite loop
        public override void PossibleAttackSquaresKing(Board board, bool skipClear = false)
        {
            if (!skipClear)
            {
                AttackList.Clear();
            }
            
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
                CalculateAttackInDirection(board, startRow, startCol, rowChange, colChange);
            }
        }

        public void CalculateAttackInDirection(Board board, int startRow, int startCol, int rowChange, int colChange)
        {
            // Gets the start position
            var piece = board.GetPieceAt(startRow, startCol);

            // New position
            int newRow = startRow + rowChange;
            int newCol = startCol + colChange;
            if (board.IsWithinBounds(newRow, newCol))
            {
                var pieceAtPosition = board.GetPieceAt(newRow, newCol);
                // if empty or enemy
                if (pieceAtPosition.Name == "Empty" || pieceAtPosition.Color != this.Color)
                {
                    this.AddToAttackList(newRow, newCol);
                }
            }
        }

    }
}
