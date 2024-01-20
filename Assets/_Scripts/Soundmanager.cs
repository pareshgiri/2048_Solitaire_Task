using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour
{
    // SoundManager

    public SoundAudioClip[] allAudioClip;

    [System.Serializable]
    public class SoundAudioClip
    {
        public Sound sound;
        public AudioClip audioClip;
    }

    public enum Sound
    {
        correct,
        wrong,
        discard,
    }

    public AudioSource OneShotAudioSource;

    public static Soundmanager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(Sound sound)
    {
        OneShotAudioSource.PlayOneShot(GetAudioClip(sound));
    }

    AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip SoundClip in allAudioClip)
        {
            if (SoundClip.sound == sound)
            {
                return SoundClip.audioClip;
            }
        }
        return null;
    }
}