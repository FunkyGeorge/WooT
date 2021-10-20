using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [Header("Attributes")]
    [SerializeField] private bool patrolMode = true;
    [SerializeField] private float speed;
    [SerializeField] private int direction = 1;
    private bool isSleeping = false;

    [Header("Ray Config")]
    [SerializeField] private float rayCastLength = 1f;
    [SerializeField] private Vector2 raycastOffset = new Vector2(0, 0);
    [SerializeField] private LayerMask raycastLayerMask;
    private RaycastHit2D raycastHit;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D enemyCollider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isSleeping)
        {
            
            if (patrolMode)
            {
                CheckAround();
                targetVelocity.x = speed * direction;
            }
            animator.SetFloat("movement", Mathf.Abs(targetVelocity.x));
        }
    }

    private void CheckAround()
    {
        targetVelocity = new Vector2(speed * direction, 0);

        // Check left ledge
        raycastHit = Physics2D.Raycast(new Vector2(transform.position.x - raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down, rayCastLength);
        //Debug.Log(raycastHit.collider.gameObject.name);
        Debug.DrawRay(new Vector2(transform.position.x - raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down * rayCastLength, Color.green);
        if (!raycastHit)
        {
            direction = 1;
            transform.localScale = new Vector2(1, 1);
        }

        // Check left wall
        raycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, rayCastLength, raycastLayerMask);
        //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * rayCastLength, Color.cyan);
        if (raycastHit && raycastHit.collider != null)
        {
            direction = 1;
            transform.localScale = new Vector2(1, 1);
        }

        // Check right ledge
        raycastHit = Physics2D.Raycast(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down, rayCastLength);
        //Debug.Log(raycastHit.collider.gameObject.name);
        Debug.DrawRay(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down * rayCastLength, Color.red);
        if (!raycastHit)
        {
            direction = -1;
            transform.localScale = new Vector2(-1, 1);
        }

        // Check right wall
        raycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, rayCastLength, raycastLayerMask);
        //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * rayCastLength, Color.blue);
        if (raycastHit && raycastHit.collider != null)
        {
            direction = -1;
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private void Awaken()
    {
        isSleeping = false;
        animator.SetBool("isSleeping", false);
        enemyCollider.size = new Vector2(0.85f, 1.5f);
        enemyCollider.offset = Vector2.zero;
    }

    private void GoToSleep()
    {
        isSleeping = true;
        animator.SetBool("isSleeping", true);
        targetVelocity.x = 0;
        enemyCollider.size = new Vector2(1.5f, 0.3f);
        enemyCollider.offset = new Vector2(0, -0.6f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isSleeping && collision.gameObject.tag == "Projectile")
        {
            GoToSleep();
        }

        if (collision.gameObject == Player.Instance.gameObject)
        {
            if (isSleeping)
            {
                Invoke("Awaken", 1f);
            }
            else
            {
                Player.Instance.Die();
            }
        }
    }
}
