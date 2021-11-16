using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryingPlatform : MonoBehaviour
{
    protected Rigidbody2D rb;
    private Vector3 lastPosition;
    private Transform _transform;
    [SerializeField] protected float carryDistanceCheck = 0.1f;
    private bool isCarryingPlayer = false;

    void OnEnable()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _transform = transform;
        lastPosition = _transform.position;
    }

    void LateUpdate()
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
                isCarryingPlayer = true;
            }
        }

        Vector3 velocity = _transform.position - lastPosition;
        if (!playerDetected && isCarryingPlayer && velocity.y >= 0)
        {
            isCarryingPlayer = false;
        }


        if (isCarryingPlayer)
        {
            // Only going to carry the player right now
            Player.Instance.VelocityTweak(velocity);
        }

        

        lastPosition = _transform.position;
    }
}
