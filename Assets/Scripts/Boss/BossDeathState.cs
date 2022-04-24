using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : BossBaseState
{
    public override void EnterState(BossStateManager stateManager)
    {
        Debug.Log("Entering Death state");
    }

    public override void UpdateState(BossStateManager stateManager)
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(BossStateManager stateManager, Collision2D collision)
    {
        
    }
}
