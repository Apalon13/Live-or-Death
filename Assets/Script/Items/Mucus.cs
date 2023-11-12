using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Mucus : IInventoryItem
{
    public Type type => GetType();

    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }

    public Mucus(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }

    public IInventoryItem Clone()
    {
        var cloneMucus = new Mucus(info);
        cloneMucus.state.amount = state.amount;
        return cloneMucus;
    }
}
