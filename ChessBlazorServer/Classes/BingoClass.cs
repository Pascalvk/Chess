namespace ChessBlazorServer.Classes
{
    public class BingoClass
    {
        int gridSize = 5;
        public int[,] grid = new int[5, 5];

        public int[,] CreateGrid()
        {            
            List<int> numbers = new List<int>();

            // Voeg 5 keer elk getal (1-5) toe aan de lijst
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    numbers.Add(i);
                }
            }

            Random rand = new Random();
            bool success = false;

            while (!success)
            {
                success = true;
                // Schud de lijst
                Shuffle(numbers, rand);

                // Probeer het grid te vullen
                int index = 0;
                for (int row = 0; row < gridSize; row++)
                {
                    for (int col = 0; col < gridSize; col++)
                    {
                        grid[row, col] = numbers[index++];
                    }
                }

                // Valideer het grid volgens de nieuwe regels
                if (!IsValid(grid, gridSize))
                {
                    success = false;
                }
            }
            return grid;
        }

        // Functie om de lijst te schudden
        static void Shuffle(List<int> list, Random rand)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                int temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        // Valideer het grid volgens de nieuwe regels
        static bool IsValid(int[,] grid, int gridSize)
        {
            int overlapCount = 0; // Tel het aantal overlappen (fenomenen)
            int pairsOfTwo = 0;   // Tel het aantal pairs van 2 gelijke getallen naast elkaar

            // Controleer rijen
            for (int row = 0; row < gridSize; row++)
            {
                Dictionary<int, int> rowCounts = new Dictionary<int, int>();
                for (int col = 0; col < gridSize; col++)
                {
                    int current = grid[row, col];

                    // Tel hoe vaak elk getal voorkomt in de rij
                    if (rowCounts.ContainsKey(current))
                    {
                        rowCounts[current]++;
                    }
                    else
                    {
                        rowCounts[current] = 1;
                    }

                    // Controleer of er een paar van 2 gelijk getallen naast elkaar zijn
                    if (col > 0 && grid[row, col] == grid[row, col - 1])
                    {
                        pairsOfTwo++;
                    }
                }

                // Controleer of er meer dan 2 van hetzelfde getal in de rij staan
                foreach (var count in rowCounts.Values)
                {
                    if (count > 2)
                    {
                        return false;
                    }
                }
            }

            // Controleer kolommen
            for (int col = 0; col < gridSize; col++)
            {
                Dictionary<int, int> colCounts = new Dictionary<int, int>();
                for (int row = 0; row < gridSize; row++)
                {
                    int current = grid[row, col];

                    // Tel hoe vaak elk getal voorkomt in de kolom
                    if (colCounts.ContainsKey(current))
                    {
                        colCounts[current]++;
                    }
                    else
                    {
                        colCounts[current] = 1;
                    }

                    // Controleer of er een paar van 2 gelijk getallen naast elkaar zijn
                    if (row > 0 && grid[row, col] == grid[row - 1, col])
                    {
                        pairsOfTwo++;
                    }
                }

                // Controleer of er meer dan 2 van hetzelfde getal in de kolom staan
                foreach (var count in colCounts.Values)
                {
                    if (count > 2)
                    {
                        return false;
                    }
                }
            }

            // Controleer het grid voor de overlapregel
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    int current = grid[row, col];

                    // Controleer hoeveel buren hetzelfde zijn
                    int sameCount = 0;

                    // Check links
                    if (col > 0 && grid[row, col - 1] == current)
                    {
                        sameCount++;
                    }

                    // Check boven
                    if (row > 0 && grid[row - 1, col] == current)
                    {
                        sameCount++;
                    }

                    // Check rechts
                    if (col < gridSize - 1 && grid[row, col + 1] == current)
                    {
                        sameCount++;
                    }

                    // Check onder
                    if (row < gridSize - 1 && grid[row + 1, col] == current)
                    {
                        sameCount++;
                    }

                    // Als meer dan 1 buur hetzelfde is, tel dit als een fenomeen
                    if (sameCount > 1)
                    {
                        overlapCount++;
                    }

                    // Als het totaal aantal fenomenen groter dan 2 is, faalt validatie
                    if (overlapCount > 2)
                    {
                        return false;
                    }
                }
            }

            // Controleer of er meer dan 2 paren van 2 gelijke getallen naast elkaar zijn
            if (pairsOfTwo > 2)
            {
                return false;
            }

            // Controleer de diagonalen
            if (!CheckDiagonals(grid, gridSize))
            {
                return false;
            }

            return true;
        }

        // Functie om de diagonalen te controleren
        static bool CheckDiagonals(int[,] grid, int gridSize)
        {
            // Hoofd-diagonaal van linksboven naar rechtsonder
            Dictionary<int, int> mainDiagonalCounts = new Dictionary<int, int>();
            // Anti-diagonaal van rechtsboven naar linksonder
            Dictionary<int, int> antiDiagonalCounts = new Dictionary<int, int>();

            for (int i = 0; i < gridSize; i++)
            {
                int mainDiagonalValue = grid[i, i]; // Hoofd-diagonaal
                int antiDiagonalValue = grid[i, gridSize - i - 1]; // Anti-diagonaal

                // Hoofd-diagonaal
                if (mainDiagonalCounts.ContainsKey(mainDiagonalValue))
                {
                    mainDiagonalCounts[mainDiagonalValue]++;
                }
                else
                {
                    mainDiagonalCounts[mainDiagonalValue] = 1;
                }

                // Anti-diagonaal
                if (antiDiagonalCounts.ContainsKey(antiDiagonalValue))
                {
                    antiDiagonalCounts[antiDiagonalValue]++;
                }
                else
                {
                    antiDiagonalCounts[antiDiagonalValue] = 1;
                }
            }

            // Controleer of er meer dan 2 gelijke getallen in de hoofd-diagonaal staan
            foreach (var count in mainDiagonalCounts.Values)
            {
                if (count > 2)
                {
                    return false;
                }
            }

            // Controleer of er meer dan 2 gelijke getallen in de anti-diagonaal staan
            foreach (var count in antiDiagonalCounts.Values)
            {
                if (count > 2)
                {
                    return false;
                }
            }

            return true;
        }

        public void DebugPrintCard()
        {
            // Print het grid
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Console.Write(grid[row, col] + " ");
                }
                Console.WriteLine();
            }
        }

    }
}
