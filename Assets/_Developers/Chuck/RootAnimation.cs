using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootAnimation : MonoBehaviour
{
    public Root rootData;
    public Vector2 targetPosition;
    public GameObject parent;
    Vector2 currentPosition;
    Vector2 currentDirection;

    float _health;

    [SerializeField] GameObject _temp;
    float _stunDuration = 1.2f;
    bool _isStunned = false;
    LineRenderer r;

    public bool IsStunned { get => _isStunned;  }

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = this.transform.localPosition;
        r = this.GetComponent<LineRenderer>();
        //targetPosition = this.transform.InverseTransformPoint(targetPosition);
        currentDirection = (targetPosition - currentDirection).normalized;
        StartCoroutine(Frame());
        _health = rootData.Hp;
    }

    public bool LoseHealth(float dano)
    {
        StartCoroutine(COR_GetStunned());
        _health -= dano;
        if(_health < 0)
        {
            Death();
            return true;
        }
        return false;
    }

    IEnumerator Frame()
    {
        while (Vector2.Distance(targetPosition, currentPosition) > 3f)
        {
            //get a new position randomly withing the new threshold cone.
            //chose an angle to go towards;
            float angleCelcius = Random.Range(-rootData.ConeAngle, rootData.ConeAngle);
            Vector2 dir = targetPosition - currentPosition;
            //dir = dir.normalized * 0.3f + currentDirection * 0.7f;
            Vector2 dirRotated = MathTools.RotateVector2(dir.normalized, angleCelcius);
            Vector2 newPosition = currentPosition + dirRotated * rootData.Speed;
            currentDirection = dirRotated;
            currentPosition = newPosition;
            //create a new square in
            var i = r.positionCount;
            r.positionCount += 1;
            r.SetPosition(i, newPosition);
            yield return Yielders.Get(rootData.GrowthRate);
            //cry
        }
        
        Debug.Log("Stopped");
        EnableCollisions();

        //Apply Vanish Maybe
        // Damage Base if stopped, and not stunned
        while(!_isStunned)
        {
            GameManager.Instance.BaseManager.DamageBase(rootData.AttackDamage);
            yield return Yielders.Get(rootData.AttackStep);
        }
    }

    private void EnableCollisions()
    {
        var o = Instantiate(_temp, currentPosition, Quaternion.identity, this.transform);
        var collider = (CircleCollider2D)o.AddComponent(typeof(CircleCollider2D));
        collider.isTrigger = true;
        collider.radius = 1f;
        o.name = "Trigger";
        o.tag = this.tag;
    }

    private IEnumerator COR_GetStunned()
    {
        _isStunned = true;
        yield return Yielders.Get(_stunDuration);
        _isStunned = false;
    }

    public void Death()
    {
        GameManager.Instance.RootSystem.RemoveRoot(this); // This sucks, but works.
        Destroy(this.gameObject);
    }
}
