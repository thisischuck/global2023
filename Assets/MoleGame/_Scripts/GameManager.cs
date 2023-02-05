using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] MoleController _player;

    public delegate void OnLoseDelegate();
    public delegate void OnWinDelegate();
    public delegate void OnGamePhaseDelegate(GamePhase mode);


    public OnLoseDelegate OnLose;
    public OnWinDelegate OnWin;
    public OnGamePhaseDelegate OnGamePhase;


    private GamePhase _currentGamePhase;
    private WaveManager _waveManager;
    private BaseManager _baseManager;
    private RootSystem _rootSystem;

    public WaveManager WaveManager { get => _waveManager;  }
    public RootSystem RootSystem { get => _rootSystem;  }
    public BaseManager BaseManager { get => _baseManager;  }
    public MoleController Player { get => _player; }

    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance 
    {
        get 
        { 
            if(_instance == null)
            Debug.LogError("No Game Manager Available.");
            return _instance;
        } 
    }


    private void Awake() => _instance = this;
    #endregion

    private void Start()
    {
        _waveManager = GetComponentInChildren<WaveManager>();
        _baseManager = GetComponentInChildren<BaseManager>();
        _rootSystem = GetComponentInChildren<RootSystem>();
       
        _waveManager.StartNextWave();
    }

    public void ChangeGamePhase(GamePhase newGameMode) 
    {
        _currentGamePhase = newGameMode;
        OnGamePhase(_currentGamePhase);
    }

    public void Lose() 
    {
        if(_currentGamePhase == GamePhase.Defeat) return;

        ChangeGamePhase(GamePhase.Defeat);
        Debug.Log("Loss requested!!!!!!!!!!");
        OnLose();
    }

    public void Win() 
    {
        if(_currentGamePhase == GamePhase.Victory) return;

        ChangeGamePhase(GamePhase.Victory);
        Debug.Log("Win requested !!!!!!!!!!!!");
        OnWin();
    }

    public enum GamePhase
    {
        Wave,
        Shop,
        Defeat,
        Victory
    }
}
