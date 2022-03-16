using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclingPlatform : CarryingPlatform
{
    [SerializeField] private Vector2 destinationVector = new Vector2(0, 0);
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float timeOffset = 0;
    private float speedAdjustment = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newX = destinationVector.x * (Time.time * moveSpeed * speedAdjustment % 1);
        float newY = destinationVector.y * (Time.time * moveSpeed * speedAdjustment % 1);
        
        Debug.Log(newX + " " + newY);
        rb.position = new Vector2(newX, newY);
    }
}
