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

        private const string FIRE = "🔥";
        private const string NOTHING = "  ";
        private const string BALLIE = "🟢";
        private const string BAD = "❌";

        public MapManager(int numRows, int numCols, int startRow, int startCol)
        {
            this.numRows = numRows;
            this.numCols = numCols;

            canWalkHere = new bool[numRows, numCols];
            whatGoesHere = new string[numRows, numCols];

            for (int r = 0; r < numRows; r++)
                for (int c = 0; c < numCols; c++)
                {
                    canWalkHere[r, c] = true;
                    whatGoesHere[r, c] = NOTHING;
                }

            playerRow = startRow;
            playerCol = startCol;

            BuildTheMap();
        }

        // Mapa xd
        private void BuildTheMap()
        {
            WallHere(1, 9, 8, 9);
            WallHere(11, 9, 18, 9);
            WallHere(7, 1, 7, 8);
            WallHere(7, 10, 7, 18);
            WallHere(13, 1, 13, 8);
            WallHere(13, 10, 13, 18);


            ObjectHere(3, 3); ObjectHere(3, 4);
            ObjectHere(5, 6); ObjectHere(4, 2);


            ObjectHere(3, 12); ObjectHere(3, 15);
            ObjectHere(5, 13); ObjectHere(4, 17);


            ObjectHere(15, 3); ObjectHere(16, 5);
            ObjectHere(17, 2); ObjectHere(15, 7);


            ObjectHere(15, 12); ObjectHere(16, 15);
            ObjectHere(17, 17); ObjectHere(15, 16);


            ObjectHere(9, 4); ObjectHere(9, 14);
            ObjectHere(10, 4); ObjectHere(10, 14);
        }

        private void WallHere(int r1, int c1, int r2, int c2)
        {
            for (int r = r1; r <= r2; r++)
                for (int c = c1; c <= c2; c++)
                {
                    canWalkHere[r, c] = false;
                    whatGoesHere[r, c] = FIRE;
                }
        }

        private void ObjectHere(int row, int col)
        {
            canWalkHere[row, col] = false;
            whatGoesHere[row, col] = BAD;
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
                    Console.WriteLine($"That key does nothing {key}");
                    return false;
            }

            if (!OkayToGoHere(newRow, newCol))
            {
                Console.WriteLine("Somethings in the way, can't go there");
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

        public string[,] GetGrid()
        {
            string[,] MapLooks = new string[numRows, numCols];

            for (int r = 0; r < numRows; r++)
                for (int c = 0; c < numCols; c++)
                {
                    bool itsBorder = (r == 0 || r == numRows - 1 || c == 0 || c == numCols - 1);
                    bool itsPlayer = (r == playerRow && c == playerCol);

                    if (itsPlayer)
                        MapLooks[r, c] = BALLIE;
                    else if (itsBorder)
                        MapLooks[r, c] = FIRE;
                    else
                        MapLooks[r, c] = whatGoesHere[r, c];
                }

            return MapLooks;
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