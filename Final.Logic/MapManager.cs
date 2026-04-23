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

        private const string WALL = "⬛";
        private const string NOTHING = "  ";
        private const string BALLIE = "🟢";
        private const string FIRE = "🔥";
        private const string EXIT = "🚪";
        private const string KEY = "🗝️";

        private bool levelWon = false;
        private bool levelLost = false;
        private bool hasKey = false;

        public bool DidWin() => levelWon;
        public bool DidLose() => levelLost;
        public bool HasKey() => hasKey;

        public MapManager(int numRows, int numCols, int startRow, int startCol, bool isTutorial = false)
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

            if (!isTutorial)
                BuildTheMap();
        }

        // Mapa xd
        private void BuildTheMap()
        {
            // Llave 
            whatGoesHere[17, 3] = KEY;
            canWalkHere[17, 3] = true;
            // Paredes verticales con puertas
            WallHere(1, 9, 5, 9);
            WallHere(9, 9, 18, 9);

            // Paredes horizontales con puertas 
            WallHere(7, 1, 7, 4);
            WallHere(7, 6, 7, 9);
            WallHere(7, 10, 7, 13);
            WallHere(7, 16, 7, 18);

            WallHere(13, 1, 13, 3);
            WallHere(13, 6, 13, 9);
            WallHere(13, 10, 13, 14);
            WallHere(13, 17, 13, 18);

            // Fuego en habitación arriba izquierda
            ObjectHere(3, 3); ObjectHere(3, 4);
            ObjectHere(5, 6); ObjectHere(4, 2);

            // Fuego en habitación arriba derecha
            ObjectHere(3, 12); ObjectHere(3, 15);
            ObjectHere(5, 13); ObjectHere(4, 17);

            // Fuego en habitación abajo izquierda
            ObjectHere(15, 3); ObjectHere(16, 5);
            ObjectHere(17, 2); ObjectHere(15, 7);

            // Fuego en habitación abajo derecha
            ObjectHere(15, 12); ObjectHere(16, 15);
            ObjectHere(17, 17); ObjectHere(15, 16);

            // Fuego en zona central
            ObjectHere(9, 4); ObjectHere(9, 14);
            ObjectHere(10, 4); ObjectHere(10, 14);

            // Salida en esquina abajo derecha
            whatGoesHere[18, 18] = EXIT;
            canWalkHere[18, 18] = true;
        }

        public void TutorialObject(int row, int col, string emoji, bool lethal)
        {
            if (!StillOnTheMap(row, col)) return;
            whatGoesHere[row, col] = emoji;
            canWalkHere[row, col] = true;
        }

        private void WallHere(int r1, int c1, int r2, int c2)
        {
            for (int r = r1; r <= r2; r++)
                for (int c = c1; c <= c2; c++)
                {
                    canWalkHere[r, c] = false;
                    whatGoesHere[r, c] = WALL;
                }
        }

        private void ObjectHere(int row, int col)
        {
            canWalkHere[row, col] = true;
            whatGoesHere[row, col] = FIRE;
        }

        public void ToggleWalkable(int row, int col, bool yesOrNo)
        {
            if (StillOnTheMap(row, col))
                canWalkHere[row, col] = yesOrNo;
        }

        // Task 7
        public (int row, int col) GetCurrentPosition()
        {
            return (playerRow, playerCol);
        }

        // Task 6
        public List<string> GetAvailableMoves()
        {
            var goThisWay = new List<string>();

            if (OkayToGoHere(playerRow - 1, playerCol)) goThisWay.Add("W");
            if (OkayToGoHere(playerRow + 1, playerCol)) goThisWay.Add("S");
            if (OkayToGoHere(playerRow, playerCol - 1)) goThisWay.Add("A");
            if (OkayToGoHere(playerRow, playerCol + 1)) goThisWay.Add("D");

            return goThisWay;
        }

        // Task 5
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

            // Chequear si recogió la llave
            if (whatGoesHere[playerRow, playerCol] == KEY)
            {
                hasKey = true;
                whatGoesHere[playerRow, playerCol] = NOTHING;
            }

            // Chequear si pisó fuego
            if (whatGoesHere[playerRow, playerCol] == FIRE)
            {
                levelLost = true;
                Console.WriteLine("\n🔥 You touched fire and died!");
            }

            // Chequear si llegó a la salida
            if (whatGoesHere[playerRow, playerCol] == EXIT)
            {
                if (hasKey)
                {
                    levelWon = true;
                    Console.WriteLine("\n🚪 You found the exit! You win 🎉");
                }
                else
                {
                    Console.WriteLine("You need a key to open this door");
                }
            }

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
                        Console.Write(WALL);
                    else
                        Console.Write(whatGoesHere[r, c]);
                }
                Console.WriteLine();
            }
        }

        // Task 11
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
                        MapLooks[r, c] = WALL;
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
            // El borde no es accesible
            if (row <= 0 || row >= numRows - 1 || col <= 0 || col >= numCols - 1)
                return false;

            return StillOnTheMap(row, col) && canWalkHere[row, col];
        }
    }
}