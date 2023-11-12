using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "GamePlay/Items/Create New ItemInfo")]
public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _spriteIcoin;
    [SerializeField] private int _maxItemsInInventotySlot;
    public int maxItemsInInventotySlot => _maxItemsInInventotySlot;

    public string id => _id;

    public string title => _title;

    public string description => _description;

    public Sprite spriteIcoin => _spriteIcoin;
}
