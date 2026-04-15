namespace Final.UI;

public class InputHandler
{
    private string _input = string.Empty;
    private bool _isValid;

    public bool ValidateInput(string input)
    {
        _input = input;
        _isValid = !string.IsNullOrWhiteSpace(_input);
        return _isValid;
    }

    public string ReadInput()
    {
        while (true)
        {
            _input = Console.ReadLine() ?? string.Empty;
            _isValid = ValidateInput(_input);

            if (!_isValid)
            {
                break;
            }
            
            DisplayError();
        }
        return _input;
    }

    public void DisplayError()
    {
        System.Console.WriteLine("Invalid input. Try again.");
    }
}