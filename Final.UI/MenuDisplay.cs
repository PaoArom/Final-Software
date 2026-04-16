namespace Final.UI;

public class MenuDisplay
{
    private readonly InputHandler _inputHandler;

    public MenuDisplay()
    {
        _inputHandler = new InputHandler();   
    }
    public void MainMenu()
    {
        LoginScreen();
        
        bool inMenu = true;

        while(inMenu)
        {
            Console.Clear();
            System.Console.WriteLine("==========================");
            System.Console.WriteLine("Escape Room Game");
            System.Console.WriteLine("1. Play");
            System.Console.WriteLine("2. Language");
            System.Console.WriteLine("3. Exit");
            System.Console.WriteLine("==========================");

            string input = _inputHandler.ReadInput();

            switch(input)
            {
                case "1":
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

        while(!_inputHandler.ValidateInput(username))
        {
            _inputHandler.DisplayError();
            username = _inputHandler.ReadInput();
        }
    }

    private void Pause()
    {
        System.Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}