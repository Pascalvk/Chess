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
        public List<(int, int)> MoveList { get; set; }
        public List<(int, int)> AttackList { get; set; }
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

        // Is used within specific piece classes to calculate the possible moves; can be overwriten bij classes
        public virtual void CalculateMovesInDirection(Board board, int startRow, int startCol, int rowChange, int colChange)
        {
            int row = startRow + rowChange;
            int col = startCol + colChange;
            while (board.IsWithinBounds(row, col))
            {
                ChessPiece pieceAtPosition = board.GetPieceAt(row, col);
                // No piece
                if (pieceAtPosition == null || pieceAtPosition.Name == "Empty")
                {
                    this.AddToPossibleMoveList(row, col);
                    this.AddToAttackList(row, col);
                }
                // Opponent
                else if (pieceAtPosition.Color != this.Color)
                {
                    this.AddToPossibleMoveList(row, col);
                    this.AddToAttackList(row, col);
                    break;
                }
                // Own Piece
                else
                {
                    break;
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

        public virtual void ChangeEnPassantStatusForAllPawnsToFalse(Board board)
        {

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


    }
}
