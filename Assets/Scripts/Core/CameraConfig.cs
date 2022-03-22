using UnityEngine;
using Cinemachine;

public class CameraConfig : MonoBehaviour
{
    [SerializeField] private CameraManagerScriptableObject cameraManager;
    [SerializeField] private DeathManagerScriptableObject deathManager;
    [SerializeField] private CinemachineStateDrivenCamera vCam;
    [SerializeField] private Animator animator;
    // Look at player on startup
    void Start()
    {
        vCam.Follow = Player.Instance.transform;
        vCam.LookAt = Player.Instance.transform;

        if (deathManager && cameraManager)
        {
            deathManager.deathEvent.AddListener(ResetCamera);
            cameraManager.CameraChangeEvent.AddListener(SetCamera);
        }
        else
        {
            Debug.LogWarning("Death Manager or Camera Manager not set");
        }
    }

    void OnDisable()
    {
        deathManager.deathEvent.RemoveListener(ResetCamera);
        cameraManager.CameraChangeEvent.RemoveListener(SetCamera);
    }

    private void SetCamera(CameraManagerScriptableObject.CameraState cameraState)
    {
        switch (cameraState) 
        {
            case CameraManagerScriptableObject.CameraState.Basic:
                animator.Play("Basic");
                break;
            case CameraManagerScriptableObject.CameraState.Wide:
                animator.Play("Wide");
                break;
            default:
                animator.Play("Basic");
                break;
        }
    }

    private void ResetCamera(int deathCount)
    {
        cameraManager.SetCamera(CameraManagerScriptableObject.CameraState.Basic);
    }
}
