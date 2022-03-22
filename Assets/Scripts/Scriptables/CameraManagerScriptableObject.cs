using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CameraManager", menuName = "ScriptableObject/CameraManager", order = 3)]
public class CameraManagerScriptableObject : ScriptableObject
{
     public int lensSize = 8;

    [System.NonSerialized]
    public UnityEvent<int> LensChangeEvent;

    private void OnEnable()
    {
        if (LensChangeEvent == null) { LensChangeEvent = new UnityEvent<int>(); }
        lensSize = 8;
    }

    public void SetSize(int newSize)
    {
        lensSize = newSize;
        LensChangeEvent.Invoke(lensSize);
    }
}
