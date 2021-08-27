using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongPlatform : CarryingPlatform
{

    [SerializeField] private Vector2 destinationVector = new Vector2(0, 0);
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float timeOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newX = 0;
        float newY = 0;
        if (destinationVector.x > 0)
        {
            newX = Mathf.PingPong((Time.time + timeOffset) * moveSpeed, destinationVector.x);
        }
        if (destinationVector.y > 0)
        {
            newY = Mathf.PingPong((Time.time + timeOffset) * moveSpeed, destinationVector.y);
        }
        
        rb.MovePosition(new Vector2(newX, newY));
    }
}
