using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField] StatsManager _statsManager;
    [SerializeField] StoreManager _store;
    [SerializeField] Image _warningCircle;
    [SerializeField] TMP_Text _timer;
     private bool _isDisplayingTimer;
    private float _t;
    public string TimerTxt { get => _timer.text; set => _timer.text = value; }


    private void Start()
    {
        DisableBreachWarning();
        HideStats();
    }

    public void StartTimer(float timerDuration)
    {
        if(_isDisplayingTimer) return;
        _timer.enabled = true;
        _t = timerDuration;
        _isDisplayingTimer = true;
    }
#region  Stats
    public void DisplayStats()
    {
        var gameManager = GameManager.Instance;
        _statsManager.UpdateStats(gameManager.RootsAnnihilated,gameManager.WavesConquered, gameManager.TimeFighting,gameManager.TimeSleeping);
        _statsManager.gameObject.SetActive(true);
    }

    public void HideStats()
    {
        _statsManager.gameObject.SetActive(false);
    }
#endregion
    public void EnableBreachWarning(RangedFloat angleOfAttack)
    {
        _warningCircle.enabled = true;
    }
    
    [ContextMenu("Breach Warning")]
    public void DisableBreachWarning()
    {
        _warningCircle.enabled = false;
    }


    [ContextMenu("Open Store")]
    public void OpenStore(List<Item> stock)
    {
        _store.UpdateAvailableStock(stock);
        _store.ShowStore();
    }

    [ContextMenu("Close Store")]
    public void CloseStore()
    {
        _store.UpdateAvailableStock(null);
        _store.HideStore();
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
    }

}
