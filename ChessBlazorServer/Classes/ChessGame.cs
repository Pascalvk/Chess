using System.Security.Cryptography;

namespace ChessBlazorServer.Classes
{
    public class ChessGame
    {
        public string currentPlayer {  get; set; }

        public ChessGame()
        {
            currentPlayer = "white";
        }

        public void GameLoop()
        {
            Board board = new();
            // Update board UnderAttackPositions
            board.UpdateUnderAttackPositionsCurrentPlayerIs(currentPlayer);


            // Make a move; from the piece movelist

            

            //

        }

        public void SwitchPlayers()
        {
            if (currentPlayer == "white")
            {
                currentPlayer = "black";
            }
            else
            {
                currentPlayer = "white";
            }
        }

    }
}
