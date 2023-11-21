using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SlimeRing : IInventoryItem
{
    public Type type => GetType();

    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }

    public SlimeRing(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }

    public IInventoryItem Clone()
    {
        var cloneSlimeRing = new SlimeRing(info);
        cloneSlimeRing.state.amount = state.amount;
        return cloneSlimeRing;
    }
}

