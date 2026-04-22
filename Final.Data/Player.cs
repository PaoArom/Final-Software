using Final.Logic;

namespace Final.Data;

public class Player
{
    public string Username { get; private set;}
    public int CurrentLevel {get; set;}
    public InventoryManager Inventory {get; private set;}
    public List<Medal> EarnedMedals { get; private set; } 
    public Player(string username)
    {
        Username = username;
        CurrentLevel = 0;
        Inventory = new InventoryManager();
        EarnedMedals = new List<Medal>();
    }

    public void EarnMedal(Medal medal)
    {
        if (!EarnedMedals.Contains(medal))
        {
            EarnedMedals.Add(medal);
        }
    }

    public PlayerSaveData ToSaveData()
    {
        return new PlayerSaveData
        {
            Username = Username,
            CurrentLevel = CurrentLevel,
            //InventoryItems
            //Medals 
        };
    }
    
    public void LoadFromSaveData(PlayerSaveData saveData)
    {
        CurrentLevel = saveData.CurrentLevel;
        // Load inventory items and medals
    }
}
