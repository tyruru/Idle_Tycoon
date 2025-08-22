using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayComponent : MonoBehaviour
{
    [SerializeField] private SoundType _type;
    [SerializeField] private float _minPitch = 0.8f;
    [SerializeField] private float _maxPitch = 1f;
    
    private AudioSource Source;
    private void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Source.volume = _type == SoundType.Sfx ? AudioSettings.I.SfxVolume : AudioSettings.I.MusicVolume;
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Audio clip is null, cannot play sound.");
            return;
        }

        GameObject tempGO = new GameObject("TempAudio");
        AudioSource tempSource = tempGO.AddComponent<AudioSource>();

        tempSource.clip = clip;
        tempSource.pitch = Random.Range(_minPitch, _maxPitch);

        tempSource.Play();

        Destroy(tempGO, clip.length / tempSource.pitch);

    }
}
