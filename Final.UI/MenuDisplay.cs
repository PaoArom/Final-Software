namespace Final.UI;

using Final.Data;

public class MenuDisplay
{
    private readonly InputHandler _inputHandler;
    private string _selectedLanguage = "English";

    public MenuDisplay()
    {
        _inputHandler = new InputHandler();
    }

    public string SelectedLanguage => _selectedLanguage;

    public string LoginScreen()
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

        return username.Trim();
    }

    public string ShowMainMenu(Player currentPlayer)
    {
        Console.Clear();
        System.Console.WriteLine("==========================");
        System.Console.WriteLine("Escape Room Game");
        System.Console.WriteLine("==========================");
        System.Console.WriteLine("Welcome, " + currentPlayer.Username + "!");
        System.Console.WriteLine("Level: " + currentPlayer.CurrentLevel);
        System.Console.WriteLine("Language: " + _selectedLanguage);
        System.Console.WriteLine("==========================");
        System.Console.WriteLine("1. Play");
        System.Console.WriteLine("2. Language");
        System.Console.WriteLine("3. Exit");
        System.Console.WriteLine("==========================");
        System.Console.WriteLine("Please select an option:");

        return _inputHandler.ReadInput();
    }

    public string ShowLanguageMenu()
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
                return _selectedLanguage;
        }

        System.Console.WriteLine("Language set to: " + _selectedLanguage);
        Pause();
        return _selectedLanguage;
    }

    public void DisplayError()
    {
        _inputHandler.DisplayError();
    }

    public void Pause()
    {
        System.Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}