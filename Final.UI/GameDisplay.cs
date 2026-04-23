
namespace Final.Logic;

public class GameManager
{
    public void StartGame()
    {
        var map = new MapManager(20, 20, 1, 1);

        bool playing = true;

        while (playing)
        {
            Console.Clear();
            map.DisplayMap();
            System.Console.WriteLine();
            System.Console.WriteLine("Use W/A/S/D to move Press Q to return to the menu");

            var keyInfo = Console.ReadKey(true);
            string key = keyInfo.Key.ToString().ToUpper();

            if (key == "Q")
            {
                playing = false;
                continue;
            }

            if (key is "W" or "A" or "S" or "D")
            {
                map.MovePlayer(key);

                if (map.DidLose())
                {
                    ShowGameOver();
                    playing = false;
                }
                else if (map.DidWin())
                {
                    ShowLevelCleared();
                    playing = false;
                }
            }
            else
            {
                System.Console.WriteLine("Invalid key Use W/A/S/D or Q");
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }
    }

    private void ShowGameOver()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine("  ╔══════════════════════════════════════╗");
        Console.WriteLine("  ║                                      ║");
        Console.WriteLine("  ║   ██████╗  █████╗ ███╗   ███╗███████╗║");
        Console.WriteLine("  ║  ██╔════╝ ██╔══██╗████╗ ████║██╔════╝║");
        Console.WriteLine("  ║  ██║  ███╗███████║██╔████╔██║█████╗  ║");
        Console.WriteLine("  ║  ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ║");
        Console.WriteLine("  ║  ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗║");
        Console.WriteLine("  ║   ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝║");
        Console.WriteLine("  ║                                      ║");
        Console.WriteLine("  ║   ██████╗ ██╗   ██╗███████╗██████╗   ║");
        Console.WriteLine("  ║  ██╔═══██╗██║   ██║██╔════╝██╔══██╗  ║");
        Console.WriteLine("  ║  ██║   ██║██║   ██║█████╗  ██████╔╝  ║");
        Console.WriteLine("  ║  ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗  ║");
        Console.WriteLine("  ║  ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║  ║");
        Console.WriteLine("  ║   ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝  ║");
        Console.WriteLine("  ║                                      ║");
        Console.WriteLine("  ║         🔥 You touched fire 🔥       ║");
        Console.WriteLine("  ║                                      ║");
        Console.WriteLine("  ╚══════════════════════════════════════╝");
        Console.WriteLine();
        Console.ResetColor();
        Console.WriteLine("          Press any key to return to the menu");
        Console.ReadKey(true);
    }

    private void ShowLevelCleared()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine("  ╔═══════════════════════════════════════════╗");
        Console.WriteLine("  ║                                           ║");
        Console.WriteLine("  ║  ██╗     ███████╗██╗   ██╗███████╗██╗     ║");
        Console.WriteLine("  ║  ██║     ██╔════╝██║   ██║██╔════╝██║     ║");
        Console.WriteLine("  ║  ██║     █████╗  ██║   ██║█████╗  ██║     ║");
        Console.WriteLine("  ║  ██║     ██╔══╝  ╚██╗ ██╔╝██╔══╝  ██║     ║");
        Console.WriteLine("  ║  ███████╗███████╗ ╚████╔╝ ███████╗███████╗║");
        Console.WriteLine("  ║  ╚══════╝╚══════╝  ╚═══╝  ╚══════╝╚══════╝║");
        Console.WriteLine("  ║                                           ║");
        Console.WriteLine("  ║   ██████╗██╗     ███████╗ █████╗ ██████╗  ║");
        Console.WriteLine("  ║  ██╔════╝██║     ██╔════╝██╔══██╗██╔══██╗ ║");
        Console.WriteLine("  ║  ██║     ██║     █████╗  ███████║██████╔╝ ║");
        Console.WriteLine("  ║  ██║     ██║     ██╔══╝  ██╔══██║██╔══██╗ ║");
        Console.WriteLine("  ║  ╚██████╗███████╗███████╗██║  ██║██║  ██║ ║");
        Console.WriteLine("  ║   ╚═════╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝ ║");
        Console.WriteLine("  ║                                           ║");
        Console.WriteLine("  ║       🚪 You found the exit 🎉            ║");
        Console.WriteLine("  ║                                           ║");
        Console.WriteLine("  ╚═══════════════════════════════════════════╝");
        Console.WriteLine();
        Console.ResetColor();
        Console.WriteLine("          Press any key to return to the menu");
        Console.ReadKey(true);
    }
}