using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfig : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;

    // Look at player on startup
    void Start()
    {
        vCam.Follow = Player.Instance.transform;
        vCam.LookAt = Player.Instance.transform;
    }
}
