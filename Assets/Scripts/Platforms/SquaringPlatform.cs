using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deprecated
public class SquaringPlatform : CarryingPlatform
{
    [SerializeField] private Vector2 destinationVector = new Vector2(0, 0);
    [SerializeField] private Vector2 startingVector = new Vector2(0, 0); // Should make sure this is on the correct path
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private bool reverseDirection = false;
    [SerializeField] private bool invertPosition = false;
    private bool isMovementVectorValidated = false;
    private float xCursor = 0f;
    private bool xReversed = false;
    private float yCursor = 0f;
    private bool yReversed = false;
    private bool isMovingYAxis = true;

    // Start is called before the first frame update
    void Start()
    {
        ValidateMovementVector();
        if (startingVector != Vector2.zero || invertPosition)
        {
            if (startingVector != Vector2.zero)
            {
                xCursor = startingVector.x;
                yCursor = startingVector.y;
                transform.position = startingVector;

                if (invertPosition)
                {
                    yReversed = true;
                    xReversed = true;
                    // isMovingYAxis = true;

                    if (xCursor == 0) { xReversed = false; isMovingYAxis = true; }
                    if (yCursor == 0) { yReversed = false; isMovingYAxis = false; }
                }
            }
            else if (invertPosition)
            {
                if (startingVector != Vector2.zero)
                {
                    xCursor = destinationVector.x;
                    yCursor = destinationVector.y;
                    transform.position = destinationVector;

                    yReversed = true;
                    xReversed = true;
                    isMovingYAxis = !isMovingYAxis;
                }
            }
        }

        if (reverseDirection)
        {
            isMovingYAxis = !isMovingYAxis;
            xReversed = !xReversed;
            yReversed = !yReversed;
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
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y);

        if (isMovingYAxis)
        {
            float newY = transform.position.y;
            if (!yReversed)
            {
                yCursor += Time.deltaTime * moveSpeed;
                newY = yCursor;
                if (newY >= destinationVector.y)
                {
                    newPosition.y = destinationVector.y;
                    yReversed = !yReversed;
                    isMovingYAxis = !isMovingYAxis;
                }
                else
                {
                    newPosition.y = newY;
                }
            }
            else
            {
                yCursor -= Time.deltaTime * moveSpeed;
                newY = yCursor;
                if (newY <= 0)
                {
                    newPosition.y = 0;
                    yReversed = !yReversed;
                    isMovingYAxis = !isMovingYAxis;
                }
                else
                {
                    newPosition.y = newY;
                }
            }
        }
        else
        {
            float newX = transform.position.x;
            if (!xReversed)
            {
                xCursor += Time.deltaTime * moveSpeed;
                newX = xCursor;
                if (newX >= destinationVector.x)
                {
                    newPosition.x = destinationVector.x;
                    xReversed = !xReversed;
                    isMovingYAxis = !isMovingYAxis;
                }
                else
                {
                    newPosition.x = newX;
                }
            }
            else
            {
                xCursor -= Time.deltaTime * moveSpeed;
                newX = xCursor;
                if (newX <= 0)
                {
                    newPosition.x = 0;
                    xReversed = !xReversed;
                    isMovingYAxis = !isMovingYAxis;
                }
                else
                {
                    newPosition.x = newX;
                }
            }
        }
        

        rb.MovePosition(newPosition);
    }
}
