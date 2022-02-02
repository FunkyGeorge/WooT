using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [SerializeField] private float xDelta = 0f;

    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float sweepFrequency = 2f;
    [SerializeField] private float initialSweepTime = 1f;
    private float xInitial = 0f;
    private float currentDelta = 0f;
    private float accTime = 0f;
    private bool isSweeping = false;
    private float sweepTime = 1f;
    private float nextSweep = 2f;

    // Start is called before the first frame update
    void Start()
    {
        xInitial = transform.position.x;
        sweepTime = initialSweepTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSweeping)
        {
            sweepTime -= Time.deltaTime;

            if (sweepTime < 0)
            {
                isSweeping = false;
                sweepTime = initialSweepTime;
                nextSweep = sweepFrequency;
            }
        } else {
            nextSweep -= Time.deltaTime;
            accTime += Time.deltaTime;
            if (nextSweep < 0)
            {
                isSweeping = true;
            }
            Move();
        }
        animator.SetBool("isSweeping", isSweeping);
    }

    private void Move()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        currentDelta = Mathf.PingPong(accTime * moveSpeed, xDelta);
        float newX = currentDelta + xInitial;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = newX < transform.position.x;

        rb.MovePosition(new Vector2(newX, transform.position.y));
    }
}
