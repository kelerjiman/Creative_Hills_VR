using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReviewCanvas : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    private ShopItem _detectedItem;

    private void Start()
    {
        PlayerRayCast.DetectionEvent += PlayerRayCastOnDetectionEvent;
    }

    private void LateUpdate()
    {
        if (_detectedItem is null)
            return;
        //transform.LookAt(player);
        //RotationHandler();
    }

    private void PlayerRayCastOnDetectionEvent(ShopItem item)
    {
        // if (item is not null)
        // {
        //     transform.localPosition = item.transform.position + offset;
        // }

        _detectedItem = item;
        mainMenu.SetActive(item is not null);
    }

    private void RotationHandler()
    {
        var tempTransform = transform;
        var tempEuler = tempTransform.localRotation.eulerAngles;
        tempEuler.x = 0;
        tempEuler.z = 0;
        tempTransform.eulerAngles = tempEuler;
    }
}