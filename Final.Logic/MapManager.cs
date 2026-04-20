namespace Final.Logic
{
    public class MapManager
    {
        //Task 4
        private int numRows;
        private int numCols;
        private bool[,] canWalkHere;
        private string[,] whatGoesHere;
        private int playerRow;
        private int playerCol;

        private const string FIRE     = "🔥";
        private const string NOTHING  = "  ";
        private const string BALLIE   = "🟢";
        private const string BADSTUFF = "❌";

        public MapManager(int numRows, int numCols, int startRow, int startCol)
        {
            this.numRows = numRows;
            this.numCols = numCols;

            canWalkHere  = new bool[numRows, numCols];
            whatGoesHere = new string[numRows, numCols];

            for (int r = 0; r < numRows; r++)
                for (int c = 0; c < numCols; c++)
                {
                    canWalkHere[r, c]  = true;
                    whatGoesHere[r, c] = NOTHING;
                }

            playerRow = startRow;
            playerCol = startCol;

            BuildTheMap();
        }

        // Mapa xd
        private void BuildTheMap()
        {
            SlapAWallHere(1, 9, 8, 9);
            SlapAWallHere(11, 9, 18, 9);
            SlapAWallHere(7, 1, 7, 8);
            SlapAWallHere(7, 10, 7, 18);
            SlapAWallHere(13, 1, 13, 8);
            SlapAWallHere(13, 10, 13, 18);

            // Top left room
            PlonkAThing(3, 3); PlonkAThing(3, 4);
            PlonkAThing(5, 6); PlonkAThing(4, 2);

            // Top right room
            PlonkAThing(3, 12); PlonkAThing(3, 15);
            PlonkAThing(5, 13); PlonkAThing(4, 17);

            // Bottom left room
            PlonkAThing(15, 3); PlonkAThing(16, 5);
            PlonkAThing(17, 2); PlonkAThing(15, 7);

            // Bottom right room
            PlonkAThing(15, 12); PlonkAThing(16, 15);
            PlonkAThing(17, 17); PlonkAThing(15, 16);

            // Middle zone
            PlonkAThing(9, 4);  PlonkAThing(9, 14);
            PlonkAThing(10, 4); PlonkAThing(10, 14);
        }

        private void SlapAWallHere(int r1, int c1, int r2, int c2)
        {
            for (int r = r1; r <= r2; r++)
                for (int c = c1; c <= c2; c++)
                {
                    canWalkHere[r, c]  = false;
                    whatGoesHere[r, c] = FIRE;
                }
        }

        private void PlonkAThing(int row, int col)
        {
            canWalkHere[row, col]  = false;
            whatGoesHere[row, col] = BADSTUFF;
        }

        public void ToggleWalkable(int row, int col, bool yesOrNo)
        {
            if (StillOnTheMap(row, col))
                canWalkHere[row, col] = yesOrNo;
        }

        public (int row, int col) GetCurrentPosition()
        {
            return (playerRow, playerCol);
        }

        //Task 6
        public List<string> GetAvailableMoves()
        {
            var goThisWay = new List<string>();

            if (OkayToGoHere(playerRow - 1, playerCol)) goThisWay.Add("W");
            if (OkayToGoHere(playerRow + 1, playerCol)) goThisWay.Add("S");
            if (OkayToGoHere(playerRow, playerCol - 1)) goThisWay.Add("A");
            if (OkayToGoHere(playerRow, playerCol + 1)) goThisWay.Add("D");

            return goThisWay;
        }

        //Task 5
        public bool MovePlayer(string key)
        {
            int newRow = playerRow;
            int newCol = playerCol;

            switch (key.ToUpper())
            {
                case "W": newRow--; break;
                case "S": newRow++; break;
                case "A": newCol--; break;
                case "D": newCol++; break;
                default:
                    Console.WriteLine($"That key does nothing: {key}");
                    return false;
            }

            if (!OkayToGoHere(newRow, newCol))
            {
                Console.WriteLine("Something's in the way, can't go there.");
                return false;
            }

            playerRow = newRow;
            playerCol = newCol;
            return true;
        }

        // DisplayMap
        public void DisplayMap()
        {
            for (int r = 0; r < numRows; r++)
            {
                for (int c = 0; c < numCols; c++)
                {
                    bool itsBorder = (r == 0 || r == numRows - 1 || c == 0 || c == numCols - 1);
                    bool itsPlayer = (r == playerRow && c == playerCol);

                    if (itsPlayer)
                        Console.Write(BALLIE);
                    else if (itsBorder)
                        Console.Write(FIRE);
                    else
                        Console.Write(whatGoesHere[r, c]);
                }
                Console.WriteLine();
            }
        }

        private bool StillOnTheMap(int row, int col)
        {
            return row >= 0 && row < numRows && col >= 0 && col < numCols;
        }

        private bool OkayToGoHere(int row, int col)
        {
            return StillOnTheMap(row, col) && canWalkHere[row, col];
        }
    }
}