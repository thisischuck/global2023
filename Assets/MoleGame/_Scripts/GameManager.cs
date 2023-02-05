using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
#region Components
    [SerializeField] MoleController _player;
    
    private WaveManager _waveManager;
    private BaseManager _baseManager;
    private RootSystem _rootSystem;
#endregion

#region Stats
    private int _rootsAnnihilated = 0;
    private int _wavesConquered = 0;
    private int _timeFighting = 0;
    private int _timeSleeping = 0;

    public int RootsAnnihilated { get => _rootsAnnihilated; set => _rootsAnnihilated = value; }
    public int WavesConquered { get => _wavesConquered; set => _wavesConquered = value; }
    public int TimeFighting { get => _timeFighting; set => _timeFighting = value; }
    public int TimeSleeping { get => _timeSleeping; set => _timeSleeping = value; }
#endregion

#region Delegates
    public delegate void OnLoseDelegate();
    public delegate void OnWinDelegate();
    public delegate void OnGamePhaseDelegate(GamePhase mode);
    public OnLoseDelegate OnLose;
    public OnWinDelegate OnWin;
    public OnGamePhaseDelegate OnGamePhase;
#endregion

    private GamePhase _currentGamePhase;
    

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
        Debug.Log("changing game phase to " + _currentGamePhase);
    }

    public void Lose() 
    {
        if(_currentGamePhase == GamePhase.Defeat) return;

        ChangeGamePhase(GamePhase.Defeat);
        Debug.Log("Loss requested!!!!!!!!!!");
        UIManager.Instance.DisplayStats();
        OnLose();
        
        Time.timeScale = 0;
    }

    public void Win() 
    {
        if(_currentGamePhase == GamePhase.Victory) return;

        ChangeGamePhase(GamePhase.Victory);
        Debug.Log("Win requested !!!!!!!!!!!!");
        UIManager.Instance.DisplayStats();
        OnWin();

        Time.timeScale = 0;
    }

    public enum GamePhase
    {
        Wave,
        Shop,
        Defeat,
        Victory
    }
}
