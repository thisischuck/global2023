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

    private void Awake()
    {
        _waveIterator = _waves.GetEnumerator();
    }

    [ContextMenu("Start Next Wave")]
    public void StartNextWave()
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

        StartCoroutine(COR_Wave(_currentWave));
    }


    private IEnumerator COR_Wave(Wave waveData)
    {
        yield return StartCoroutine(COR_Rest(waveData));

        Debug.Log("Wave Started! Duration: " + waveData.Duration);
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

            //yield return new WaitUntil(()=> _rootSystem.RootCount <= 0);
        }

        Debug.Log("Wave Completed!");
        StartNextWave();
    }

   // private bool IsThereAnyRootsLeft()
    //{
     //   if(_rootSystem.RootCount <= 0) return false;
    //    return true;
    //}

    private IEnumerator COR_Rest(Wave waveData)
    {
        GameManager.Instance.ChangeGameMode(GameManager.GameMode.Shop);

        if (waveData.IsThereItemsOnWave())
            UIManager.Instance.OpenStore(waveData.PreWaveItems);

        UIManager.Instance.StartTimer(waveData.RestDuration);
        yield return Yielders.Get(waveData.RestDuration);

        if (waveData.IsThereItemsOnWave())
            UIManager.Instance.CloseStore();

        yield return Yielders.Get(.5f);
    }

}
