using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance 
    {
        get 
        { 
            if(_instance == null)
            Debug.LogError("No UI Manager Available.");
            return _instance;
        } 
    }
    private void Awake() => _instance = this;
    #endregion

    [SerializeField] TMP_Text _timer;
    [SerializeField] bool _isDisplayingTimer;
    public string TimerTxt { get => _timer.text; set => _timer.text = value; }
    private float _t;

    public void StartTimer(float timerDuration)
    {
        if(_isDisplayingTimer) return;
        _t = timerDuration;
        _isDisplayingTimer = true;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(_isDisplayingTimer)
        {
            if(_t > 0)
            {
                _t -= Time.deltaTime;
            }else
            {
                _t = 0;
                _isDisplayingTimer = false;
                _timer.enabled = false;
            }
            TimerTxt = Mathf.Round(_t).ToString();
        }
        else
        {
           
        }
    }
}
