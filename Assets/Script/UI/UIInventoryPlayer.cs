using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class UIInventoryPlayer
{
    private InventoryItemInfo _slimeRingInfo;
    private UIInventorySlot[] _uiSlots;
    public InventoryWithSlots inventory { get; }

    public UIInventoryPlayer(UIInventorySlot[] uiSlots, InventoryItemInfo slimeRingInfo)
    {
        _slimeRingInfo = slimeRingInfo;
        _uiSlots = uiSlots;

        inventory = new InventoryWithSlots(24);
        inventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
    }
    private IInventorySlot AddI(List<IInventorySlot> slots)
    {
        var rSlotIntdex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIntdex];
        var slimeRing = new SlimeRing(_slimeRingInfo);
        slimeRing.state.amount = 1;
        inventory.TryToAddToSlot(this, rSlot, slimeRing);
        return rSlot;
    }
    public void SetupInvetoryUI(InventoryWithSlots inventory)
    {
        var allSlots = inventory.GetAllSlots();
        var avaibleSlot = new List<IInventorySlot>(allSlots);
        var allSlotsCount = allSlots.Length;
        AddI(avaibleSlot);
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
