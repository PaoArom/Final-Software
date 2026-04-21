using Final.Logic;

namespace Final.Data;

public class Player
{
    public string Username { get; private set;}
    public int CurrentLevel {get; set;}
    public InventoryManager Inventory {get; private set;}
    public Player(string username)
    {
        Username = username;
        CurrentLevel = 0;
        Inventory = new InventoryManager();
    }
}
