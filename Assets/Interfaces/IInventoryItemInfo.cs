using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItemInfo
{
    int maxItemsInInventotySlot { get; }
    string id { get; }
    string title { get; }
    string description { get; }
    Sprite spriteIcoin {  get; }
}
