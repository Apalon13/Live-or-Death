using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class UIInventoryTester
{
    private InventoryItemInfo _appleInfo;
    private InventoryItemInfo _mucusInfo;
    private UIInventorySlot[] _uiSlots;
    public InventoryWithSlots inventory { get; }

    public UIInventoryTester(InventoryItemInfo appleInfo, InventoryItemInfo mucusInfo, UIInventorySlot[] uiSlots)
    {
        _appleInfo = appleInfo;
        _mucusInfo = mucusInfo;
        _uiSlots = uiSlots;

        inventory = new InventoryWithSlots(24);
        inventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
    }

    public void FillSlots()
    {
        var allSlots = inventory.GetAllSlots();
        var availableSlots = new List<IInventorySlot>(allSlots);

        var fillSlots = 5;
        for (int i = 0; i < fillSlots; i++)
        {
            var fillSlot = AddRandomApplesIntoRandomSlot(availableSlots);
            availableSlots.Remove(fillSlot);

            fillSlot = AddRandomMucusIntoRandomSlot(availableSlots);
            availableSlots.Remove(fillSlot);
        }

        SetupInvetoryUI(inventory);
    }
    private IInventorySlot AddRandomApplesIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlots = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var apple = new Apple(_appleInfo);
        apple.state.amount = rCount;
        inventory.TryToAddToSlot(this, rSlots, apple);
        return rSlots;
    }
    private IInventorySlot AddRandomMucusIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlots = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var mucus = new Mucus(_mucusInfo);
        mucus.state.amount = rCount;
        inventory.TryToAddToSlot(this, rSlots, mucus);
        return rSlots;
    }

    private void SetupInvetoryUI(InventoryWithSlots inventory)
    {
        var allSlots = inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;
        for (int i  = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }
    private void OnInventoryStateChanged(object sender)
    {
        foreach (var uiSlot in _uiSlots)
        {
            uiSlot.Refresh();
        } 
    }
}
