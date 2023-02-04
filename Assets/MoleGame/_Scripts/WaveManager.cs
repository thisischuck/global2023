using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Wave> _waves;
    private IEnumerator<Wave> _iterator;
    private Wave _currentWave;

    public Wave CurrentWave { get => _currentWave;  }

    private void Start()
    {
        _iterator = _waves.GetEnumerator();
        //StartNextWave();
    }
    
    [ContextMenu("Start Next Wave")]
    private void StartNextWave()
    {
        StopAllCoroutines();
        
        if (_iterator.MoveNext())
        {
            _currentWave= _iterator.Current;
            Debug.Log(_currentWave.name + " is current wave.");
        }
        else
        {
            Debug.Log("No more waves Available.");
            GameManager.Instance.Win();
            return;
        }
        
        StartCoroutine(COR_Wave(_currentWave));    
    }


    private IEnumerator COR_Wave(Wave waveData)
    {   
        yield return StartCoroutine(COR_Rest(waveData));
        
        yield return Yielders.Get(waveData.Duration);
        StartNextWave();
    }

    private IEnumerator COR_Rest(Wave waveData)
    {
        GameManager.Instance.ChangeGameMode(GameManager.GameMode.Shop);
        
        UIManager.Instance.StartTimer(waveData.RestDuration);
        yield return Yielders.Get(waveData.RestDuration);
    }


}
