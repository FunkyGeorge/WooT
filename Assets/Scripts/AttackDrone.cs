using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackDrone : MonoBehaviour
{
    public GameObject bossObject;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private BossStateManager bossStateManager;
    private float yRadius = 2.2f;
    private float xRadius = 5f;
    private float speed = 1f;
    public float offset = 0;
    private float shotSpeed = 0.02f;
    private float shotCooldown = 1f;
    private float lastShotTime = 0;
    private float edgeThreshold = 0.001f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        if (spriteRenderer.color.a == 0)
        {
            spriteRenderer.DOFade(1, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (bossObject && !bossStateManager)
        {
            bossStateManager = bossObject.GetComponent<BossStateManager>();
        } else {
            CirclingMovement();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bossStateManager.lastDroneDeath = Time.time;
        spriteRenderer.color = new Color(1, 1, 1, 0);
        gameObject.SetActive(false);
    }

    private void CirclingMovement()
    {
        if (bossObject)
        {
            float newX = bossObject.transform.position.x + Mathf.Cos((Time.timeSinceLevelLoad + offset) * speed) * xRadius;
            float newY = bossObject.transform.position.y + Mathf.Sin((Time.timeSinceLevelLoad + offset) * speed) * yRadius;

            if (newY < bossObject.transform.position.y)
            {
                spriteRenderer.flipX = true;
            } else {
                spriteRenderer.flipX = false;
            }

            if (Time.time - lastShotTime > shotCooldown && (newX > bossObject.transform.position.x + xRadius - edgeThreshold || newX < bossObject.transform.position.x - xRadius + edgeThreshold))
            {
                Shoot();
            }

            Vector2 newPosition = new Vector2(newX, newY);
            rb.MovePosition(newPosition);
        }
    }

    void Shoot()
    {
        lastShotTime = Time.time;

        Vector3 spawnPoint = transform.position + Vector3.down * 1.5f;
        GameObject bullet = bossStateManager.SpawnFromPool("bullets", spawnPoint);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.bodyType = RigidbodyType2D.Dynamic;
        bulletRB.velocity = Vector2.zero;

        bulletRB.AddForce(Vector2.down * shotSpeed);
    }
}
