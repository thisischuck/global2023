using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Wave> _waves;
    private IEnumerator<Wave> _waveIterator;
    private IEnumerator<Breach> _breachIterator;
    private Wave _currentWave;
    public RootSystem _rootSystem;

    public Wave CurrentWave { get => _currentWave; }

    public void StartFirstWave()
    {
        _waveIterator = _waves.GetEnumerator();
        StartNextWave();
    }

    [ContextMenu("Start Next Wave")]
    private void StartNextWave()
    {
        StopAllCoroutines();

        if (_waveIterator.MoveNext())
        {
            _currentWave = _waveIterator.Current;
        }
        else
        {
            Debug.Log("No more waves Available.");
            GameManager.Instance.Win();
            return;
        }

        if(_currentWave)
            StartCoroutine(COR_Wave(_currentWave));
    }


    private IEnumerator COR_Wave(Wave waveData)
    {
        yield return StartCoroutine(COR_Rest(waveData));

        GameManager.Instance.ChangeGamePhase(GameManager.GamePhase.Wave);
        Debug.Log("Wave Started!" );
        _breachIterator = waveData.Breaches.GetEnumerator();

        UIManager.Instance.EnableBreachWarning(waveData.Breaches[0].BreachData.AngleOfAttack);
        yield return Yielders.Get(waveData.Breaches[0].BreachData.FirstAttackWarningDuration);
        UIManager.Instance.DisableBreachWarning();

        while (_breachIterator.MoveNext())
        {
            var breach = _breachIterator.Current;
            yield return Yielders.Get(breach.attackDelay);
            Debug.Log("Started Breach: " + breach.BreachData.name);

            foreach (var item in breach.BreachData.Roots)
            {
                _rootSystem.CreateRoot(item, breach.BreachData.AngleOfAttack);
            }
        }
        
        // Wait until there are no roots left to move to next Wave;
        yield return new WaitUntil(()=> _rootSystem.RootCount() <= 0);
        Debug.Log("Wave Completed!");

        GameManager.Instance.TimeFighting += Mathf.RoundToInt(waveData.Duration);
        GameManager.Instance.WavesConquered++;
        StartNextWave();
    }

    private IEnumerator COR_Rest(Wave waveData)
    {
        GameManager.Instance.ChangeGamePhase(GameManager.GamePhase.Shop);

        if (waveData.IsThereItemsOnWave())
            UIManager.Instance.OpenStore(waveData.PreWaveItems);

        UIManager.Instance.StartTimer(waveData.RestDuration);
        yield return Yielders.Get(waveData.RestDuration);
        GameManager.Instance.TimeSleeping += Mathf.RoundToInt(waveData.RestDuration);

        if (waveData.IsThereItemsOnWave())
            UIManager.Instance.CloseStore();

        yield return Yielders.Get(.5f);
    }

}
