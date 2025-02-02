using System.Linq;

namespace ChessBlazorServer.Classes
{
    public class ChessPiece
    {
        public string Name { get; set; }
        public string SVGName { get; set; }
        public string Color { get; set; }
        public (int Row, int Col) Position { get; set; }
        public bool IsCaptured { get; set; }
        public bool HasMoved { get; set; }

        // Possible moves list for a piece
        public List<(int, int)> MoveList { get; set; }

        // All the squares a piece can attack -> no logic just all squares
        public List<(int, int)> AttackList { get; set; }

        // All the enemy pieces a piece can attack
        public List<(int, int)> AttackingPieceList { get; set; }


        public ChessPiece(string name, string svgName, string color, int row, int col)
        {
            Name = name;
            SVGName = svgName;
            Color = color;
            Position = (row, col);
            IsCaptured = false;
            HasMoved = false;
            MoveList = new List<(int, int)>();
            AttackList = new List<(int, int)>();
            AttackingPieceList = new List<(int, int)>();
        }

        // Placeholder Method to override
        public virtual void PossibleMoves(Board board)
        {
            
        }

        public virtual void PossiblePiecesToAttack(Board board)
        {

        }

        // Is used within specific piece classes to calculate the possible moves; can be overwriten at classes
        public virtual void CalculateMovesInDirection(Board board, int startRow, int startCol, int rowChange, int colChange)
        {
            CalculateInDirection(board, startRow, startCol, rowChange, colChange, isAttack: false, isAttackPiece: false);

            // !!!!!!!!!!!!!!! remove this after implementation of all attack calculations !!!!!!!!!!!!
            //CalculateInDirection(board, startRow, startCol, rowChange, colChange, isAttack: true, isAttackPiece: false);
        }

        // Is used within specific piece classes to calculate the possible attacks; can be overwriten at classes
        public virtual void CalculateAttacksInDirection(Board board, int startRow, int startCol, int rowChange, int colChange)
        {
            CalculateInDirection(board, startRow, startCol, rowChange, colChange, isAttack: true, isAttackPiece: false);
        }
        
        public virtual void CalculateAttackingPiecesInDirection(Board board, int startRow, int startCol, int rowChange, int colChange)
        {
            CalculateInDirection(board, startRow, startCol, rowChange, colChange, isAttack: false, isAttackPiece: true);
        }

        // generic algorithm for queen, rook and bishop; other pieces uses own implementation
        private void CalculateInDirection(Board board, int startRow, int startCol, int rowChange, int colChange, bool isAttack, bool isAttackPiece)
        {
            int row = startRow + rowChange;
            int col = startCol + colChange;

            while (board.IsWithinBounds(row, col))
            {
                ChessPiece pieceAtPosition = board.GetPieceAt(row, col);

                if (isAttackPiece)
                {
                    if (pieceAtPosition.Color == this.Color)
                    {
                        break;
                    }
                    
                    if (pieceAtPosition.Name != "Empty" &&  pieceAtPosition.Color != this.Color)
                    {
                        
                        this.AddToAttackingPieceList(row, col);
                        break;
                    }

                }
                else
                {
                    // No Piece
                    if (pieceAtPosition == null || pieceAtPosition.Name == "Empty")
                    {
                        if (isAttack)
                        {
                            this.AddToAttackList(row, col);
                        }
                        else
                        {
                            this.AddToPossibleMoveList(row, col);
                        }
                    }
                    // Opponent
                    else if (pieceAtPosition.Color != this.Color)
                    {
                        if (isAttack)
                        {
                            this.AddToAttackList(row, col);
                        }
                        else
                        {
                            this.AddToPossibleMoveList(row, col);
                        }
                        break;
                    }
                    // Own Piece
                    else
                    {
                        break;
                    }
                }
                
                // Update row and col for the next iteration
                row += rowChange;
                col += colChange;
            }
        }


