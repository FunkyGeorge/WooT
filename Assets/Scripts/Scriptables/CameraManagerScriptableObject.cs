using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CameraManager", menuName = "ScriptableObject/CameraManager", order = 3)]
public class CameraManagerScriptableObject : ScriptableObject
{
     public enum CameraState {
         Basic,
         Wide
     }

    [System.NonSerialized]
    public UnityEvent<CameraState> CameraChangeEvent;

    private void OnEnable()
    {
        if (CameraChangeEvent == null) { CameraChangeEvent = new UnityEvent<CameraState>(); }
    }

    public void SetCamera(CameraState cameraState)
    {
        CameraChangeEvent.Invoke(cameraState);
    }
}
