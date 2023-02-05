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

    LineRenderer r;

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
        _health -= dano;
        return _health < 0;
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
            yield return new WaitForSecondsRealtime(rootData.GrowthRate);
            //cry
        }
        Debug.Log("Stopped");
        //Apply Vanish Maybe
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}
