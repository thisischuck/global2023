using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : DescriptionBasedSO
{
    [SerializeField] Sprite _storeIcon;

    public Sprite StoreIcon { get => _storeIcon; }
}

