using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    IInventoryItemInfo info { get; }
    IInventoryItemState state { get; }
    Type type { get;}
    IInventoryItem Clone();
}
