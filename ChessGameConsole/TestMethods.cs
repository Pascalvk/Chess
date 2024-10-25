using ChessBlazorServer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameConsole
{
    public class TestMethods
    {

        public void EmptyBoardForTestingAttackedPositions()
        {
            Board board = new();
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    board.SetPieceAt(row, col, null);
                }
            }
            
            board.SetPieceAt(6, 0, new Pawn("P", "wp", "white", 6, 0));
            board.SetPieceAt(4, 1, new King("K", "bq", "black", 4, 1));
            board.DebugPrintBoard();
            board.UpdateUnderAttackPositions();
            ChessPiece bk = board.GetPieceAt(4, 1);
            bk.PossibleMoves(board);
            bk.DebugPrintMoveListToConsole();

            //board.DebugPrintUnderAttackList();

        }
    }
}
