using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquaringPlatform : CarryingPlatform
{
    [SerializeField] private Vector2 destinationVector = new Vector2(0, 0);
    [SerializeField] private Vector2 startingVector = new Vector2(0, 0); // Should make sure this is on the correct path
    [SerializeField] private float timeOffset = 0;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private bool reverseDirection = false;
    [SerializeField] private bool invertPosition = false;
    private bool isMovementVectorValidated = false;

    // Start is called before the first frame update
    void Start()
    {
        ValidateMovementVector();
        if (startingVector != Vector2.zero)
        {
            rb.transform.position = startingVector;
        }
        if (invertPosition)
        {
            rb.transform.position = destinationVector;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovementVectorValidated)
        {
            SquaringMovement();
        }
    }

    private void ValidateMovementVector()
    {
        if (destinationVector.x != 0 && destinationVector.y != 0) { isMovementVectorValidated = true; }
        else { Debug.Log("Not valid movement vector"); }
    }

    private void SquaringMovement()
    {
        Vector3 position = rb.transform.position;
        Vector2 newPosition = new Vector2(position.x, position.y);
        float movementStep = moveSpeed/20;

        if (!reverseDirection)
        {
            if (position.x == 0 && position.y < destinationVector.y)
            {
                newPosition.y = Mathf.Min(destinationVector.y, position.y + movementStep);
            }
            else if (position.x < destinationVector.x && position.y == destinationVector.y)
            {
                newPosition.x = Mathf.Min(destinationVector.x, position.x + movementStep);
            }
            else if (position.x == destinationVector.x && position.y > 0)
            {
                newPosition.y = Mathf.Max(0, position.y - movementStep);
            }
            else if (position.y == 0 && position.x > 0)
            {
                newPosition.x = Mathf.Max(0, position.x - movementStep);
            }
        }
        else
        {
            if (position.y == 0 && position.x < destinationVector.x)
            {
                newPosition.x = Mathf.Min(destinationVector.x, position.x + movementStep);
            }
            else if (position.x == destinationVector.x && position.y < destinationVector.y)
            {
                newPosition.y = Mathf.Min(destinationVector.y, position.y + movementStep);
            }
            else if (position.y == destinationVector.y && position.x > 0)
            {
                newPosition.x = Mathf.Max(0, position.x - movementStep);
            }
            else if (position.x == 0 && position.y > 0)
            {
                newPosition.y = Mathf.Max(0, position.y - movementStep);
            }
        }
        

        rb.MovePosition(newPosition);
    }
}
