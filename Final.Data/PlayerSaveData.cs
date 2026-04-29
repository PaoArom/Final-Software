namespace Final.Data;
using Final.Logic;

public class PlayerSaveData
{
    public string Username { get; set; } = "";
    public int CurrentLevel { get; set; }
    public List<string> InventoryItems { get; set; } = new();
    public List<string> Medals { get; set; } = new();
    public bool TutorialCompleted { get; set; } = false;
}