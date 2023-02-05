using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] MoleController _player;

    public delegate void OnLoseDelegate();
    public delegate void OnWinDelegate();
    public delegate void OnGameModeDelegate(GameMode mode);


    public OnLoseDelegate OnLose;
    public OnWinDelegate OnWin;
    public OnGameModeDelegate OnGameMode;


    private GameMode _currentGameMode;
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
    public void ChangeGameMode(GameMode newGameMode) 
    {
        _currentGameMode = newGameMode;
        OnGameMode(_currentGameMode);
    }
    public void Lose() 
    {
        Debug.Log("Loss requested.");
        OnLose();
    }

    public void Win() 
    {
        Debug.Log("Win requested.");
        OnWin();
    }

    public enum GameMode
    {
        Wave,
        Shop
    }
}
