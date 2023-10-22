using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] private int totalMoney;
    [SerializeField] private List<QuestItem> questItems;

    private void Start()
    {
        Instance = this;
        InventoryUi.Instance.InitializeUi(questItems);
    }

    public void AddItem(ShopItem item)
    {
        foreach (
            var questItem in questItems.Where(
                questItem => questItem.itemProperties.ItemId == item.Properties.ItemId)
        )
        {
            questItem.currentCount++;
            InventoryUi.Instance.RefreshUi();
        }
    }

    public void RemoveItem(ShopItem item)
    {
        foreach (
            var questItem in questItems.Where(
                questItem => questItem.currentCount > 0
                             && questItem.itemProperties.ItemId == item.Properties.ItemId)
        )
        {
            questItem.currentCount--;
            InventoryUi.Instance.RefreshUi();
        }
    }
}

// ReSharper disable UnassignedReadonlyField
[Serializable]
public class QuestItem
{
    public ItemProperties itemProperties;
    public int currentCount;
    public int amount;
}