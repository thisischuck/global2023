using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Root", menuName = "Root")]
public class Root : DescriptionBasedSO
{
    [SerializeField] RootType _type;
    [SerializeField] int _hp;
    [SerializeField] float _speed;
    [SerializeField] List<DamageTypes> _weaknesses;

    public RootType Type { get => _type;}
    public int Hp { get => _hp; set => _hp = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public List<DamageTypes> Weaknesses { get => _weaknesses; }

    /// <param name="damageType">weakness to compare against</param>
    /// <returns>true if contains weakness</returns>
    public bool IsWeakTo(DamageTypes damageType) => _weaknesses.Contains(damageType);

}

public enum RootType
{
    Taproot,
    Fibrous,
    Carrot
}

public enum DamageTypes
{
    Tnt,
    Chemical,
    Bite
}
