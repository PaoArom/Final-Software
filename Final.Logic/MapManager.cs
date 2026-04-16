namespace Final.Logic;

public class MapManager
{

    //la task 4 xd

    private int rows;
        private int columns;
        private bool[,] walkableCells;   
        private int playerRow;
        private int playerCol;

        public MapManager(int rows, int columns, int startRow, int startCol)
        {
            this.rows    = rows;
            this.columns = columns;

            walkableCells = new bool[rows, columns];

            
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                    walkableCells[r, c] = true;

            playerRow = startRow;
            playerCol = startCol;
        }

        // para que si queremos poner un obtsaculo o algo se pueda luego uwu
        public void SetWalkable(int row, int col, bool walkable)
        {
            if (IsInBounds(row, col))
                walkableCells[row, col] = walkable;
        }

        // Task 7 
        public (int row, int col) GetCurrentPosition()
        {
            return (playerRow, playerCol);
        }

        //  Task 6
        public List<string> GetAvailableMoves()
        {
            var moves = new List<string>();

            if (CanMoveTo(playerRow - 1, playerCol)) moves.Add("Up");
            if (CanMoveTo(playerRow + 1, playerCol)) moves.Add("Down");
            if (CanMoveTo(playerRow, playerCol - 1)) moves.Add("Left");
            if (CanMoveTo(playerRow, playerCol + 1)) moves.Add("Right");

            return moves;
        }

        //  Task 5
        public bool MovePlayer(string direction)
        {
            int newRow = playerRow;
            int newCol = playerCol;

            switch (direction.ToLower())
            {
                case "up":    newRow--; break;
                case "down":  newRow++; break;
                case "left":  newCol--; break;
                case "right": newCol++; break;
                default:
                    Console.WriteLine($"Dirección inválida: {direction}");
                    return false;
            }

            if (!CanMoveTo(newRow, newCol))
            {
                Console.WriteLine("No puedes moverte en esa dirección.");
                return false;
            }

            playerRow = newRow;
            playerCol = newCol;
            return true;
        }

        
        private bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < rows && col >= 0 && col < columns;
        }

        private bool CanMoveTo(int row, int col)
        {
            return IsInBounds(row, col) && walkableCells[row, col];
        }

        public void DisplayMap()
{
    string wall   = "🟫";
    string floor  = "  "; 
    string player = "🟢";

    for (int r = 0; r < rows; r++)
    {
        for (int c = 0; c < columns; c++)
        {
            bool isBorder = (r == 0 || r == rows - 1 || c == 0 || c == columns - 1);
            bool isPlayer = (r == playerRow && c == playerCol);

            if (isBorder)
                Console.Write(wall);
            else if (isPlayer)
                Console.Write(player);
            else
                Console.Write(floor);
        }
        Console.WriteLine();
    }
}


    }

