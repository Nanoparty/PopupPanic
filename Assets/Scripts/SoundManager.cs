using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sm;

    public AudioClip SuccessSound;
    public AudioClip FailureSound;
    public AudioClip ErrorSound;
    public AudioClip CloseSound;
    public AudioClip OpenSound;
    public AudioClip ClickSound;
    public AudioClip StartSound;

    public AudioClip BackgroundMusic;

    private AudioSource musicSource;
    private AudioSource soundSource;

    private void Awake()
    {
        if (sm == null)
        {
            sm = this;
        }
        else if (sm != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        musicSource = GetComponent<AudioSource>();
        soundSource = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        musicSource.clip = BackgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySucessSound()
    {
        soundSource.PlayOneShot(SuccessSound);
    }

    public void PlayStartSound()
    {
        soundSource.PlayOneShot(StartSound);
    }

    public void PlayFailureSound()
    {
        soundSource.PlayOneShot(FailureSound);
    }

    public void PlayErrorSound()
    {
        soundSource.PlayOneShot(ErrorSound, 2.0f);
    }

    public void PlayWindowOpenSound()
    {
        soundSource.PlayOneShot(OpenSound);
    }

    public void PlayWindowCloseSound()
    {
        soundSource.PlayOneShot(CloseSound, 0.2f);
    }
}
