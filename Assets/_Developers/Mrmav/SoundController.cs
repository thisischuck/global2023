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
        GameManager.Instance.OnGamePhase += PlayGameModeSound;

        AmbientAudioEvent.Play(null, true);
        SoundtrackAudioEvent.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayLoseSound()
    {
        LoseSound.Play();
        SoundtrackAudioEvent.Stop();

    }

    void PlayWinSound()
    {
        WinSound.Play();
    }

    void PlayGameModeSound(GameManager.GamePhase mode)
    {

        if(mode == GameManager.GamePhase.Wave)
        {
            GameModeWave.Play();

        } else if(mode == GameManager.GamePhase.Shop)
        {
            GameModeStore.Play();
        }

        if(!SoundtrackAudioEvent.IsPlaying())
        {
            if(mode == GameManager.GamePhase.Wave)
                SoundtrackAudioEvent.Play();
        }

    }

}
