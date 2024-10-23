// See https://aka.ms/new-console-template for more information
using ChessBlazorServer.Classes;

Console.WriteLine("Hello, World!");



NotationConverter converter = new();
converter.DebugConsoleDictionraries();

//Console.WriteLine(converter.algebraicToCoord["A1"]);
Console.WriteLine();

Board board = new();
board.DebugPrintBoard();

board.MovePieceToNewPositionOnBoard(7, 1, 5, 3);
board.DebugPrintBoard();
ChessPiece enne = board.GetPieceAt(5, 3);
Console.WriteLine(enne.Position);
Console.WriteLine(enne.Name);



enne.PossibleMoves(board);
Console.WriteLine(enne.MoveList.Count);
(int x, int y) = enne.MoveList[0];
Console.WriteLine(x);
Console.WriteLine(y);
board.MovePieceToNewPositionOnBoard(5, 3, x, y);
board.DebugPrintBoard();
enne.DebugPrintMoveListToConsole();


ChessPiece wp = board.GetPieceAt(6, 0);
ChessPiece bp = board.GetPieceAt(1, 0);

board.MovePieceToNewPositionOnBoard(1, 0, 3, 0);
board.MovePieceToNewPositionOnBoard(6, 0, 3, 1);
board.DebugPrintBoard();
bp.ChangeEnPassantStatus(true);
wp.PossibleMoves(board);
wp.DebugPrintMoveListToConsole();