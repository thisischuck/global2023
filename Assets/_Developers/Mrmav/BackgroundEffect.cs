using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEffect : MonoBehaviour
{

    public MoleController Mole = null;

    public float EffectSize = 0.2f;

    public float SmoothOff = 0.95f;

    public float MaxEffectSize = 10.0f;

    private Vector2 _translationEffect = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        Mole = GameManager.Instance.Player;
    }

    void LateUpdate()
    {
        //transform.position = Vector2.zero;

        if (Mole.GetInput().magnitude > 0)
            _translationEffect += -Mole.GetInput().normalized * EffectSize * Time.deltaTime;
        else
            _translationEffect *= SmoothOff;

        if(_translationEffect.magnitude > MaxEffectSize)
            _translationEffect = _translationEffect.normalized * MaxEffectSize;

        transform.position = new Vector3(_translationEffect.x, _translationEffect.y, 0.0f);

        Debug.DrawLine(Vector3.zero, _translationEffect, Color.green);
    }
}
