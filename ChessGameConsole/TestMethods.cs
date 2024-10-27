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
            
            board.SetPieceAt(2, 2, new King("W", "wk", "white", 2, 2));
            board.SetPieceAt(0, 0, new Queen("B", "bk", "black", 0, 0));
            board.DebugPrintBoard();
            ChessPiece wk = board.GetPieceAt(2, 2);
            ChessPiece bk = board.GetPieceAt(0, 0);
            wk.PossibleMoves(board);
            wk.DebugPrintMoveListToConsole();
            bk.DebugPrintAttackListToConsole();

          
            Console.WriteLine("END");


        }

        public void TestKingAgainstPawns()
        {
            Board board = new();
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    board.SetPieceAt(row, col, null);
                }
            }

            /*
            board.SetPieceAt(6, 0, new Pawn("P", "wp", "white", 6, 0));
            board.SetPieceAt(6, 1, new Pawn("P", "wp", "white", 6, 1));
            board.SetPieceAt(6, 2, new Pawn("P", "wp", "white", 6, 2));
            board.SetPieceAt(6, 3, new Pawn("P", "wp", "white", 6, 3));
            board.SetPieceAt(6, 4, new Pawn("P", "wp", "white", 6, 4));
            board.SetPieceAt(6, 5, new Pawn("P", "wp", "white", 6, 5));
            board.SetPieceAt(6, 6, new Pawn("P", "wp", "white", 6, 6));
            board.SetPieceAt(6, 7, new Pawn("P", "wp", "white", 6, 7));
            */
            //board.SetPieceAt(6, 0, new Pawn("P", "wp", "white", 6, 0));
            //board.SetPieceAt(2, 0, new Pawn("P", "wp", "white", 2, 0));
            board.SetPieceAt(6, 2, new Rook("R", "wr", "white", 6, 2));


            board.SetPieceAt(4, 2, new King("K", "bk", "black", 4, 2));


            board.DebugPrintBoard();
            ChessPiece wp = board.GetPieceAt(6, 0);
            ChessPiece wr = board.GetPieceAt(6, 2);
            ChessPiece bk = board.GetPieceAt(4, 2);

            //board.UpdateUnderAttackPositionsAttackerIs(board, "white");
            //board.DebugPrintUnderAttackList();
            Console.WriteLine();
            Console.WriteLine("Hierna");
            bk.PossibleMoves(board);
            Console.WriteLine("===============");
            bk.DebugPrintMoveListToConsole();
            //board.UpdateUnderAttackPositionsAttackerIs(board, "white");

            //wr.DebugPrintAttackListToConsole();
            //wk.DebugPrintAttackListToConsole();


        }

        public void testAgainst2Kings()
        {
            Board board = new();
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    board.SetPieceAt(row, col, null);
                }
            }

            board.SetPieceAt(0, 0, new King("K", "wk", "white", 0, 0));
            board.SetPieceAt(3, 3, new King("K", "bk", "black", 3, 3));
            ChessPiece wk = board.GetPieceAt(0, 0);
            ChessPiece bk = board.GetPieceAt(3, 3);

            board.DebugPrintBoard();
            board.UpdateUnderAttackPositionsCurrentPlayerIs("black");
            bk.DebugPrintMoveListToConsole();

        }

        public void testAgainst2Rooks()
        {
            Board board = new();
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    board.SetPieceAt(row, col, null);
                }
            }

            board.SetPieceAt(0, 0, new Rook("R", "wr", "white", 0, 0));
            board.SetPieceAt(3, 3, new Rook("R", "br", "black", 3, 3));
            ChessPiece wr = board.GetPieceAt(0, 0);
            ChessPiece br = board.GetPieceAt(3, 3);

            board.DebugPrintBoard();
            board.UpdateUnderAttackPositionsCurrentPlayerIs("white");
            wr.DebugPrintMoveListToConsole();

        }

        public void testAgainst2KingsCloseby()
        {
            Board board = new();
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    board.SetPieceAt(row, col, null);
                }
            }

            board.SetPieceAt(0, 0, new King("K", "wk", "white", 0, 0));
            board.SetPieceAt(2, 2, new King("K", "bk", "black", 2, 2));
            ChessPiece wk = board.GetPieceAt(0, 0);
            ChessPiece bk = board.GetPieceAt(2, 2);

            board.DebugPrintBoard();
            board.UpdateUnderAttackPositionsCurrentPlayerIs("black");
            bk.DebugPrintMoveListToConsole();


        }

        public void testAgainstQueenandKing()
        {
            Board board = new();
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    board.SetPieceAt(row, col, null);
                }
            }

            board.SetPieceAt(7, 4, new King("K", "wk", "white", 7, 4));
            board.SetPieceAt(4, 3, new Queen("Q", "wq", "white", 4, 3));
            board.SetPieceAt(0, 4, new King("K", "bk", "black", 0, 4));
            ChessPiece wk = board.GetPieceAt(7, 4);
            ChessPiece wq = board.GetPieceAt(4, 3);
            ChessPiece bk = board.GetPieceAt(0, 4);

            board.DebugPrintBoard();
            bk.PossibleMoves(board);
            bk.DebugPrintMoveListToConsole();
            Console.WriteLine("--------");
            bk.DebugPrintAttackListToConsole();

        }

        public void testInstantinCheck()
        {
            Board board = new();
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    board.SetPieceAt(row, col, null);
                }
            }

            board.SetPieceAt(7, 4, new King("K", "wk", "white", 7, 4));
            board.SetPieceAt(4, 4, new Queen("Q", "wq", "white", 4, 4));
            board.SetPieceAt(0, 4, new King("K", "bk", "black", 0, 4));
            ChessPiece wk = board.GetPieceAt(7, 4);
            ChessPiece wq = board.GetPieceAt(4, 4);
            ChessPiece bk = board.GetPieceAt(0, 4);

            board.DebugPrintBoard();
            bk.PossibleMoves(board);
            bk.DebugPrintMoveListToConsole();
            Console.WriteLine("--------");
            bk.DebugPrintAttackListToConsole();
        }
    }
}
