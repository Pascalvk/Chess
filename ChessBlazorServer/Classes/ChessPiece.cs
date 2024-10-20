namespace ChessBlazorServer.Classes
{
    public class ChessPiece
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public (string Character, string Number) Position { get; set; }
        public bool isCaptured { get; set; }

        public ChessPiece(string name, string color, string character, string number)
        {
            Name = name;
            Color = color;
            Position = (character, number);
            isCaptured = false;
        }


        // Method to move a piece to a new position
        public void Move(string newCharacter, string newNumber)
        {
            Position = (newCharacter, newNumber);
        }

        // Method to capture a piece
        public void Capture()
        {
            isCaptured = true;
        }
    }
}
