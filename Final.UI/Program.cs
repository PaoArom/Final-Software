using Final.Data;
using Final.Logic;
using Final.UI;

var menu = new MenuDisplay();
var saveSystem = new SaveSystem();
var gameManager = new GameManager();

string username = menu.LoginScreen();
Player currentPlayer = saveSystem.LoadGame(username) ?? new Player(username);

if (!saveSystem.SaveExists(username))
{
    saveSystem.SaveGame(currentPlayer);
}

bool inMenu = true;

while (inMenu)
{
    string option = menu.ShowMainMenu(currentPlayer);

    switch (option)
    {
        case "1":
            if (!currentPlayer.TutorialCompleted)
            {
                gameManager.StartTutorial();
                currentPlayer.TutorialCompleted = true;
                saveSystem.SaveGame(currentPlayer);
            }

            int levelsCompleted = gameManager.StartGame();

            if(levelsCompleted > currentPlayer.CurrentLevel)
            {
                currentPlayer.CurrentLevel = levelsCompleted;
            }

            saveSystem.SaveGame(currentPlayer);
            break;
        case "2":
            menu.ShowLanguageMenu();
            break;
        case "3":
            Console.Clear();
            System.Console.WriteLine("Exiting the game...");
            inMenu = false;
            break;
        default:
            menu.DisplayError();
            menu.Pause();
            break;
    }
}
