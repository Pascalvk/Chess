using System.Linq;

namespace ChessBlazorServer.Classes
{
    public class ChessPiece
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public (string Character, string Number) Position { get; set; }
        public bool IsCaptured { get; set; }
        public bool HasMoved { get; set; }
        public List<(int, int)> MoveList { get; set; }

        public ChessPiece(string name, string color, string character, string number)
        {
            Name = name;
            Color = color;
            Position = (character, number);
            IsCaptured = false;
            HasMoved = false;
            MoveList = new List<(int, int)>();
        }

        // Method to move a piece to a new position
        public void NewPosition(string newCharacter, string newNumber)
        {
            Position = (newCharacter, newNumber);
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

        // Method to check if a move is valid
        public bool IsMoveValid(List<(int, int)> MoveList, (int, int) MoveToLocation)
        {           
            return MoveList.Contains(MoveToLocation);       
        }

    }
}
