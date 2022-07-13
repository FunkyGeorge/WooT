using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 intentDirection;
    [SerializeField] private float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + (intentDirection * speed * 0.2f));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            intentDirection = context.ReadValue<Vector2>();
        }
    }
}
