using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : IInventorySlot
{
    public bool isFull => !isEmpty && amount == capaciti;

    public bool isEmpty => item == null;

    public IInventoryItem item { get; private set; }

    public Type itemType => item.type;

    public int amount => isEmpty? 0 : item.state.amount;

    public int capaciti { get; private set; }

    public void Clear()
    {
        if (isEmpty) 
        {
            return;
        }
        item.state.amount = 0;
        item = null;
    }

    public void setItem(IInventoryItem item)
    {
        if (!isEmpty)
        {
            return;
        }
        this.item = item;
        this.capaciti = item.info.maxItemsInInventotySlot;
        
    }
}
