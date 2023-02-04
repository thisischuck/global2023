using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Waves/Wave")]
public class Wave : DescriptionBasedSO
{
    [Header("Rest")]
    [SerializeField] List<Item> _preWaveItems;
    [SerializeField] float _restDuration = 10f;
    
    [Header("Defense")]
    [SerializeField] List<Breach> _breaches;
    [SerializeField] float _duration = 20f;

    public bool IsThereItemsOnWave() 
    {
        if(_preWaveItems.Count < 1) return false;
        return true;
    }

    

    public List<Item> PreWaveItems { get => _preWaveItems;}
    public float RestDuration { get => _restDuration;  }
    public List<Breach> Breaches { get => _breaches;  }
    public float Duration { get => _duration;  }
}

