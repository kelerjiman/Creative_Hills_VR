using System;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public static event Action<ShopItem> DetectionEvent;
    [SerializeField] private Transform origin;
    [SerializeField] private float distance;

    private void LateUpdate()
    {
        RayCast();
    }

    private ShopItem _item;

    private void RayCast()
    {
        if (Physics.Raycast(origin.position,
                origin.TransformDirection(Vector3.forward), out var hit, distance) &&
            hit.collider.gameObject.layer == LayerMask.NameToLayer($"Items"))
        {
#if UNITY_EDITOR
            Debug.DrawRay(origin.position, origin.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log("yep");
#endif
            if (_item is not null)
                if (_item.gameObject == hit.collider.gameObject) return;
                else
                    _item.Deselect();
            _item = hit.collider.gameObject.GetComponent<ShopItem>();
            _item.Select();
            DetectionEvent?.Invoke(_item);
        }
        else
        {
            if (_item is not null)
            {
                _item.Deselect();
                _item = null;
                DetectionEvent?.Invoke(null);
            }
#if UNITY_EDITOR
            Debug.DrawRay(origin.position, origin.TransformDirection(Vector3.forward) * distance, Color.red);
            Debug.Log("Not");
#endif
        }
    }
}