using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Breach", menuName = "Waves/Breach")]
public class BreachData : DescriptionBasedSO
{
    [SerializeField] List<Root> _roots;
    [SerializeField] [MinMaxRange(0, 359)] RangedFloat _angleOfAttack;
    [SerializeField] float _firstAttackWarningDuration;
    

    public List<Root> Roots { get => _roots; }
    public RangedFloat AngleOfAttack { get => _angleOfAttack;  }
    public float FirstAttackWarningDuration { get => _firstAttackWarningDuration;  }
}

