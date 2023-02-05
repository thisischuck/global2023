using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    [SerializeField] TMP_Text _rootsAnnihilated;
    [SerializeField] TMP_Text _wavesConquered;
    [SerializeField] TMP_Text _timeFighting;
    [SerializeField] TMP_Text _timeSleeping;

  
    public void UpdateStats(int rootsAnnihilatedAmount, int wavesConqueredAmount, int timeFightingAmount, int timeSleepingAmount)
    {
        _rootsAnnihilated.text = _rootsAnnihilated.text + " " + rootsAnnihilatedAmount;
        _wavesConquered.text = _wavesConquered.text + " " + wavesConqueredAmount;
        _timeFighting.text = _timeFighting.text + " " + timeFightingAmount;
        _timeSleeping.text = _timeSleeping.text + " " + timeSleepingAmount;
    }
}
