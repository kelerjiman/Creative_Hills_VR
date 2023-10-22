using System;
using TMPro;
using UnityEngine;

public class QuestUiTick : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName, itemPrice,itemNeedCount, itemCurrentCount, cost;
    [SerializeField] private GameObject tick;
    private QuestItem _questItem;

    public void InitializeTick(QuestItem qItem)
    {
        _questItem = qItem;
        itemName.text = _questItem.itemProperties.Name;
        //itemPrice.text = _questItem.ItemProperties.ItemPrice.ToString();
        itemCurrentCount.text = _questItem.currentCount.ToString();
        itemNeedCount.text = _questItem.amount.ToString();
        cost.text = (_questItem.itemProperties.ItemPrice * _questItem.currentCount).ToString();
    }

    public void RefreshTick()
    {
        itemCurrentCount.text = _questItem.currentCount.ToString();
        cost.text = (_questItem.itemProperties.ItemPrice * _questItem.currentCount).ToString();
        tick.SetActive(_questItem.amount <= _questItem.currentCount);
    }
}