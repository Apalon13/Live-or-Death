using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventoryItemInfo slimeRingInfo;
    public InventoryWithSlots inventory => playerInventory.inventory;

    private UIInventoryPlayer playerInventory;
    public void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        playerInventory = new UIInventoryPlayer(uiSlots, slimeRingInfo);
        playerInventory.SetupInvetoryUI(inventory);
        
    }
}
