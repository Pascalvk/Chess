// See https://aka.ms/new-console-template for more information
using ChessBlazorServer.Classes;
using ChessGameConsole;
using System.Reflection;

Console.WriteLine("Hello, World!");
TestMethods testMethods = new TestMethods();

testMethods.testInstantinCheck();
/* 
testMethods.testAgainstQueenandKing();

/*
//testMethods.testAgainst2KingsCloseby();

Board board = new Board();
board.DebugPrintBoard();
ChessPiece wr = board.GetPieceAt(7, 7);
wr.PossibleMoves(board);
foreach (var move in wr.MoveList)
{
    Console.WriteLine(move);
}
Console.WriteLine("----------");
foreach (var attack in wr.AttackList)
{
    Console.WriteLine(attack);
}



//testMethods.testAgainst2Kings();
//testMethods.testAgainst2Rooks();
//testMethods.TestKingAgainstPawns();





/*
NotationConverter converter = new();
//converter.DebugConsoleDictionraries();

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

Console.WriteLine("Hallooooo");

board.MovePieceToNewPositionOnBoard(0, 4, 4, 4);
MoveSimulator Sim = new(board);
board.DebugPrintBoard();
Sim.DebugPrintBoard();
ChessPiece bk = board.GetPieceAt(4, 4);
bk.PossibleMoves(board);

board.DebugPrintBoard();
Sim.DebugPrintBoard();
Sim.DebugPrintBoard();
bk.DebugPrintMoveListToConsole();


Console.WriteLine();

TestMethods testMethods = new TestMethods();
testMethods.EmptyBoardForTestingAttackedPositions();
Console.WriteLine("-------------------------------------------------");
testMethods.TestKingAgainstPawns();


/*
board.MovePieceToNewPositionOnBoard(0, 4, 5, 4);
MoveSimulator Sim = new(board);
board.DebugPrintBoard();
Sim.DebugPrintBoard();
ChessPiece bk = board.GetPieceAt(5, 4);
bk.PossibleMoves(board);
bk.DebugPrintMoveListToConsole();





/*
Console.WriteLine("------------------");
board.UpdateUnderAttackPositions();
foreach (var moves in board.UnderAttackPositions)
{
    Console.WriteLine(moves);
}

/*
board.MovePieceToNewPositionOnBoard(0, 4, 6, 3);
ChessPiece bk = board.GetPieceAt(6, 3);
//bk.PossibleMoves(board);
board.DebugPrintBoard();
Console.WriteLine(bk.Position);
/*
board.UpdateUnderAttackPositions();

board.DebugPrintBoard();
bk.DebugPrintMoveListToConsole();
*/

/*
MoveSimulator Sim = new(board);
Console.WriteLine();
board.DebugPrintBoard();
Sim.DebugPrintBoard();
Sim.SimulateMove(0, 7, 5, 5);
Sim.DebugPrintBoard();
*/