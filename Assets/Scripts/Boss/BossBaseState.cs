using UnityEngine;

public abstract class BossBaseState
{
    public abstract void EnterState(BossStateManager stateManager);
    public abstract void UpdateState(BossStateManager stateManager);
    public abstract void OnCollisionEnter(BossStateManager stateManager, Collision2D collision);
}
