using UnityEngine;
using Cinemachine;

public class CameraConfig : MonoBehaviour
{
    [SerializeField] private CameraManagerScriptableObject cameraManager;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private DeathManagerScriptableObject deathManager;
    [SerializeField] private CinemachineVirtualCamera virtualCam;
    // Look at player on startup
    void Start()
    {
        vCam.Follow = Player.Instance.transform;
        vCam.LookAt = Player.Instance.transform;

        if (deathManager && cameraManager)
        {
            deathManager.deathEvent.AddListener(ResetLens);
            cameraManager.LensChangeEvent.AddListener(SetLens);
        }
        else
        {
            Debug.LogWarning("Death Manager or Camera Manager not set");
        }
    }

    void OnDisable()
    {
        deathManager.deathEvent.RemoveListener(ResetLens);
        cameraManager.LensChangeEvent.RemoveListener(SetLens);
    }

    private void SetLens(int lensSize)
    {
        virtualCam.m_Lens.OrthographicSize = lensSize;
    }

    private void ResetLens(int deathCount)
    {
        virtualCam.m_Lens.OrthographicSize = 8;
    }
}
