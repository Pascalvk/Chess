// See https://aka.ms/new-console-template for more information
using ChessBlazorServer.Classes;

Console.WriteLine("Hello, World!");



NotationConverter converter = new();
converter.DebugConsoleDictionraries();

Console.WriteLine(converter.algebraicToCoord["A1"]);