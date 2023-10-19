using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using StarterAssets.FirstPersonController.Scripts;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private InputActionReference addToBucket;
    [SerializeField] private Transform bucket;
    private bool _activeShelveAction, _focusedOnShelve;
    private IInteractable _currentInteractable;

    private void OnDestroy()
    {
        PlayerRayCast.DetectionEvent -= PlayerRayCastOnDetectionEvent;
    }

    private void Start()
    {
        PlayerRayCast.DetectionEvent += PlayerRayCastOnDetectionEvent;
        //_bucketList = new List<ShopItem>();
    }

    private bool _itemDetected;
    private ShopItem _item;
    //private List<ShopItem> _bucketList;

    private void PlayerRayCastOnDetectionEvent(ShopItem item)
    {
        _itemDetected = item is not null;
        _item = item;
    }

    private void Update()
    {
        if (!_itemDetected)
            return;
        if (addToBucket is not null && addToBucket.action.WasReleasedThisFrame())
        {
            AddToBucket();
        }
    }

    private void AddToBucket()
    {
        if (!_itemDetected || _item is null)
            return;
        Debug.Log("add to Bucket");
        //_bucketList.Add(_item);
        ShopCart.Instance.AddToCart(_item);
        _item = null;
    }
}