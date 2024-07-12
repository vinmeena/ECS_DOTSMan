using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public string defaultMusic;
    public static AudioManager instance;
    public AudioSource audioSource;
    private void Awake()
    {
        instance = this;
    }


    public void PlaySFX(string sfxClipName)
    {
        var sfxAudioClip = Resources.Load<AudioClip>("SFX/" + sfxClipName);

        if (sfxAudioClip == null) return;

        AudioSource.PlayClipAtPoint(sfxAudioClip, Camera.main.transform.position);
    }


    public void PlayMusic(string musicClipName)
    {
        var musicAudioClip = Resources.Load<AudioClip>("Music/" + musicClipName);

        if (musicAudioClip == null) return;

        if (musicAudioClip == audioSource.clip) return;

        audioSource.clip = musicAudioClip;
        audioSource.Play();
    }

}
