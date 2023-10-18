using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shelve : MonoBehaviour, IInteractable
{
    [SerializeField] private CinemachineVirtualCamera cameraRoot;
    [SerializeField] private Transform cameraRootParent;
    [SerializeField] private InputAction cameraMoveAction, pickingItemAction;
    [SerializeField] private float delayAmount = 0.5f, moveThreshold = 0.01f, rotationSpeed;
    [SerializeField] private bool isCurrentDeviceMouse = true;

    [Tooltip("How far in degrees can you move the camera up")]
    public float topClamp = 90.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float bottomClamp = -90.0f;

    [Tooltip("How far in degrees can you move the camera up")]
    public float rightClamp = 90.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float leftClamp = -90.0f;

    private bool _isFocused;
    private float _cinemachineTargetPitch, _rotationVelocity;
    private Quaternion _cameraRootDefParentRot;

    private void Start()
    {
        cameraMoveAction.Disable();
        _cameraRootDefParentRot = cameraRootParent.rotation;
    }

    private void LateUpdate()
    {
        if (!_isFocused)
            return;
        CameraRotation();
        RayCast();
        DetectShelveItems();
    }

    private void DetectShelveItems()
    {
    }

    private void CameraRotation()
    {
        var lookAmount = cameraMoveAction.ReadValue<Vector2>();
        if (!(lookAmount.sqrMagnitude >= moveThreshold)) return;
        //Don't multiply mouse input by Time.deltaTime
        var deltaTimeMultiplier = isCurrentDeviceMouse ? 1.0f : Time.deltaTime;

        _cinemachineTargetPitch += -lookAmount.y * rotationSpeed * deltaTimeMultiplier;
        _rotationVelocity = lookAmount.x * rotationSpeed * deltaTimeMultiplier;

        // clamp our pitch rotation
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, bottomClamp, topClamp);

        // Update Cinemachine camera target pitch
        cameraRoot.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0, 0.0f);
        // rotate the player left and right
        cameraRootParent.Rotate(Vector3.up * _rotationVelocity);
    }

    [SerializeField] private float rayCastDistance = 1;

    private void RayCast()
    {
        if (Physics.Raycast(cameraRoot.transform.position,
            cameraRoot.transform.TransformDirection(Vector3.forward), out var hit, rayCastDistance) &&
            hit.collider.gameObject.layer == LayerMask.NameToLayer($"Items"))
        {
            Debug.DrawRay(cameraRoot.transform.position,
                cameraRoot.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(cameraRoot.transform.position,
                cameraRoot.transform.TransformDirection(Vector3.forward) * rayCastDistance, Color.red);
        }
    }

    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    public void Focus()
    {
        FocusHandler(true);
    }

    public void UnFocus()
    {
        FocusHandler(false);
    }

    private void FocusHandler(bool con)
    {
        if (con)
        {
            //after change position 
            // camera must be transition to new Pos
            CameraController.Instance.ActivateCam(cameraRoot);
            cameraMoveAction.Enable();
            pickingItemAction.Enable();
        }
        else
        {
            _isFocused = false;
            cameraMoveAction.Disable();
            pickingItemAction.Disable();
            //on this camera must be transition to default Pos
            CameraController.Instance.ActivateCam();
        }

        StartCoroutine(ToggleFocusState());
    }

    private IEnumerator ToggleFocusState()
    {
        yield return new WaitForSeconds(delayAmount);
        if (_isFocused)
        {
            _isFocused = false;
            cameraRoot.transform.parent.rotation = _cameraRootDefParentRot;
        }
        else
            _isFocused = true;
    }
}