using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DeathManager", menuName = "ScriptableObject/DeathManager", order = 2)]
public class DeathManagerScriptableObject : ScriptableObject
{
    public int deathCount = 0;

    [System.NonSerialized]
    public UnityEvent<int> deathEvent;

    private void OnEnable()
    {
        deathCount = Prefs.GetDeathCount();
        if (deathEvent == null) { deathEvent = new UnityEvent<int>(); }
    }

    public void Death()
    {
        deathCount++;
        Prefs.SetDeathCount(deathCount);
        deathEvent.Invoke(deathCount);
    }
}
