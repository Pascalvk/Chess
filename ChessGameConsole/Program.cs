// See https://aka.ms/new-console-template for more information
using ChessBlazorServer.Classes;

Console.WriteLine("Hello, World!");



NotationConverter converter = new();
converter.DebugConsoleDictionraries();

//Console.WriteLine(converter.algebraicToCoord["A1"]);
Console.WriteLine();

Board board = new();
board.DebugPrintBoard();

board.MovePieceToNewPositionOnBoard(7, 3, 5, 3);
board.DebugPrintBoard();
ChessPiece enne = board.GetPieceAt(5, 3);
Console.WriteLine(enne.Position);
Console.WriteLine(enne.Name);



enne.PossibleMoves(board);
Console.WriteLine(enne.MoveList.Count);
enne.DebugPrintMoveListToConsole();

