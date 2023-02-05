using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public SimpleAudioEvent SoundtrackAudioEvent = null;
    public SimpleAudioEvent AmbientAudioEvent = null;
    public SimpleAudioEvent GameModeStore = null;
    public SimpleAudioEvent GameModeWave = null;
    public SimpleAudioEvent LoseSound = null;
    public SimpleAudioEvent WinSound = null;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnLose += PlayLoseSound;
        GameManager.Instance.OnWin  += PlayWinSound;
        GameManager.Instance.OnGameMode += PlayGameModeSound;

        AmbientAudioEvent.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayLoseSound()
    {
        LoseSound.Play();

    }

    void PlayWinSound()
    {
        WinSound.Play();
    }

    void PlayGameModeSound(GameManager.GameMode mode)
    {

        if(mode == GameManager.GameMode.Wave)
        {
            GameModeWave.Play();

        } else if(mode == GameManager.GameMode.Shop)
        {
            GameModeStore.Play();
        }

    }

}
