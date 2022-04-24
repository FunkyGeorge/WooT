using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAwakeningState : BossBaseState
{
    private Vector2 destinationVector = new Vector2(-2, 9);
    private float awakeningSpeed = 0.02f;
    private float awakeningRotateSpeed = 0.2f;
    private bool isDoneShaking = false;
    private int currentRotationPointIndex = 0;
    public override void EnterState(BossStateManager stateManager)
    {
        Debug.Log("Entered Awakening state");
        stateManager.health = 100;
    }

    public override void UpdateState(BossStateManager stateManager)
    {
        if (!isDoneShaking)
        {
            Shake(stateManager);
        } else {
            stateManager.transform.position = Vector2.MoveTowards(stateManager.rb.position, destinationVector, awakeningSpeed);
            if (stateManager.transform.position.x == destinationVector.x)
            {
                stateManager.SwitchState(stateManager.stage1State);
            }
        }
    }

    public override void OnCollisionEnter(BossStateManager stateManager, Collision2D collision)
    {
        Debug.Log("do nothing");
    }

    private void Shake(BossStateManager stateManager)
    {
        switch (currentRotationPointIndex) {
            case 0:
                stateManager.transform.rotation = Quaternion.RotateTowards(stateManager.transform.rotation, Quaternion.Euler(0, 0, -10), awakeningRotateSpeed);
                if (stateManager.transform.rotation == Quaternion.Euler(0, 0, -10))
                {
                    currentRotationPointIndex++;
                }
                break;
            case 1:
                stateManager.transform.rotation = Quaternion.RotateTowards(stateManager.transform.rotation, Quaternion.Euler(0, 0, 10), awakeningRotateSpeed);
                if (stateManager.transform.rotation == Quaternion.Euler(0, 0, 10))
                {
                    currentRotationPointIndex++;
                }
                break;
            case 2:
                stateManager.transform.rotation = Quaternion.RotateTowards(stateManager.transform.rotation, Quaternion.Euler(0, 0, 0), awakeningRotateSpeed);
                if (stateManager.transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    currentRotationPointIndex++;
                }
                break;
            default:
                isDoneShaking = true;
                break;
        }
        
    }
}
