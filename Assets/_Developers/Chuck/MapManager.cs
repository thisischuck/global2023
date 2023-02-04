using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public float Radius;

    // Start is called before the first frame update
    void Start()
    {
    }

    Vector2 angleToPos(float angleInCelsius)
    {
        //negative to acomodate our "clock" position
        return MathTools.RotateVector2(Vector2.up * Radius, -angleInCelsius);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
