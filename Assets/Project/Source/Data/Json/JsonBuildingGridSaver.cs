using System.IO;
using UnityEngine;

public static class JsonBuildingGridSaver
{
    private static string FilePath => Path.Combine(Application.persistentDataPath, "buildings.json");

    public static void Save(BuildingsSaveData data)
    {
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(FilePath, json);
        Debug.Log("Buildings saved: " + FilePath);
    }

    public static BuildingsSaveData Load()
    {
        if (!File.Exists(FilePath))
        {
            Debug.Log("No building save file found.");
            return new BuildingsSaveData();
        }

        var json = File.ReadAllText(FilePath);
        return JsonUtility.FromJson<BuildingsSaveData>(json) ?? new BuildingsSaveData();
    }
}
