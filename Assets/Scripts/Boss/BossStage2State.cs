using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossStage2State : BossBaseState
{
    private float speed = 0.7f;
    private float range = 5f;
    private float shotSpeed = 0.01f;
    private float shotCooldown = 1f;
    private bool verticalShotType = false;
    private float lastShotTime = 0;
    private int droneAmount = 4;
    private float respawnCooldown = 7f;
    public override void EnterState(BossStateManager stateManager)
    {
        Color redColor = new Color(0.8784314f, 0.2156863f, 0.1921569f, 1f);
        stateManager.spriteRenderer.DOColor(redColor, 0.2f).SetLoops(6, LoopType.Yoyo);


        SpawnDrones(stateManager);
        lastShotTime = Time.time;
    }

    public override void UpdateState(BossStateManager stateManager)
    {
        if (stateManager.health <= 0)
        {
            DespawnAllDrones(stateManager);
            stateManager.SwitchState(stateManager.deathState);
        }

        Move(stateManager);
        Shoot(stateManager);

        if (Time.time - stateManager.lastDroneDeath > respawnCooldown && Time.time - stateManager.lastDroneSpawnTime > respawnCooldown)
        {
            SpawnDrones(stateManager);
        }
    }

    public override void OnCollisionEnter(BossStateManager stateManager, Collision2D collision)
    {
        stateManager.health -= 10;

        stateManager.spriteRenderer.DOColor(Color.HSVToRGB(0, 0, 0.5f), 0.1f).SetLoops(6, LoopType.Yoyo);
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
            verticalShotType = !verticalShotType;

            Vector3 spawnPoint = stateManager.transform.position + Vector3.down * 2.5f;

            if (verticalShotType)
            {
                GameObject middleBullet = stateManager.SpawnFromPool("bullets", spawnPoint);
                Rigidbody2D middleRB = middleBullet.GetComponent<Rigidbody2D>();
                middleRB.bodyType = RigidbodyType2D.Dynamic;
                middleRB.velocity = Vector2.zero;
                middleRB.AddForce(Vector2.down * shotSpeed);
            } else {
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

                leftRB.AddForce((leftSpawn - stateManager.transform.position).normalized * shotSpeed * 1.41f);
                rightRB.AddForce((rightSpawn - stateManager.transform.position).normalized * shotSpeed * 1.41f);
            }
        }
    }

    void SpawnDrones(BossStateManager stateManager)
    {
        stateManager.lastDroneSpawnTime = Time.time;
        for (int i = 0; i < droneAmount; i++)
        {
            GameObject newDrone = stateManager.SpawnFromPool("drones", stateManager.rb.transform.position);
            AttackDrone droneScript = newDrone.GetComponent<AttackDrone>();

            if (i == 1)
            {
                droneScript.offset = Mathf.PI;
            }
            else if (i == 2)
            {
                droneScript.offset = Mathf.PI/2;
            }
            else if (i == 3)
            {
                droneScript.offset = -Mathf.PI/2;
            }

            droneScript.bossObject = stateManager.gameObject;
        }
    }

    void DespawnAllDrones(BossStateManager stateManager)
    {
        for (int i = 0; i < droneAmount; i++)
        {
            GameObject newDrone = stateManager.SpawnFromPool("drones", stateManager.rb.transform.position);
            newDrone.SetActive(false);
        }
    }
}
