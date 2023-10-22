using System;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private ItemProperties properties;
    public ItemProperties Properties => properties;
    [SerializeField] private Outline outLine;
    private bool _isActivated = true;
    public bool IsActivated => _isActivated;
    [SerializeField] private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody ??= GetComponent<Rigidbody>();
    }

    public void Select()
    {
        if (_isActivated)
            outLine.enabled = true;
    }

    public void Deselect()
    {
        if (_isActivated)
            outLine.enabled = false;
    }

    public void PickUp()
    {
        transform.localPosition = Vector3.zero;
        rigidbody.isKinematic = true;
        _isActivated = false;
        outLine.enabled = false;
    }

    public void Drop()
    {
    }
}