using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage1State : BossBaseState
{
    private float speed = 0.7f;
    private float shotSpeed = 0.02f;
    private float range = 5f;
    private float shotCooldown = 1.7f;
    private float lastShotTime = 0;

    public override void EnterState(BossStateManager stateManager)
    {
        Debug.Log("Enter Stage 1");
        stateManager.stageStartTime = Time.time;
        lastShotTime = Time.time;
    }

    public override void UpdateState(BossStateManager stateManager)
    {
        if (stateManager.health <= 50)
        {
            stateManager.SwitchState(stateManager.stage2State);
        }
        Move(stateManager);
        Shoot(stateManager);
    }

    public override void OnCollisionEnter(BossStateManager stateManager, Collision2D collision)
    {
        if (stateManager.health > 50)
        {
            stateManager.health -= 10;
        }
    }

    private void Move(BossStateManager stateManager)
    {
        stateManager.transform.position = new Vector3(-Mathf.Sin((Time.time - stateManager.stageStartTime) * speed) * range - 2, 9, 0);
    }

    private void Shoot(BossStateManager stateManager)
    {
        if (Time.time - lastShotTime >= shotCooldown)
        {
            // Fire
            lastShotTime = Time.time;

            Vector3 spawnPoint = stateManager.transform.position + Vector3.down * 2.5f;

            GameObject middleBullet = stateManager.SpawnFromPool("bullets", spawnPoint);
            Rigidbody2D middleRB = middleBullet.GetComponent<Rigidbody2D>();
            middleRB.bodyType = RigidbodyType2D.Dynamic;
            middleRB.velocity = Vector2.zero;

            Vector3 leftSpawn = spawnPoint + Vector3.left * 2.5f;
            GameObject leftBullet = stateManager.SpawnFromPool("bullets", leftSpawn);
            Rigidbody2D leftRB = leftBullet.GetComponent<Rigidbody2D>();
            leftRB.bodyType = RigidbodyType2D.Dynamic;
            leftRB.velocity = Vector2.zero;

            Vector3 rightSpawn = spawnPoint + Vector3.right * 2.5f;
            GameObject rightBullet = stateManager.SpawnFromPool("bullets", rightSpawn);
            Rigidbody2D rightRB = rightBullet.GetComponent<Rigidbody2D>();
            rightRB.bodyType = RigidbodyType2D.Dynamic;
            rightRB.velocity = Vector2.zero;

            middleRB.AddForce(Vector2.down * shotSpeed);
            leftRB.AddForce((leftSpawn - stateManager.transform.position).normalized * shotSpeed * 1.41f);
            rightRB.AddForce((rightSpawn - stateManager.transform.position).normalized * shotSpeed * 1.41f);
        }
    }
}
