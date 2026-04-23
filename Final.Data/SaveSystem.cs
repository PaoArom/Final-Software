namespace Final.Data;
using System.Text.Json;

public class SaveSystem
{
    private readonly string _saveFile = "saves/";

    public void SaveGame(Player player)
    {
        Directory.CreateDirectory(_saveFile);
        string filePath = _saveFile + player.Username + ".json";

        PlayerSaveData saveData = player.ToSaveData();

        string json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions
        { 
            WriteIndented = true 
        });

        File.WriteAllText(filePath, json);
    }

    public Player? LoadGame(string username)
    {
        string filePath = _saveFile + username + ".json";

        if (!File.Exists(filePath))
            return null;

        string json = File.ReadAllText(filePath);
        PlayerSaveData saveData = JsonSerializer.Deserialize<PlayerSaveData>(json);

        if (saveData == null)
            return null;

        Player player = new Player(saveData.Username);
        player.LoadFromSaveData(saveData);
        return player;
    }

    public bool SaveExists(string username)
    {
        string filePath = _saveFile + username + ".json";
        return File.Exists(filePath);
    }
}