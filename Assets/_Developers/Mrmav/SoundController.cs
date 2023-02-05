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

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        AmbientAudioEvent.Play();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        GameManager.Instance.OnLose += PlayLoseSound;
        GameManager.Instance.OnWin  += PlayWinSound;
        GameManager.Instance.OnGamePhase += PlayGameModeSound; 
    }

    void OnDisable()
    {
        GameManager.Instance.OnLose -= PlayLoseSound;
        GameManager.Instance.OnWin  -= PlayWinSound;
        GameManager.Instance.OnGamePhase -= PlayGameModeSound;
    }

    void PlayLoseSound()
    {
        LoseSound.Play();
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

    }

}
