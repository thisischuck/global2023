using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Breach", menuName = "Waves/Breach")]
public class Breach : DescriptionBasedSO
{
    [SerializeField] List<Root> _roots;
    [SerializeField] [MinMaxRange(1, 12)] RangedFloat _angleOfAttack;
    [SerializeField] [Range(.1f,1)] float _timeOfAttack;

    public List<Root> Roots { get => _roots; }
    public RangedFloat AngleOfAttack { get => _angleOfAttack;  }
    public float TimeOfAttack { get => _timeOfAttack;  }
}

