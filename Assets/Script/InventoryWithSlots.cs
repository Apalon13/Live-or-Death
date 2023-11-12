using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWithSlots : IInventory
{
    public event Action<object, IInventoryItem, int> OnInventoryItemAddEvent;
    public event Action<object, Type, int> OnInventoryItemRemovedEvent;
    public event Action<object> OnInventoryStateChangedEvent;
    public int capacity { get; set; }
    public bool isFull => _slots.All(slot => slot.isFull);

    private List<IInventorySlot> _slots;
    public InventoryWithSlots(int capaciy)
    {
        this.capacity = capaciy;

        _slots = new List<IInventorySlot>(capaciy);
        for( int i = 0 ; i < capaciy; i++ )
        {
            _slots.Add(new InventorySlot());
        }
    }
    public IInventoryItem[] GetAllItems()
    {
        var allItems = new List<IInventoryItem>();
        foreach (var slot in _slots)
        {
            if (!slot.isEmpty)
            {
                allItems.Add(slot.item);
            }
        }
        return allItems.ToArray();
    }
    public IInventoryItem[] GetAllItems(Type itemType)
    {
        var allItemsOfType = new List<IInventoryItem>();
        var slotsOfType = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);
        foreach (var slot in slotsOfType)
        {
            allItemsOfType.Add(slot.item);
        }
        return allItemsOfType.ToArray();
    }
    public IInventoryItem[] GetEquippedItems()
    {
        var requiredSlots = _slots.FindAll(slot => !slot.isEmpty && slot.item.state.isEqipped);
        var equippedItems = new List<IInventoryItem>();
        foreach(var slot in requiredSlots)
        {
            equippedItems.Add(slot.item);
        }
        return equippedItems.ToArray();
    }
    public IInventoryItem GetItem(Type itemType)
    {
        return _slots.Find(slot => slot.itemType == itemType).item;
    }
    public int GetItemAmount(Type itemType)
    {
        var amount = 0;
        var allItemsSlots = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);
        foreach(var itemSlot in allItemsSlots)
        {
            amount += itemSlot.amount;
        }
        return amount;
    }
    public bool HasItem(Type type, out IInventoryItem item)
    {
        item = GetItem(type);
        return item != null;
    }

    public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (fromSlot.isEmpty)
        {
            return;
        }

        if (toSlot.isFull)
        {
            return;
        }

        if (!toSlot.isEmpty && fromSlot.itemType != toSlot.itemType)
        {
            return;
        }

        var slotCapaciti = fromSlot.capaciti;
        var fits = fromSlot.amount + toSlot.amount <= slotCapaciti;
        var amountToAdd = fits ? fromSlot.amount : slotCapaciti - toSlot.amount;
        var amountLeft = fromSlot.amount - amountToAdd;

        if (toSlot.isEmpty)
        {
            toSlot.setItem(fromSlot.item);
            fromSlot.Clear();
            OnInventoryStateChangedEvent?.Invoke(sender);
        }

        toSlot.item.state.amount += amountToAdd;
        if (fits)
        {
            fromSlot.Clear();
        }
        else
        {
            fromSlot.item.state.amount = amountLeft;
        }
        OnInventoryStateChangedEvent?.Invoke(sender);
    }
    public void Remove(object sender, Type itemType, int amount = 1)
    {
        var slotWithItem = GetAllSlots(itemType);
        if (slotWithItem.Length == 0)
        {
            return;
        }

        var amountToRemove = amount;
        var count = slotWithItem.Length;

        for (int i = count - 1; i >= 0; i--) 
        {
            var slot = slotWithItem[i];
            if (slot.amount >= amountToRemove)
            {
                slot.item.state.amount -= amountToRemove;

                if (slot.amount <= 0)
                {
                    slot.Clear();
                }

                Debug.Log($"Item removed from inventory. ItemType: {itemType}, amount: {amountToRemove}");
                OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
                OnInventoryStateChangedEvent?.Invoke(sender);

                break;
            }
            var amountRemoved = slot.amount;
            amountToRemove -= slot.amount;
            slot.Clear();
            Debug.Log($"Item removed from inventory. ItemType: {itemType}, amount: {amountRemoved}");
            OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountRemoved);
            OnInventoryStateChangedEvent?.Invoke(sender);
        }
    }
    public IInventorySlot[] GetAllSlots(Type itemType)
    {
        return _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType).ToArray();
    }
    public bool TryToAdd(object sender, IInventoryItem item)
    {
        var slotWithSameItemButNotEmpty = _slots.Find(slot => !slot.isEmpty && slot.itemType == item.type && !slot.isFull);
        if (slotWithSameItemButNotEmpty != null)
        {
            return TryToAddToSlot(sender, slotWithSameItemButNotEmpty, item);
        }

        var emptySlot = _slots.Find(slot => slot.isEmpty);
        if (emptySlot != null)
        {
            return TryToAddToSlot(sender, emptySlot, item);
        }

        return false;
    }
    public bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        var fits = slot.amount + item.state.amount <= item.info.maxItemsInInventotySlot;
        var amountToAdd = fits ? item.state.amount : item.info.maxItemsInInventotySlot - slot.amount;
        var amountLeft = item.state.amount - amountToAdd;
        var clonedItem = item.Clone();
        clonedItem.state.amount = amountToAdd;

        if (slot.isEmpty)
        {
            slot.setItem(clonedItem);
        }
        else
        {
            slot.item.state.amount += amountToAdd;
        }

        Debug.Log($"Item added to inventory. ItemType: {item.type}, amount: {amountToAdd}");
        OnInventoryItemAddEvent?.Invoke(sender, item, amountToAdd);
        OnInventoryStateChangedEvent?.Invoke(sender);

        if (amountLeft <= 0)
        {
            return true;
        }

        item.state.amount = amountLeft;
        return TryToAdd(sender, item);
    }
    public IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }
}
