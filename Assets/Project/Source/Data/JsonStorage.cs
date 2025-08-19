using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonStorage
{
    private readonly string _baseDirectory;

    public JsonStorage(string baseDirectory)
    {
        _baseDirectory = baseDirectory;
        Directory.CreateDirectory(_baseDirectory);
    }

    private string GetFilePath<T>() => Path.Combine(_baseDirectory, $"{typeof(T).Name}.json");

    public List<T> Load<T>()
    {
        var path = GetFilePath<T>();

        if (!File.Exists(path))
            return new List<T>();

        var json = File.ReadAllText(path);
        return JsonUtility.FromJson<List<T>>(json) ?? new List<T>();
    }

    public void Save<T>(List<T> data)
    {
        var path = GetFilePath<T>();
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }
}
