using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundEffect : MonoBehaviour
{

    public MoleController Mole = null;

    public float EffectSize = 0.2f;

    public float SmoothOff = 0.95f;

    public float MaxEffectSize = 10.0f;

    private Vector2 _translationEffect = Vector2.zero;

    private Image background;

    private Color maxTint = new Color(255 / 255, 163 / 255, 181 / 255);

    private float _maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        Mole = GameManager.Instance.Player;
        background = this.GetComponent<Image>();
        _maxHealth = GameManager.Instance.BaseManager.HitPoints;
    }

    void LateUpdate()
    {
        //transform.position = Vector2.zero;
        if (Mole.GetInput().magnitude > 0)
            _translationEffect += -Mole.GetInput().normalized * EffectSize * Time.deltaTime;
        else
            _translationEffect *= SmoothOff;

        if (_translationEffect.magnitude > MaxEffectSize)
            _translationEffect = _translationEffect.normalized * MaxEffectSize;

        transform.position = new Vector3(_translationEffect.x, _translationEffect.y, 0.0f);

        float t = MathTools.Remap(GameManager.Instance.BaseManager.HitPoints, 0, _maxHealth, 1, 0);

        background.color = Color.Lerp(Color.white, maxTint, t);

        Debug.DrawLine(Vector3.zero, _translationEffect, Color.green);
    }
}
