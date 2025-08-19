using System.IO;
using UnityEngine;

public static class JsonPlayerInventorySaver
{
    private static string FilePath => Path.Combine(Application.persistentDataPath, "playerData.json");

    public static void Save(PlayerInventory inventory)
    {
        var json = JsonUtility.ToJson(inventory, true);
        File.WriteAllText(FilePath, json);
        Debug.Log("Player data saved: " + FilePath);
    }

    public static PlayerInventory Load()
    {
        if (!File.Exists(FilePath))
        {
            Debug.Log("No save file found, creating new inventory.");
            return new PlayerInventory();
        }

        var json = File.ReadAllText(FilePath);
        var inventory = JsonUtility.FromJson<PlayerInventory>(json);
        return inventory ?? new PlayerInventory();
    }
}
