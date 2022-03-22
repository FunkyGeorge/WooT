using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingPlatform : MonoBehaviour
{
    [SerializeField] Vector2 origin = new Vector2(0, 0);
    [SerializeField] float speed = 1f;
    [SerializeField] float radius = 1f;
    [SerializeField] float offset = 0;
    [SerializeField] protected DeathManagerScriptableObject deathManager;
    private float timeKey = 0f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        if (deathManager)
        {
            timeKey = Time.timeSinceLevelLoad;
            deathManager.deathEvent.AddListener(ResetPosition);
        }
    }

    void OnDisable()
    {
        if (deathManager)
        {
            deathManager.deathEvent.RemoveListener(ResetPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CircleMovement();
    }

    private void CircleMovement()
    {
        float newX = origin.x;
        float newY = origin.y;
        
        newX -= Mathf.Cos((Time.timeSinceLevelLoad - timeKey + offset) * speed) * radius;
        newY += Mathf.Sin((Time.timeSinceLevelLoad - timeKey + offset) * speed) * radius;


        Vector2 newPosition = new Vector2(newX, newY);
        rb.MovePosition(newPosition);
    }

    private void ResetPosition(int deathCount)
    {
        timeKey = Time.timeSinceLevelLoad;
    }
}
