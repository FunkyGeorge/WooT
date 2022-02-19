using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingPlatform : MonoBehaviour
{
    [SerializeField] protected float carryDistanceCheck = 0.1f;
    [SerializeField] private float shotSpeed = 1f;
    [SerializeField] private float returnSpeed = 1f;
    [SerializeField] private float returnDelay = 0.5f;
    [SerializeField] private Vector2 shotVector;

    private Rigidbody2D rb;
    private Vector2 initialPosition;
    private Vector2 destinationPosition;
    [SerializeField] private bool isActivated = false;
    [SerializeField] private bool isReturning = false;
    private float currentDelay = 0;
    private float speedModifier = 3f;
    private float checkThreshold = 0.02f;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        initialPosition = rb.position;
        destinationPosition = rb.position + shotVector;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActivated)
        {
            if (currentDelay >= 0)
            {
                PlatformDelay();
            }
            else if (isReturning)
            {
                MoveReturn();
            }
            else
            {
                MoveSling();
            }
        }
        else
        {
            CheckForActivation();
        }
    }

    private void PlatformDelay()
    {
        currentDelay -= Time.deltaTime;
    }

    private void MoveSling()
    {
        // Find movement vector
        Vector2 moveVector;
        Vector2 currentPosition = rb.position;

        moveVector = destinationPosition - currentPosition;
        moveVector = moveVector.normalized * Time.deltaTime * shotSpeed * speedModifier;
        
        if (Mathf.Abs(rb.position.x - destinationPosition.x) < checkThreshold || rb.position.y >= destinationPosition.y)
        {
            isReturning = true;
            currentDelay = returnDelay;
        }

        rb.MovePosition(currentPosition + moveVector);
        
        MovePlayer(moveVector);
    }

    private void MoveReturn()
    {
        // Find movement vector
        Vector2 moveVector;
        Vector2 currentPosition = rb.position;

        moveVector = initialPosition - currentPosition;
        moveVector = moveVector.normalized * Time.deltaTime * returnSpeed * speedModifier;



        if (Mathf.Abs(rb.position.x - initialPosition.x) < checkThreshold || rb.position.y <= initialPosition.y)
        {
            isReturning = false;
            isActivated = false;
        }

        rb.MovePosition(currentPosition + moveVector);

        MovePlayer(moveVector);
    }

    private void MovePlayer(Vector2 moveVector)
    {
        bool shouldAdjustPlayer = false;
        RaycastHit2D[] hits = new RaycastHit2D[16];
        int hitCount = rb.Cast(moveVector, hits, carryDistanceCheck);
        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].collider.gameObject == Player.Instance.gameObject && Player.Instance.grounded)
            {
                shouldAdjustPlayer = true;
            }
        }

        // Check for momentum
        hitCount = rb.Cast(Vector2.up, hits, carryDistanceCheck);
        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].collider.gameObject == Player.Instance.gameObject && Player.Instance.grounded)
            {
                // Also move player if directly on top of platform
                shouldAdjustPlayer = true;
            }
        }

        if (shouldAdjustPlayer)
        {
            Player.Instance.PositionTweak(moveVector);
        }
    }

    private void CheckForActivation()
    {
        RaycastHit2D[] hits = new RaycastHit2D[16];
        int hitCount = rb.Cast(Vector2.up, hits, carryDistanceCheck);
        bool playerDetected = false;
        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].collider.gameObject == Player.Instance.gameObject && Player.Instance.grounded)
            {
                playerDetected = true;
            }
        }

        if (playerDetected)
        {
            isActivated = true;
        }
    }
}
