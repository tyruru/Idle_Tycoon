using System.IO;
using UnityEngine;

public class BaseJsonSaver<T> where T : new()
{
    protected string FilePath => Path.Combine(Application.persistentDataPath, typeof(T).Name);

    public void Save(T data)
    {
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(FilePath, json);
        Debug.Log($"{typeof(T).Name} saved: " + FilePath);
    }

    public T Load()
    {
        if (!File.Exists(FilePath))
        {
            Debug.Log($"No {typeof(T).Name} save file found.");
            return new T();
        }

        var json = File.ReadAllText(FilePath);
        return JsonUtility.FromJson<T>(json) ?? new T();
    }
}
