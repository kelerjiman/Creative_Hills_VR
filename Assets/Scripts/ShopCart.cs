using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCart : MonoBehaviour
{
    public static ShopCart Instance;
    [SerializeField] private List<ShopItem> items;
    [SerializeField] private List<Transform> grid;
    private Dictionary<int, ShopItem> _filledSlot;
    private int _lastGridIndex = 0;

    private void Awake()
    {
        Instance = this;
        _filledSlot = new Dictionary<int, ShopItem>();
    }

    public void AddToCart(ShopItem item)
    {
        items.Add(item);
        _filledSlot.Add(_lastGridIndex, item);
        item.transform.SetParent(grid[_lastGridIndex]);
        _lastGridIndex = _lastGridIndex >= grid.Count - 1 ? 0 : _lastGridIndex + 1;
        item.PickUp();
    }
}