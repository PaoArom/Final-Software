namespace Final.Logic;

public class TutorialManager
{
    private MapManager map;
    private InventoryManager inventory;

    private const string TREE    = "⬛";
    private const string NOTHING = "  ";
    private const string BALLIE  = "🟢";
    private const string FIRE    = "🔥";
    private const string EXIT    = "🚪";
    private const string KEY     = "🗝️";

    private bool levelWon  = false;
    private bool levelLost = false;

    public bool DidWin()  => levelWon;
    public bool DidLose() => levelLost;

    public TutorialManager()
    {
        map       = new MapManager(10, 10, 1, 1, isTutorial: true);
        inventory = new InventoryManager();
        BuildTutorialMap();
    }

    private void BuildTutorialMap()
    {
        map.TutorialObject(4, 5, FIRE, true);
        map.TutorialObject(2, 7, KEY,  false);
        map.TutorialObject(7, 7, EXIT, false);
    }

    public void Run()
    {
        bool playing = true;

        while (playing)
        {
            Console.Clear();
            DisplayTutorialWithHints();
            Console.WriteLine();

            var keyInfo = Console.ReadKey(true);
            string key  = keyInfo.Key.ToString().ToUpper();

            if (key == "Q")
            {
                playing = false;
                continue;
            }

            if (key == "E")
            {
                if (map.HasKey() && !inventory.HasItem("🗝️  Old Key"))
                    inventory.PickUpItem("🗝️  Old Key");

                inventory.ShowInventory();
                continue;
            }

            if (key is "W" or "A" or "S" or "D")
            {
                map.MovePlayer(key);

                if (map.DidLose())
                {
                    levelLost = true;
                    playing   = false;
                }
                else if (map.DidWin())
                {
                    levelWon = true;
                    playing  = false;
                }
            }
            else if (key != "E" && key != "Q")
            {
                Console.WriteLine("Invalid key.");
                Console.ReadKey(true);
            }
        }
    }

    private void DisplayTutorialWithHints()
    {
        string[] mapRows   = GetMapRows();
        string[] hints     = GetHints();
        int      totalRows = Math.Max(mapRows.Length, hints.Length);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("  ╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("  ║                         📖 TUTORIAL                          ║");
        Console.WriteLine("  ╚══════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        for (int i = 0; i < totalRows; i++)
        {
            // mapa a la izquierda con padding fijo de 25 caracteres
            string mapPart = i < mapRows.Length ? mapRows[i] : "";
            mapPart = mapPart.PadRight(25);

            Console.Write(mapPart);
            Console.Write("        "); // espacio entre mapa y texto

            Console.ForegroundColor = ConsoleColor.Yellow;
            string hintPart = i < hints.Length ? hints[i] : "";
            Console.WriteLine(hintPart);
            Console.ResetColor();
        }
    }

    private string[] GetMapRows()
    {
        string[,] grid = map.GetGrid();
        int rows       = grid.GetLength(0);
        int cols       = grid.GetLength(1);
        string[] lines = new string[rows];

        for (int r = 0; r < rows; r++)
        {
            string line = "  ";
            for (int c = 0; c < cols; c++)
                line += grid[r, c];
            lines[r] = line;
        }

        return lines;
    }

    private string[] GetHints()
    {
        bool playerHasKey = map.HasKey();

        return new string[]
        {
            "                                          ",
            " ╔══════════════════════════════════════╗",
            " ║           HOW TO PLAY                ║",
            " ╠══════════════════════════════════════╣",
            " ║                                      ║",
            " ║  🕹️  Move around with W/A/S/D        ║",
            " ║                                      ║",
            " ║  🗝️  Grab the key to unlock the exit ║",
            " ║                                      ║",
            " ║  🔥 Don't touch fire — instant death!║",
            " ║                                      ║",
            " ║  🚪 Reach the door to finish!        ║",
            " ║                                      ║",
            " ║  🎒 Press E to open your inventory   ║",
            " ║                                      ║",
            playerHasKey
                ? " ║  ✅ You have the key! Find the exit! ║"
                : " ║  🔍 Find the key first!              ║",
            " ║                                      ║",
            " ║  Press Q to return to the menu       ║",
            " ╚══════════════════════════════════════╝",
        };
    }
}