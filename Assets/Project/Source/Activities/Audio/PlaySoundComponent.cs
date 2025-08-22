using System;
using System.Linq;
using UnityEngine;

public class PlaySoundsComponent : MonoBehaviour
{
    
    [SerializeField] private AudioData[] _sounds;

    public void PlayOneShot(string id)
    {
        var sound = _sounds.FirstOrDefault(d => d.Id == id);
        
        if (sound != null)
        {
            GameSession.I.SfxSource.PlayOneShot(sound.Clip);
        }
    }
    
    [Serializable]
    private class AudioData
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _clip;

        public string Id => _id;
        public AudioClip Clip => _clip;
    }
}