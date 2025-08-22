using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioSettingsWidget  _musicWidget;  
    [SerializeField] private AudioSettingsWidget  _sfxWidget; 
    
    private void Start()
    {
        _musicWidget.Initialize(AudioSettings.I.MusicVolume);
        _sfxWidget.Initialize(AudioSettings.I.SfxVolume);
        
        _musicWidget.OnValueChanged += OnMusicVolumeChanged;
        _sfxWidget.OnValueChanged += OnSfxVolumeChanged;
    }

    private void OnSfxVolumeChanged(float value)
    {
        AudioSettings.I.SfxVolume = value;
    }

    private void OnMusicVolumeChanged(float value)
    {
        AudioSettings.I.MusicVolume = value;
    }
}
