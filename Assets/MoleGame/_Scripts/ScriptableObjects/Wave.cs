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


    public bool IsThereItemsOnWave() 
    {
        if(_preWaveItems.Count == 0) return false;
        return true;
    }

    
    public List<Item> PreWaveItems { get => _preWaveItems;}
    public float RestDuration { get => _restDuration;  }
    public List<Breach> Breaches { get => _breaches;  }
    public float Duration 
    { 
        get 
        {
            var duration = 0f;
            foreach (var item in _breaches)
                duration += item.attackDelay;
            return duration;
        }  
    }
}

    [System.Serializable]
    public class Breach
    {
        [SerializeField] BreachData _breachData;
        [SerializeField] [Range(0f, 5f)] float _attackDelay;

        public float attackDelay { get => _attackDelay; }
        public BreachData BreachData { get => _breachData;  }
    }

