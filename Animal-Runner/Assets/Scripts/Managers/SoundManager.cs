using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> sounds;

    public static SoundManager current;
    private void Start()
    {
        if(current == null)
        {
            current = this;
        }

        int musicControl = PlayerPrefs.GetInt("Music");
        if(musicControl == 1)
        {
            PlayGameMusic();
        }
        else
        {
            CloseGameMusic();
        }

        int soundControl = PlayerPrefs.GetInt("Sounds");
        if (soundControl == 1)
        {
            ActivateSounds();
        }
        else
        {
            MuteSounds();
        }
    }

    public void PlayCoinSound()
    {
        sounds[0].Play();
    }

    public void PlayLoseGameSound()
    {
        sounds[1].Play();
        CloseGameMusic();
    }

    public void PlayWinGameSound()
    {
        sounds[2].Play();
        CloseGameMusic();
    }

    public void CloseGameMusic()
    {
        sounds[4].enabled = false;
        sounds[4].Stop();
    }

    public void PlayGameMusic()
    {
        sounds[4].enabled = true;
        sounds[4].Play();
    }

    public void MuteSounds()
    {
        for (int i = 0; i < sounds.Count-1; i++)
        {
            sounds[i].enabled = false;
        }
    }

    public void ActivateSounds()
    {
        for (int i = 0; i < sounds.Count - 1; i++)
        {
            sounds[i].enabled = true;
        }
    }

    public void PlayBuySound()
    {
        sounds[3].Play();
    }
}
