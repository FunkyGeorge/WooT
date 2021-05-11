using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private Vector2 destinationVector = new Vector2(0, 0);
    [SerializeField] private float timeOffset = 0;
    [SerializeField] private float moveSpeed = 1f;

    private Vector2 homeVector;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        homeVector = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        float newX = 0;
        float newY = 0;
        if (destinationVector.x > 0)
        {
            newX = Mathf.Sin((Time.time + timeOffset) * moveSpeed) * destinationVector.x / 2 + destinationVector.x / 2;
        }
        if (destinationVector.y > 0)
        {
            newY = Mathf.Sin((Time.time + timeOffset) * moveSpeed) * destinationVector.y / 2 + destinationVector.y / 2;
        }
        Vector2 newPosition = new Vector2(homeVector.x + newX, homeVector.y + newY);

        if (newPosition.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (newPosition.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }

        rb.MovePosition(newPosition);
    }
}
