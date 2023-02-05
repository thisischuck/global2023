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
    public SimpleAudioEvent DirtSound = null;
    public SimpleAudioEvent EatSound = null;
    public SimpleAudioEvent UnlockItemSound = null;

    // Start is called before the first frame update
    void Start()
    {
        AmbientAudioEvent.Play(null, true);
        SoundtrackAudioEvent.Play();        
    }

    private void OnEnable()
    {
        GameManager.Instance.OnLose += PlayLoseSound;
        GameManager.Instance.OnWin  += PlayWinSound;
        GameManager.Instance.OnGamePhase += PlayGameModeSound;

        // GameManager.Instance.Player.OnEat   += PlayEatSound;
        // GameManager.Instance.Player.OnSlide += PlaySlideSound;
        // GameManager.Instance.Player.OnUnlockItem += PlayUnlockItemSound;

    }

    private void OnDisable()
    {
        GameManager.Instance.OnLose -= PlayLoseSound;
        GameManager.Instance.OnWin -= PlayWinSound;
        GameManager.Instance.OnGamePhase -= PlayGameModeSound;

        // GameManager.Instance.Player.OnEat   -= PlayEatSound;
        // GameManager.Instance.Player.OnSlide -= PlaySlideSound;
        // GameManager.Instance.Player.OnUnlockItem -= PlayUnlockItemSound;

    }

    void PlayLoseSound()
    {
        LoseSound.Play();
        SoundtrackAudioEvent.Stop();
    }

    void PlayWinSound()
    {
        if(!SoundtrackAudioEvent.IsPlaying())
        {
            SoundtrackAudioEvent.Play();
        }
    }

    void PlayEatSound()
    {
        EatSound.Play();
    }

    void PlaySlideSound()
    {
        DirtSound.Play();
    }

    void PlayUnlockItemSound()
    {
        //UnlockItem.Play();
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
