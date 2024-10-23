using System.Linq;

namespace ChessBlazorServer.Classes
{
    public class ChessPiece
    {
        public string Name { get; set; }
        public string SVGName { get; set; }
        public string Color { get; set; }
        public (int XCord, int YCord) Position { get; set; }
        public bool IsCaptured { get; set; }
        public bool HasMoved { get; set; }
        public List<(int, int)> MoveList { get; set; }

        public ChessPiece(string name, string svgName, string color, int xCord, int yCord)
        {
            Name = name;
            SVGName = svgName;
            Color = color;
            Position = (xCord, yCord);
            IsCaptured = false;
            HasMoved = false;
            MoveList = new List<(int, int)>();
        }

        // Method to move a piece to a new position
        public void NewPosition(int newxCord, int newyCord)
        {
            Position = (newxCord, newyCord);
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
