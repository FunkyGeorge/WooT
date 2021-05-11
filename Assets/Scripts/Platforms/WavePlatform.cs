using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePlatform : MonoBehaviour
{
    [SerializeField] private Vector2 destinationVector = new Vector2(0, 0);
    [SerializeField] private float timeOffset = 0;
    [SerializeField] private float moveSpeed = 1f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        WaveMovement();
    }

    private void WaveMovement()
    {
        float newX = 0;
        float newY = 0;
        if (destinationVector.x > 0)
        {
            newX = Mathf.Sin((Time.time + timeOffset) * moveSpeed) * destinationVector.x/2 + destinationVector.x/2;
        }
        if (destinationVector.y > 0)
        {
            newY = Mathf.Sin((Time.time + timeOffset) * moveSpeed) * destinationVector.y/2 + destinationVector.y/2;
        }
        Vector2 newPosition = new Vector2(newX, newY);
        rb.MovePosition(newPosition);
    }
}
