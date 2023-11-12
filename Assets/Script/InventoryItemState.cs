using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItemState : IInventoryItemState
{
    public int itemAmount;

    public bool isItemEquipped;
    public int amount { get => itemAmount; set => itemAmount = value; }
    public bool isEqipped { get => isItemEquipped; set => isItemEquipped = value; }

    public InventoryItemState() 
    {
        itemAmount = 0;
        isItemEquipped = false;
    }
}
