using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] int _hitPoints;

    public int HitPoints { get => _hitPoints; set => _hitPoints = value; }

    public void DamageBase(int damageAmount)
    {
        _hitPoints -= damageAmount;
        if(_hitPoints <= 0) GameManager.Instance.Lose();
    }
}
