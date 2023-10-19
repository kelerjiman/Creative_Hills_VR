using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCart : MonoBehaviour
{
    public static ShopCart Instance;
    [SerializeField] private List<ShopItem> items;
    [SerializeField] private List<Transform> grid;

    private void Awake()
    {
        Instance = this;
    }

    public void AddToCart(ShopItem item)
    {
        items.Add(item);

        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.PickUp();
    }
}