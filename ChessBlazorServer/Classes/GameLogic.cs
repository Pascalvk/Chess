using System.Drawing;

namespace ChessBlazorServer.Classes
{
    public class GameLogic
    {
        private Board board;
        public string currentPlayer;
        public List<(int, int)> LastMoveFromToLocation;


        public GameLogic(Board board)
        {
            this.board = board;
            this.currentPlayer = "white";
            LastMoveFromToLocation = new List<(int, int)> { (-1, -1), (-1, -1) };

        }

        public string opponentPlayer => currentPlayer == "white" ? "black" : "white";

        public void MovePiece(ChessPiece piece, (int moveToRow, int moveToCol) moveToLocation, string promotionChoice)
        {
            LastMoveFromToLocation.Clear();
            LastMoveFromToLocation.Add(piece.Position);
            LastMoveFromToLocation.Add(moveToLocation);

            (int currentRow, int currentCol) = piece.Position;

            HandleSpecialMoves(piece, piece.Position, moveToLocation, promotionChoice);
            board.MovePieceToNewPositionOnBoard(currentRow, currentCol, moveToLocation.moveToRow, moveToLocation.moveToCol);
            piece.MarkAsMoved();
            piece.NewPosition(moveToLocation.moveToRow, moveToLocation.moveToCol);
            board.RemoveEnPassantStatusForColor(opponentPlayer);

            currentPlayer = currentPlayer == "white" ? "black" : "white";



        }

        public void HandleSpecialMoves(ChessPiece piece, (int currentRow, int currentCol) currentPos, (int moveToRow, int moveToCol) moveToLocation, string promotionChoice)
        {
            // Pawn
            if (piece is Pawn pawn)
            {
                // En passant
                ChessPiece enPassantablePawn = board.GetEnPassantablePiece();
                if (currentPos.currentCol != moveToLocation.moveToCol && enPassantablePawn != null)
                {
                    if (piece.AttackList.Contains(enPassantablePawn.Position) && moveToLocation.moveToCol == enPassantablePawn.Position.Col)
                    {
                        board.RemovePieceAt(enPassantablePawn.Position.Row, enPassantablePawn.Position.Col);
                    }
                        
                }

                // 2 steps forward move
                if (Math.Abs(moveToLocation.moveToRow - currentPos.currentRow) == 2)
                {
                    piece.ChangeEnPassantStatus(true);
                }



                // Promotion
                if (moveToLocation.moveToRow == 0 || moveToLocation.moveToRow == 7)
                {
                    string currentColor = moveToLocation.moveToRow == 0 ? "white" : "black";
                    string currentSVGColor = moveToLocation.moveToRow == 0 ? "w" : "b";

                    if (promotionChoice == "R")
                    {
                        Rook rook = new Rook("R", $"{currentSVGColor}r", currentColor, piece.Position.Row, piece.Position.Col);
                        board.SetPieceAt(piece.Position.Row, piece.Position.Col, rook);
                    }
                    else if (promotionChoice == "N")
                    {
                        Knight knight = new Knight("N", $"{currentSVGColor}n", currentColor, piece.Position.Row, piece.Position.Col);
                        board.SetPieceAt(piece.Position.Row, piece.Position.Col, knight);
                    }
                    else if (promotionChoice == "B")
                    {
                        Bishop bishop = new Bishop("B", $"{currentSVGColor}b", currentColor, piece.Position.Row, piece.Position.Col);
                        board.SetPieceAt(piece.Position.Row, piece.Position.Col, bishop);
                    }
                    else 
                    {
                        Queen queen = new Queen("Q", $"{currentSVGColor}q", currentColor, piece.Position.Row, piece.Position.Col);
                        board.SetPieceAt(piece.Position.Row, piece.Position.Col, queen);
                    }
                    

                }
            }

            // King
            if (piece is King king)
            {
                // 2 steps to the side
                if (Math.Abs(moveToLocation.moveToCol- currentPos.currentCol) == 2)
                {
                    if (moveToLocation.moveToCol == 2)
                    {
                        if (currentPlayer == "white")
                        {
                            board.MovePieceToNewPositionOnBoard(7, 0, 7, 3);
                            board.GetPieceAt(7, 3).MarkAsMoved();
                        }
                        else
                        {
                            board.MovePieceToNewPositionOnBoard(0, 0, 0, 3);
                            board.GetPieceAt(0, 3).MarkAsMoved();
                        }
                    }
                    else if (moveToLocation.moveToCol == 6)
                    {
                        if (currentPlayer == "white")
                        {
                            board.MovePieceToNewPositionOnBoard(7, 7, 7, 5);
                            board.GetPieceAt(7, 5).MarkAsMoved();
                        }
                        else
                        {
                            board.MovePieceToNewPositionOnBoard(0, 7, 0, 5);
                            board.GetPieceAt(0, 5).MarkAsMoved();
                        }
                    }
                }
            }

        }

    }
}
