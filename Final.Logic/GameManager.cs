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
            System.Console.WriteLine("Use W/A/S/D to move. Press Q to return to the menu.");

            var keyInfo = Console.ReadKey(true);
            string key = keyInfo.Key.ToString().ToUpper();

            if (key == "Q")
            {
                playing = false;
                continue;
            }

            if (key is "W" or "A" or "S" or "D")
            {
                bool moved = map.MovePlayer(key);
                if (!moved)
                {
                    System.Console.WriteLine("You can't move there!");
                    System.Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
            }
            else
            {
                System.Console.WriteLine("Invalid key. Use W/A/S/D or Q.");
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }
    }
}
