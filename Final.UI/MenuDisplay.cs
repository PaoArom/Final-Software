namespace Final.UI;

using Final.Data;

public class MenuDisplay
{
    private readonly InputHandler _inputHandler;
    private readonly SaveSystem _saveSystem;
    private Player? _currentPlayer;
    private string _selectedLanguage = "English";

    public MenuDisplay()
    {
        _inputHandler = new InputHandler();
        _saveSystem = new SaveSystem();
    }

    public Player? GetCurrentPlayer()
    {
        return _currentPlayer;
    }
    public void MainMenu()
    {
        LoginScreen();

        bool inMenu = true;

        while (inMenu)
        {
            Console.Clear();
            System.Console.WriteLine("==========================");
            System.Console.WriteLine("Escape Room Game");
            System.Console.WriteLine("==========================");
            System.Console.WriteLine("Welcome, " + _currentPlayer?.Username + "!");
            System.Console.WriteLine("Level: " + _currentPlayer?.CurrentLevel);
            System.Console.WriteLine("==========================");
            System.Console.WriteLine("1. Play");
            System.Console.WriteLine("2. Language");
            System.Console.WriteLine("3. Exit");
            System.Console.WriteLine("==========================");
            System.Console.WriteLine("Please select an option:");

            string input = _inputHandler.ReadInput();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    System.Console.WriteLine("Starting the game...");
                    Pause();
                    break;
                case "2":
                    break;
                case "3":
                    Console.Clear();
                    System.Console.WriteLine("Exiting the game...");
                    inMenu = false;
                    break;
                default:
                    _inputHandler.DisplayError();
                    Pause();
                    break;
            }
        }
    }

    public void LoginScreen()
    {
        Console.Clear();
        System.Console.WriteLine("==========================");
        System.Console.WriteLine("Login");
        System.Console.WriteLine("Enter your username:");
        System.Console.WriteLine("==========================");

        string username = _inputHandler.ReadInput();

        while (!_inputHandler.ValidateInput(username))
        {
            _inputHandler.DisplayError();
            username = _inputHandler.ReadInput();
        }

        Player? existingPlayer = _saveSystem.LoadGame(username);

        if(existingPlayer != null)
        {
            _currentPlayer = existingPlayer;
            System.Console.WriteLine("Welcome back, " + _currentPlayer.Username + "!");
            System.Console.WriteLine("Current level: " + _currentPlayer.CurrentLevel);
        }
        else
        {
            _currentPlayer = new Player(username);
            _saveSystem.SaveGame(_currentPlayer);
            System.Console.WriteLine("New player created: " + _currentPlayer.Username);
        }

        Pause();
    }

    public void LanguageMenu()
    {
        Console.Clear();
        System.Console.WriteLine("==========================");
        System.Console.WriteLine("Language Selection");
        System.Console.WriteLine("==========================");
        System.Console.WriteLine("1. English");
        System.Console.WriteLine("2. Spanish");
        System.Console.WriteLine("==========================");
        System.Console.WriteLine("Please select a language:");

        string input = _inputHandler.ReadInput();

        switch (input)
        {
            case "1":
                _selectedLanguage = "English";
                break;
            case "2":
                _selectedLanguage = "Spanish";
                break;
            default:
                _inputHandler.DisplayError();
                Pause();
                return;
        }

        System.Console.WriteLine("Language set to: " + _selectedLanguage);
        Pause();
    }

    private void Pause()
    {
        System.Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}