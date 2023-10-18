using System;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Outline outLine;
    private bool _isActivated = true;
    [SerializeField] private Rigidbody rigidbody;

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
        rigidbody.isKinematic = true;
        _isActivated = false;
        outLine.enabled = false;
    }

    public void Drop()
    {
    }
}