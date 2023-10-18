using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    //public event Action<bool> ActiveInteractableCamEvent;
    [SerializeField] private Vector3 interactablePosOffset, interactablePos;
    [SerializeField] private  CinemachineVirtualCamera interactableCam,playerCam;

    private void Awake()
    {
        Instance = this;
    }

   // public void InteractableCamPos(CinemachineVirtualCamera target)
    //{
        // var newPos = target.position;
        // newPos.x += interactablePosOffset.x;
        // newPos.y += interactablePosOffset.y;
        // newPos.z += interactablePosOffset.z;
        // interactablePos = newPos;
        //interactableCam.transform.position = interactablePos;
        //interactableCam.transform.LookAt(target);
       // ActivateCam(target);
    //}

    public void ActivateCam(CinemachineVirtualCamera con=null)
    {
        //ActiveInteractableCamEvent?.Invoke(con);
        if (con is null)
        {
            playerCam.Priority = 2;
        }
        else
        {
            playerCam.Priority = 0;
            con.Priority = 1;
        }
    }
}