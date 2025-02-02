using System.Linq;

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
            AttackList.Clear();
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

            // Castling
            if (!this.HasMoved)
            {
                // Gets the start position
                var piece = board.GetPieceAt(startRow, startCol);

                if (piece.Position == (7,4) || piece.Position == (0, 4))
                {
                    // Right               
                    if (board.GetPieceAt(startRow, startCol + 1).Name == "Empty" && board.GetPieceAt(startRow, startCol + 2).Name == "Empty")
                    {
                        if (board.GetPieceAt(startRow, startCol + 3).Name == "R" && !board.GetPieceAt(startRow, startCol + 3).HasMoved)
                        {
                            string opponentColor = (this.Color == "white") ? "black" : "white";
                            board.UpdateUnderAttackPositionsOpponentPlayerIs(opponentColor, true);
                            if (!board.UnderAttackPositions.Contains((startRow, startCol + 1)) && !board.UnderAttackPositions.Contains((startRow, startCol + 2)))
                            {
                                this.AddToPossibleMoveList(startRow, startCol + 2);
                            }
                        }
                    }

                    // Left
                    if (board.GetPieceAt(startRow, startCol - 1).Name == "Empty" && board.GetPieceAt(startRow, startCol - 2).Name == "Empty" && board.GetPieceAt(startRow, startCol - 3).Name == "Empty")
                    {
                        if (board.GetPieceAt(startRow, startCol - 4).Name == "R" && !board.GetPieceAt(startRow, startCol - 4).HasMoved)
                        {
                            string opponentColor = (this.Color == "white") ? "black" : "white";
                            board.UpdateUnderAttackPositionsOpponentPlayerIs(opponentColor, true);
                            if (!board.UnderAttackPositions.Contains((startRow, startCol - 1)) && !board.UnderAttackPositions.Contains((startRow, startCol - 2)) && !board.UnderAttackPositions.Contains((startRow, startCol - 3)))
                            {
                                this.AddToPossibleMoveList(startRow, startCol - 2);
                            }
                        }
                    }
                }

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
                    this.AddToAttackList(newRow, newCol);

                    string opponentColor = (this.Color == "white") ? "black" : "white";
                    MoveSimulator copiedBoard = new(board);
                    copiedBoard.MovePieceToNewPositionOnBoard(startRow, startCol, newRow, newCol);
                    copiedBoard.UpdateUnderAttackPositionsOpponentPlayerIs(opponentColor, true);
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

        public override void PossiblePiecesToAttack(Board board)
        {
            AttackingPieceList.Clear();
            
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
                CalculateAttackingPiecesInDirection(board, startRow, startCol, rowChange, colChange);
            }
            
        }

        public void CalculateAttackingPiecesInDirection(Board board, int startRow, int startCol, int rowChange, int colChange)
        {
            
            // Gets the start position
            var piece = board.GetPieceAt(startRow, startCol);

            // New position
            int newRow = startRow + rowChange;
            int newCol = startCol + colChange;
            if (board.IsWithinBounds(newRow, newCol))
            {
                var pieceAtPosition = board.GetPieceAt(newRow, newCol);
 
                if (pieceAtPosition.Name != "Empty" && pieceAtPosition.Color != this.Color)
                {
                    if (MoveList.Contains((newRow, newCol)))
                    {
                        this.AddToAttackingPieceList(newRow, newCol);
                    }

                }
            }
        }

    }
}
