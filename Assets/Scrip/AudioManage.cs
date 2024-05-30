using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioSource MusicSource,SfxSound,SfxSound2;
    private void Start()
    {
        PlayMusic();
    }
    public void PlayMusic()
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }
    public void PlaySFX()
    {
        SfxSound.PlayOneShot(clip1);
    }
    public void PlaySFX2()
    {
        SfxSound2.PlayOneShot(clip2);
        MusicSource.Stop();
    }

}
