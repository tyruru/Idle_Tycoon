using UnityEngine;

[CreateAssetMenu(menuName = "Data/AudioSettings", fileName = "AudioSettings")]
public class AudioSettings : ScriptableObject
{
    [Range(0f, 1f)]public float MusicVolume = 0.5f;
    [Range(0f, 1f)]public float SfxVolume = 0.5f;
    
    private static AudioSettings _instance;
    public static AudioSettings I => _instance == null ? LoadInstance() : _instance;

    private static AudioSettings LoadInstance()
    {
        return _instance = Resources.Load<AudioSettings>("AudioSettings");
    }
}


