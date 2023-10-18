using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using StarterAssets.FirstPersonController.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private FirstPersonController controller;

    [SerializeField] private InputActionReference move, look, jump, focusToShelves, unFocusFromShelves;

    private bool _activeShelveAction, _focusedOnShelve;
    private IInteractable _currentInteractable;

    // private void Start()
    // {
    //     CameraController.Instance.ActiveInteractableCamEvent+= InstanceOnActiveInteractableCamEvent;
    // }

    // private void InstanceOnActiveInteractableCamEvent(bool obj)
    // {
    //     playerCamera.enabled = !obj;
    //     playerCamera.tag = !obj ? "MainCamera" : "Untagged";
    // }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            _currentInteractable = other.gameObject.GetComponentInParent<IInteractable>();
            _activeShelveAction = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Interactable")) return;
        if (_currentInteractable is null) return;
        _currentInteractable = _currentInteractable == other.gameObject.GetComponentInParent<IInteractable>()
            ? null
            : _currentInteractable;
        _activeShelveAction = false;
    }

    private void Update()
    {
        if (!_activeShelveAction)
            return;
        if (focusToShelves.action.IsPressed() && !_focusedOnShelve)
        {
            _currentInteractable.Focus();
            _focusedOnShelve = true;
            move.action.Disable();
            look.action.Disable();
            jump.action.Disable();
        }

        if (unFocusFromShelves.action.IsPressed() && _focusedOnShelve)
        {
            _currentInteractable.UnFocus();
            _focusedOnShelve = false;
            move.action.Enable();
            look.action.Enable();
            jump.action.Enable();
        }
    }
}