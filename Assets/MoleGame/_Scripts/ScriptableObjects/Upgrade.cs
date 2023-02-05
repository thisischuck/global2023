using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Store/Upgrade")]
public class Upgrade : Item
{
    [SerializeField] float _speedIncrement;
    [SerializeField] float _damageIncrement;
    [SerializeField] float _vignetteDecrement;
    [SerializeField] float _slideDecrement;

    public float SlideDecrement { get => _slideDecrement; }
    public float VignetteDecrement { get => _vignetteDecrement; }
    public float DamageIncrement { get => _damageIncrement; }
    public float SpeedIncrement { get => _speedIncrement; }
}
