namespace ChessBlazorServer.Classes
{
    public class NotationConverter
    {
        public readonly Dictionary<(int, int), string> coordToAlgebraic;
        public readonly Dictionary<string, (int, int)> algebraicToCoord;
        
       // Creates dictionairies for converting coord to algebra and vice versa
        public NotationConverter() 
        { 
            coordToAlgebraic = new Dictionary<(int, int), string>();
            algebraicToCoord = new Dictionary<string, (int, int)>();
            InitializeDictionaries();
        }

        public void InitializeDictionaries()
        {
            for (int rank = 0; rank < 8; rank++)
            {
                for (int file = 0; file < 8; file++)
                {
                    string algebraic = ConvertCordsToAlgebraicNotation(file, rank);
                    coordToAlgebraic.Add((file, rank), algebraic);
                    algebraicToCoord.Add(algebraic, (file, rank));
                }
            }
        }


        // Converts from number to algebra
        public string ConvertCordsToAlgebraicNotation(int xCord, int yCord)
        {
            char file = (char)('A' + xCord); // Convert file number to corresponding letter
            int rank = 8 - yCord; // Omrekenen naar schaaknotatie
            return $"{file}{rank}";
        }


        // Use this for debugging; writes all values to console
        public void DebugConsoleDictionraries()
        {
            Console.WriteLine("Coords To Algebra");
            foreach (var value in this.coordToAlgebraic)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("Algebra To Cords");
            foreach (var value in this.algebraicToCoord)
            {
                Console.WriteLine(value);
            }
            
        }

    }
}
