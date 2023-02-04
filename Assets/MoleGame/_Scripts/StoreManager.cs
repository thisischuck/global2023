using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField] GameObject _slotsContainer;
    [SerializeField] List<StoreSlot> _itemSlots;

    public void UpdateAvailableStock(List<Item> stock)
    {
        ClearStock();
        if(stock == null)
        {
            Debug.Log("Cleaning Store Stock.");
            return;
        } 

        if(stock.Count > _itemSlots.Count) Debug.LogWarning("Pool does not contain " + stock.Count + " slots.");

        for (int i = 0; i < stock.Count; i++)
        {
            _itemSlots[i].IsAvailable = true;
            _itemSlots[i].SlotImage.sprite = stock[i].StoreIcon;
            _itemSlots[i].SlotItem = stock[i];
            _itemSlots[i].SlotImage.enabled = true;
        }
    
    }

    public void ShowStore() => _slotsContainer.SetActive(true);

    public void HideStore() => _slotsContainer.SetActive(false);

    public Item GetSlotItem(float angleOfInteraction)
    {
        foreach (var slot in _itemSlots)
        {
            if(slot.IsInteractable(angleOfInteraction))
            {
                slot.IsAvailable = false;
                slot.SlotImage.sprite = null;

                return slot.SlotItem;
            } 
        }
        return null;
    }
    
    private void ClearStock()
    {
        for (int i = 0; i < _itemSlots.Count; i++)
        {
            _itemSlots[i].IsAvailable = false;
            _itemSlots[i].SlotImage.sprite = null;
            _itemSlots[i].SlotItem = null;

            _itemSlots[i].SlotImage.enabled = false;
        }
        
    }
}


[System.Serializable]
public class StoreSlot
{
    [SerializeField] Image _slotImage;
    [SerializeField] [MinMaxRange(0, 359)] RangedFloat _interactableAngleRange;
    
    [Header("ShowOnly")]
    [SerializeField] bool _isAvailable = false; 
    [SerializeField] Item _slotItem;

    public bool IsInteractable(float angle)
    {
        if(!IsAvailable) return false;
        
        if(angle > _interactableAngleRange.minValue && angle < _interactableAngleRange.maxValue) return true;

        return false;
    }
    public Image SlotImage { get => _slotImage;  set => _slotImage = value;  }
    public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }
    public Item SlotItem { get => _slotItem; set => _slotItem = value; }
    public RangedFloat InteractableAngleRange { get => _interactableAngleRange;  }
}