        // Only used for pawn
        public virtual void ChangeEnPassantStatus(bool status)
        {

        }
        // Only used for pawn
        public virtual void ChangeEnPassantStatusForAllPawnsToFalse(Board board)
        {
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    ChessPiece piece = board.GetPieceAt(row, col);
                    if (piece is Pawn)
                    {
                        piece.ChangeEnPassantStatus(false);
                    }
                }
            }
        }


        // onlu used for king
        public virtual void PossibleAttackSquaresKing(Board board, bool skipClear = false)
        {

        }

        // Method to move a piece to a new position
        public void NewPosition(int newRow, int newCol)
        {
            Position = (newRow, newCol);
        }

        // Method to capture this piece
        public void CaptureThisPiece()
        {
            IsCaptured = true;
        }

        // method to mark a piece as moved
        public void MarkAsMoved()
        {
            HasMoved = true;
        }

        // method to add moves to the list
        public void AddToPossibleMoveList(int row, int col)
        {
            MoveList.Add((row, col));
        }

        // method to add attacks to the list
        public void AddToAttackList(int row, int col)
        {
            AttackList.Add((row, col));
        }
        
        // method to add attackingPiece list
        public void AddToAttackingPieceList(int row, int col)
        {
            AttackingPieceList.Add((row, col));
        }



        public virtual ChessPiece Clone()
        {
            ChessPiece clonedPiece = new ChessPiece(this.Name, this.SVGName, this.Color, this.Position.Row, this.Position.Col)
            {
                
                IsCaptured = this.IsCaptured,
                HasMoved = this.HasMoved,
                MoveList = new List<(int, int)>(this.MoveList), 
                AttackList = new List<(int, int)>(this.AttackList),
                AttackingPieceList = new List<(int, int)>(this.AttackingPieceList) 
            };

            return clonedPiece;
        }


        // Method to check if a move is valid
        public bool IsMoveValid((int, int) MoveToLocation)
        {           
            return this.MoveList.Contains(MoveToLocation);       
        }

        public void DebugPrintMoveListToConsole()
        {
            foreach(var move in this.MoveList)
            {
                Console.WriteLine(move.ToString());
            }
        }

        public void DebugPrintAttackListToConsole()
        {
            foreach (var attack in this.AttackList)
            {
                Console.WriteLine(attack.ToString());
            }
        }  
        
        public void DebugPrintAttackPieceListToConsole()
        {
            foreach (var attack in this.AttackingPieceList)
            {
                Console.WriteLine(attack.ToString());
            }
        }

        // Moves to remove and keep for protecting the king
        public void KeepOrRemoveMovesForProtectingKing(Board board)
        {
            List<(int, int)> movesToRemove = new ();

            if (MoveList.Count != 0)
            {
                foreach (var move in MoveList)
                {
                    MoveSimulator copiedBoard = new(board);
                    string opponentColor = (this.Color == "white") ? "black" : "white";
                    copiedBoard.MovePieceToNewPositionOnBoard(Position.Row, Position.Col, move.Item1, move.Item2);
                    copiedBoard.UpdateUnderAttackPositionsOpponentPlayerIs(opponentColor, true);
                    var kingPosition = board.GetPosOfPiece("K", this.Color);

                    if (copiedBoard.IsUnderAttack(kingPosition.Item1, kingPosition.Item2))
                    {
                        movesToRemove.Add(move);
                    }
                }

                foreach (var move in movesToRemove)
                {
                    MoveList.Remove(move);
                }
            }
        }

        // Remove attackPieces that are not in moves
        public void RemoveAttackingPiecesWhenNotInMoveList(Board board)
        {
            List<(int, int)> movesToRemove = new();

            if (AttackingPieceList.Count != 0)
            {
                foreach (var attackPiece in AttackingPieceList)
                {
                    if (!MoveList.Contains(attackPiece))
                    {
                        movesToRemove.Add(attackPiece);
                    }
                }


                foreach (var move in movesToRemove)
                {
                    AttackingPieceList.Remove(move);
                }
            }


        }

        /*
        public virtual void FillAttackThisPiecesList(Board board)
        {          
            this.AttackingPieceList.Clear();
            string oppenentColor = "";
            if (this.Color == "white")
            {
                oppenentColor = "black";
            }
            if (this.Color == "black")
            {
                oppenentColor = "white";
            }
            //board.UpdateUnderAttackPositionsCurrentPlayerIs(oppenentColor);
            foreach(var attack in this.AttackList)
            {
                ChessPiece chessPiece = board.GetPieceAt(attack.Item1, attack.Item2);
                if(chessPiece.Color == oppenentColor)
                {
                    AttackingPieceList.Add(attack);
                }
            }
        }
        */

    }
}
