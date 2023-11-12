using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public interface IInventorySlot
{
    bool isFull {  get; }
    bool isEmpty { get; }
    IInventoryItem item { get; }
    Type itemType { get; }
    int amount { get; }
    int capaciti { get; }
    void setItem(IInventoryItem item);
    void Clear();
}
