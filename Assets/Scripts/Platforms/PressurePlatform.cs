using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressurePlatform : CarryingPlatform
{
    [SerializeField] private Vector2 destinationVector = new Vector2(0, 0);
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private bool isActivated = false;
    [SerializeField] private bool isReversed = false;
    private bool isGoingToReset = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActivated)
        {
            PlatformMovement();
            if (Player.Instance.isDying && !isGoingToReset)
            {
                isGoingToReset = true;
                // Time is based on time it takes for transition to cover the screen
                Invoke("ResetState", 0.6f);
            }
        }
        else
        {
            CheckForActivation();
        }
    }

    private void PlatformMovement()
    {
        float newX = 0;
        float newY = 0;
        
        if (isReversed)
        {
            if (destinationVector.x >= 0)
            {
                newX = Mathf.Max(rb.position.x - moveSpeed * Time.deltaTime, 0);
            }
            else
            {
                newX = Mathf.Min(rb.position.x + moveSpeed * Time.deltaTime, 0);
            }

            if (destinationVector.y >= 0)
            {
                newY = Mathf.Max(rb.position.y - moveSpeed * Time.deltaTime, 0);
            }
            else
            {
                newY = Mathf.Min(rb.position.y + moveSpeed * Time.deltaTime, 0);
            }
        }
        else
        {
            if (destinationVector.x >= 0)
            {
                newX = Mathf.Min(rb.position.x + moveSpeed * Time.deltaTime, destinationVector.x);
            }
            else
            {
                newX = Mathf.Max(rb.position.x - moveSpeed * Time.deltaTime, destinationVector.x);
            }

            if (destinationVector.y >= 0)
            {
                newY = Mathf.Min(rb.position.y + moveSpeed * Time.deltaTime, destinationVector.y);
            }
            else
            {
                newY = Mathf.Max(rb.position.y - moveSpeed * Time.deltaTime, destinationVector.y);
            }
        }

        
        rb.MovePosition(new Vector2(newX, newY));

        if (rb.position == destinationVector)
        {
            isReversed = true;
        }
        else if (rb.position == Vector2.zero && isReversed)
        {
            isReversed = false;
            isActivated = false;
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
                // Only carry player right now
                playerDetected = true;
            }
        }

        if (playerDetected)
        {
            isActivated = true;
        }
    }

    private void ResetState()
    {
        isActivated = false;
        isGoingToReset = false;
        rb.MovePosition(Vector2.zero);
    }
}
