using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventoryItemInfo _appleInfo;
    [SerializeField] private InventoryItemInfo _mucusInfo;
    public InventoryWithSlots inventory => tester.inventory;

    private UIInventoryTester tester;
    public void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        tester = new UIInventoryTester(_appleInfo, _mucusInfo, uiSlots);
        tester.FillSlots();
    }
}
