using System.IO.Pipelines;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ChessBlazorServer.Classes
{
    public class Board
    {
        public const int BoardSize = 8;
        private ChessPiece[,] grid;
        public List<(int row, int col)> UnderAttackPositions { get; set; }
        public ChessPiece EmptyChessPiece { get; set; }
        public bool IsInCheck;
        public string IsInCheckColor;
        public bool IsCheckMate;
        public string IsCheckMatedColor;
       
        public Board()
        {
            grid = new ChessPiece[BoardSize, BoardSize];
            UnderAttackPositions = new List<(int, int)>();
            EmptyChessPiece = new ChessPiece("Empty", "", "", -1, -1);
            IsInCheck = false;
            IsInCheckColor = "";
            IsCheckMate = false;
            IsCheckMatedColor = "";
            SetupBoard();
        }

        // Empty constructor (used for cloning)
        public Board(bool empty)
        {
            grid = new ChessPiece[BoardSize, BoardSize];
        }

        public void SetupBoard()
        {
            
            // White pieces
            grid[6, 0] = new Pawn("P", "wp", "white", 6, 0);
            grid[6, 1] = new Pawn("P", "wp", "white", 6, 1);
            grid[6, 2] = new Pawn("P", "wp", "white", 6, 2);
            grid[6, 3] = new Pawn("P", "wp", "white", 6, 3);
            grid[6, 4] = new Pawn("P", "wp", "white", 6, 4);
            grid[6, 5] = new Pawn("P", "wp", "white", 6, 5);
            grid[6, 6] = new Pawn("P", "wp", "white", 6, 6);
            
            grid[6, 7] = new Pawn("P", "wp", "white", 6, 7);
            
            grid[7, 0] = new Rook("R", "wr", "white", 7, 0);
            grid[7, 7] = new Rook("R", "wr", "white", 7, 7);
            
            grid[7, 1] = new Knight("N", "wn", "white", 7, 1);
            grid[7, 6] = new Knight("N", "wn", "white", 7, 6);
                     
            grid[7, 2] = new Bishop("B", "wb", "white", 7, 2);
            grid[7, 5] = new Bishop("B", "wb", "white", 7, 5);
            
            grid[7, 3] = new Queen("Q", "wq", "white", 7, 3);
            
            grid[7, 4] = new King("K", "wk", "white", 7, 4);
            
            // Black pieces
            grid[1, 0] = new Pawn("P", "bp", "black", 1, 0);
            grid[1, 1] = new Pawn("P", "bp", "black", 1, 1);
            grid[1, 2] = new Pawn("P", "bp", "black", 1, 2);
            grid[1, 3] = new Pawn("P", "bp", "black", 1, 3);
            grid[1, 4] = new Pawn("P", "bp", "black", 1, 4);
            grid[1, 5] = new Pawn("P", "bp", "black", 1, 5);
            grid[1, 6] = new Pawn("P", "bp", "black", 1, 6);
            grid[1, 7] = new Pawn("P", "bp", "black", 1, 7);
            
            grid[0, 0] = new Rook("R", "br", "black", 0, 0);
            grid[0, 7] = new Rook("R", "br", "black", 0, 7);
            
            grid[0, 1] = new Knight("N", "bn", "black", 0, 1);
            grid[0, 6] = new Knight("N", "bn", "black", 0, 6);

            grid[0, 2] = new Bishop("B", "bb", "black", 0, 2);
            grid[0, 5] = new Bishop("B", "bb", "black", 0, 5);
            
            grid[0, 3] = new Queen("Q", "bq", "black", 0, 3);
            
            grid[0, 4] = new King("K", "bk", "black", 0, 4);
            


            //grid[1, 1] = new King("K", "wk", "white", 1, 1);
        }

        // Moves a piece to a new location; Note: No logic checks inside this method
        public void MovePieceToNewPositionOnBoard(int currentRow, int currentCol, int newRow, int newCol)
        {
            grid[newRow, newCol] = grid[currentRow, currentCol];
            grid[currentRow, currentCol] = null;
            grid[newRow, newCol].NewPosition(newRow, newCol);
        }

        // Just place a piece at the location
        public void SetPieceAt(int row, int col, ChessPiece Piece)
        {
            grid[row, col] = Piece;
        }

        public void RemovePieceAt(int row, int col)
        {
            grid[row, col] = EmptyChessPiece;
        }

        public ChessPiece GetEnPassantablePiece()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    ChessPiece piece = grid[row, col];
                    if (piece is Pawn pawn && pawn.CanBeTakenEnPassant)
                    {
                        return piece;
                    }
                }
            }
            return null; 
        }

        // Is the move withing bounds of the board
        public bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < BoardSize && col >= 0 && col < BoardSize;
        }

        // Gets a piece from a cord
        public ChessPiece GetPieceAt(int row, int col)
        {
            ChessPiece piece = grid[row, col];
            if (piece == null)
            {
                piece = EmptyChessPiece;
            }
            return piece;
        }

        public (int,int) GetPosOfPiece( string name, string color)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {

                    if (grid[row, col] != null && grid[row, col].Name == name && grid[row, col].Color == color)
                    {
                        return (row, col);
                    }
                }
            }
            return (-1, -1);
        }

        // Method to fill the list of attacked positions to current board; No uses yet; maybe use as helper
        public void UpdateUnderAttackPositionsOpponentPlayerIs(string attackingColor = "", bool skipKing = false)
        {            
            UnderAttackPositions.Clear();
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var piece = GetPieceAt(row, col);
                    if (piece != null)
                    {
                        if (piece.Color == attackingColor)
                        {
                            // Skips King; prevents infinite loop;
                            // but makes sure to add attack squares so a king cant move into a king                           
                            if (skipKing == true && piece is King)
                            {
                                UnderAttackPositions.AddRange(piece.AttackList);
                                continue;
                            }
                            
                            piece.PossibleMoves(this);
                            UnderAttackPositions.AddRange(piece.AttackList);
                        }
                    }
                }
            }
            // Remove duplicates
            UnderAttackPositions = UnderAttackPositions.Distinct().ToList();
        }

        public bool IsUnderAttack(int row, int col)
        {      
            return UnderAttackPositions.Contains((row, col));
        }


        // Is the king of the current player in check?
        public void IsKingInCheck(string colorOfCurrentPlayer, string colorOfOpponentPlayer)
        {
            UpdateUnderAttackPositionsOpponentPlayerIs(colorOfOpponentPlayer);
            var kingPos = GetPosOfPiece("K", colorOfCurrentPlayer);
            if (UnderAttackPositions.Contains(kingPos))
            {
                IsInCheck = true;
                IsInCheckColor = colorOfCurrentPlayer;
            }
            else
            {
                IsInCheck = false;
                IsInCheckColor = "";
            }

        }

        // Is the king of the current player checkmated?
        public void IsKingCheckMated(string colorOfCurrentPlayer)
        {
            if (IsInCheck)
            {
                var kingPos = GetPosOfPiece("K", colorOfCurrentPlayer);
                ChessPiece king = GetPieceAt(kingPos.Item1, kingPos.Item2);
                king.PossibleMoves(this);
                if (king.MoveList.Count == 0)
                {
                    IsCheckMate = true;
                    IsCheckMatedColor = king.Color;
                }
            }
        }

        public void RemoveEnPassantStatusForColor(string colorToDeleteEnPassantFrom)
        {
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    ChessPiece piece = GetPieceAt(row, col);
                    if (piece is Pawn && piece.Color == colorToDeleteEnPassantFrom)
                    {
                        piece.ChangeEnPassantStatus(false);
                    }
                }
            }
        }


        // Use to print a board to console
        public void DebugPrintBoard()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    ChessPiece piece = grid[row, col];
                    if (piece != null)
                    {
                        if (piece.Name == "Empty")
                        {
                            piece = null;
                        }                     
                    }
                    if (piece == null)
                    {
                        Console.Write(". "); 
                    }
                    else
                    {
                        Console.Write(piece.Name + " "); 
                    }
                }
                Console.WriteLine(); 
            }
        }

        public void DebugPrintUnderAttackList()
        {
            foreach (var attackedPos in UnderAttackPositions)
            {
                Console.WriteLine(attackedPos);
            }
        }

        public void DebugGetRawInfoAtPosition()
        {
            for (int temp_row = 0; temp_row < BoardSize; temp_row++)
            {
                for (int temp_col = 0; temp_col < BoardSize; temp_col++)
                {
                    if (grid[temp_row, temp_col] == null)
                    {
                        Console.WriteLine($"Piece is null, on position {temp_row},{temp_col}");
                    }
                    else
                    {
                        Console.WriteLine("No Piece is null");
                    }
                }
            }         

        }
    }
}
