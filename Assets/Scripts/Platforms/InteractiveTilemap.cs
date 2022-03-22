using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractiveTilemap : CarryingPlatform
{
    // DEPRECATED
    public enum ActionType
    {
        None, // For debugging purposes
        Show,
        Move
    }
    
    [SerializeField] ActionType actionType;
    private TilemapRenderer tilemapRenderer;

    [Header("Show Config")]
    [SerializeField] LevelState.State triggerState;
    private bool hasTriggered = false;

    [Header("Move Config")]
    [SerializeField] private Vector2 destinationVector = new Vector2(0, 0);
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float timeOffset = 0;

    void Start()
    {
        tilemapRenderer = gameObject.GetComponent<TilemapRenderer>();

        if (actionType == ActionType.Show)
        {
            //gameObject.SetActive(false);
            rb.simulated = false;
            tilemapRenderer.enabled = false;
        }

        if (deathManager)
        {
            deathManager.deathEvent.AddListener(OnDeath);
        }
    }

    private void OnDisable()
    {
        if (deathManager)
        {
            deathManager.deathEvent.RemoveListener(OnDeath);
        }
    }

   
    void FixedUpdate()
    {
        if (actionType == ActionType.Show && !hasTriggered)
        {
            LevelState.State actualLevelState = FindObjectOfType<LevelState>().state;
            if (actualLevelState == triggerState)
            {
                Show();

                hasTriggered = true;
            }
        }
        else if (actionType == ActionType.Move)
        {
            CalculateMovement();
        }
    }

    private void Show()
    {
        if (actionType == ActionType.Show)
        {
            rb.simulated = true;
            tilemapRenderer.enabled = true;
        }
    }

    private void CalculateMovement()
    {
        float newX = 0;
        float newY = 0;
        if (destinationVector.x > 0)
        {
            newX = Mathf.PingPong((Time.time - timeKey + timeOffset) * moveSpeed, destinationVector.x);
        }
        if (destinationVector.y > 0)
        {
            newY = Mathf.PingPong((Time.time - timeKey + timeOffset) * moveSpeed, destinationVector.y);
        }
        
        rb.MovePosition(new Vector2(newX, newY));
    }

    private void OnDeath(int deathCount)
    {
        timeKey = Time.time;
    }
}
