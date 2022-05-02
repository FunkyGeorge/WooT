using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossDeathState : BossBaseState
{
    private Vector2 destinationVector = new Vector2(-2, 9);

    public override void EnterState(BossStateManager stateManager)
    {
        stateManager.transform.DOMove(destinationVector, 3f);
        stateManager.transform.DOShakeRotation(7f);
        stateManager.StartCoroutine(Transition(stateManager.nextScene));
    }

    public override void UpdateState(BossStateManager stateManager)
    {
        
    }

    public override void OnCollisionEnter(BossStateManager stateManager, Collision2D collision)
    {
        
    }

    private IEnumerator Transition(string nextScene)
    {
        yield return new WaitForSeconds(7f);
        GameManager.Instance.FadeLoadLevel(nextScene);
    }
}
